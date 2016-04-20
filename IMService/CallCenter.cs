using IMService.Core;
using IMService.Loger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Td.Kylin.IM.Data;
using Td.Kylin.IM.Data.Entity;
using Td.Messenge.Protocols;
using TDQueue;
using XL.Sockets;

namespace IMService
{
    /// <summary>
    /// IM服务中心
    /// </summary>
    internal sealed class CallCenter : IDisposable
    {
        #region ////////////////////变量成员//////////////////////

        /// <summary>
        /// 是否被释放
        /// </summary>
        bool m_disposed;

        /// <summary>
        /// TCP IM 服务器
        /// </summary>
        public TcpServer Server;

        /// <summary>
        /// 消息包
        /// </summary>
        PacketAnalyzer PacketAnalyzer;

        /// <summary>
        /// 待写入历史的消息队列
        /// </summary>
        LinkQueue<TextMessage> QueueWriteToHistory;

        /// <summary>
        /// 等待写入数据库的未发送出去的消息队列
        /// </summary>
        LinkQueue<TextMessage> QueueUnSendToDB;

        /// <summary>
        /// 登录消息队列
        /// </summary>
        LinkQueue<Login> QueueLogin;

        /// <summary>
        /// 等待发送给用户的消息队列
        /// </summary>
        LinkQueue<TextMessage> QueueWaitSend;

        /// <summary>
        /// 用户未接收到的消息数量Hash存储
        /// </summary>
        Hashtable UserUnreceived = Hashtable.Synchronized(new Hashtable());

        #endregion

        #region //////////////////////线程处理队列/////////////////////////////

        /// <summary>
        /// 写入消息历史队列处理-线程
        /// </summary>
        Thread threadWriteToHistoryQueueWork;

        /// <summary>
        /// 登录消息队列处理-线程
        /// </summary>
        Thread threadLoginQueueWork;

        /// <summary>
        /// 待写入数据库的未发送出去消息队列处理-线程
        /// </summary>
        Thread threadUnsendQueueWork;

        /// <summary>
        /// 待发送给用户的消息队列处理-线程
        /// </summary>
        Thread threadWaitSendQueueWork;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public CallCenter()
        {
            #region 变量初始化
            QueueWriteToHistory = new LinkQueue<TextMessage>();
            QueueUnSendToDB = new LinkQueue<TextMessage>();
            QueueLogin = new LinkQueue<Login>();
            QueueWaitSend = new LinkQueue<TextMessage>();
            #endregion

            #region IM服务端初始化配置
            PacketAnalyzer = new ProtobufPacket();
            TcpConfig.Setup(Startup.ServerConfig.MaxConnectionCount);
            Server = new TcpServer();
            Server.ChannelConnected += Server_ChannelConnected; //客户端连接时处理
            Server.ChannelDisposed += Server_ChannelDisposed;   //客户端断开时处理
            Server.ChannelReceived += Server_ChannelReceived;   //客户端收到消息时处理
            Server.ChannelSent += Server_ChannelSent;           //客户端发送消息处理
            Server.Error += Server_Error;                       //服务发生错误时处理
            #endregion

            #region 线程初始化

            threadWriteToHistoryQueueWork = new Thread(new ThreadStart(WriteToHistoryQueueWork));   //写入消息历史队列处理-线程
            threadLoginQueueWork = new Thread(new ThreadStart(LoginQueueWork));                     //登录消息队列处理-线程
            threadUnsendQueueWork = new Thread(new ThreadStart(UnsendQueueWork));                   //待写入数据库的未发送出去消息队列处理-线程
            threadWaitSendQueueWork = new Thread(new ThreadStart(WaitSendQueueWork));               //待发送给用户的消息队列处理-线程
            threadWriteToHistoryQueueWork.IsBackground = true;
            threadLoginQueueWork.IsBackground = true;
            threadUnsendQueueWork.IsBackground = true;
            threadWaitSendQueueWork.IsBackground = true;

            #endregion
        }

        /// <summary>
        /// 服务开始
        /// </summary>
        public void Start()
        {
            Server.Open(Startup.ServerConfig.ServerIP, Startup.ServerConfig.Port, Startup.ServerConfig.MaxConnectionCount);

            threadWriteToHistoryQueueWork.Start();
            threadLoginQueueWork.Start();
            threadUnsendQueueWork.Start();
            threadWaitSendQueueWork.Start();
        }

        #region //////////////////////////队列处理///////////////////////////

        /// <summary>
        /// 待写入消息历史的队列处理
        /// </summary>
        private void WriteToHistoryQueueWork()
        {
            while (true)
            {
                //出队
                var item = QueueWriteToHistory.DeQueue();  

                if (null != item)
                {
                    var userNameDic = ServicesProvider.Items.UserService.GetUserName(new[] { item.SenderID, item.ReceiverID });

                    MessageHistory message = new MessageHistory
                    {
                        Content = item.Content,
                        MessageID = IDProvider.NewId(),
                        MessageType = item.MessageType,
                        ReadTime = item.SendTime,
                        ReceiverID = item.ReceiverID,
                        ReceiverName = userNameDic.ContainsKey(item.ReceiverID) ? userNameDic[item.ReceiverID] : item.ReceiverName,
                        SenderID = item.SenderID,
                        SenderName = userNameDic.ContainsKey(item.SenderID) ? userNameDic[item.SenderID] : string.Empty,
                        SendTime = item.SendTime
                    };

                    //写入数据库
                    ServicesProvider.Items.MessageHistoryService.AddMessage(message);
                }

                //避免无数据操作时CPU空转
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 未发送出去消息队列处理
        /// </summary>
        private void UnsendQueueWork()
        {
            while (true)
            {
                //出队
                var item = QueueUnSendToDB.DeQueue();

                if (null != item)
                {
                    UnSendMessage message = new UnSendMessage
                    {
                        Content = item.Content,
                        MessageID = IDProvider.NewId(),
                        MessageType = item.MessageType,
                        ReceiverID = item.ReceiverID,
                        SenderID = item.SenderID,
                        SenderName = item.SernderName,
                        SendTime = item.SendTime
                    };

                    //写入数据库
                    ServicesProvider.Items.UnsendMessageService.AddMessage(message);
                }

                //避免无数据操作时CPU空转
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 登录操作队列处理
        /// </summary>
        private void LoginQueueWork()
        {
            while (true)
            {
                //出队
                var item = QueueLogin.DeQueue();

                if (null != item)
                {
                    UserLoginRecords records = new UserLoginRecords
                    {
                        AreaID = item.AreaID,
                        AreaName = item.AreaName,
                        Latitude = item.Latitude,
                        LoginTime = item.LoginTime,
                        Longitude = item.Longitude,
                        RecordID = IDProvider.NewId(),
                        TerminalDevice = item.TerminalDevice,
                        UserID = item.UserID
                    };

                    //更新用户信息
                    ServicesProvider.Items.UserService.UpdateLastInfo(item.UserID, item.UserName, item.UserType, item.AreaName, item.LoginTime);

                    //记录登录操作信息
                    ServicesProvider.Items.UserLoginRecordService.AddRecord(records);
                }

                //避免无数据操作时CPU空转
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 待发送的消息队列
        /// </summary>
        private void WaitSendQueueWork()
        {
            while (true)
            {
                //出队
                var item = QueueWaitSend.DeQueue();

                if (null != item)
                {
                    IChannel channel = GetUserChannel(item.ReceiverID);

                    //通道存在
                    if (null != channel)
                    {
                        //发送消息
                        Server.Send(item, channel);
                    }
                }

                //避免无数据操作时CPU空转
                Thread.Sleep(100);
            }
        }

        #endregion

        #region /////////////////////////IM服务监听事件/////////////////////////

        /// <summary>
        /// 服务错误触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Server_Error(object sender, ErrorEventArgs e)
        {
            ErrorLoger loger = new ErrorLoger();
            loger.Write(e.Error);

            //写入错误日志
            ServicesProvider.Items.ErrorLogService.AddLog(e.Error, e.Tag);
        }

        /// <summary>
        /// 消息发送成功后触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Server_ChannelSent(object sender, ChannelSendEventArgs e)
        {
            //TODO 
        }

        /// <summary>
        /// 成功接收到通道消息后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Server_ChannelReceived(object sender, ChannelReceiveEventArgs e)
        {
            if (!e.Channel.IsDisposed)
            {
                e.Channel.Package.Import(e.Data.Array, e.Data.Offset, e.Data.Count);
            }
        }

        /// <summary>
        /// 客户端中断连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Server_ChannelDisposed(object sender, ChannelEventArgs e)
        {
            //TODO
            ServerLoger loger = new ServerLoger();
            loger.Write(string.Format("通道：{0}[IP:{1}]已断开！ 时间：{2}", e.Channel.Name, e.Channel.EndPoint.ToString(), DateTime.Now));
        }

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Server_ChannelConnected(object sender, ChannelConnectEventArgs e)
        {
            e.Channel.Package = (IPackage)PacketAnalyzer.Clone();
            e.Channel.Package.PackageReceive = OnReceiveMessage;
            e.Channel.Package.Channel = e.Channel;

            ServerLoger loger = new ServerLoger();
            loger.Write(string.Format("通道：{0}[IP:{1}]已连接！ 时间：{2}", e.Channel.Name, e.Channel.EndPoint.ToString(), DateTime.Now));
        }

        /// <summary>
        /// 接收到消息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnReceiveMessage(object sender, PackageReceiveArgs e)
        {
            if (e.Message is Login)
            {
                ReceiveForLogin(e);
            }
            else if (e.Message is TextMessage)
            {
                RecieveForTextMessage(e);
            }
        }

        #region 消息包处理

        /// <summary>
        /// 登录消息包接收后处理
        /// </summary>
        /// <param name="e"></param>
        void ReceiveForLogin(PackageReceiveArgs e)
        {
            var login = e.Message as Login;

            if (null == login) return;
            
            login.LoginTime = DateTime.Now;

            //设置通道名称为UserID
            e.Channel.Name = login.UserID.ToString();

            //写入登录队列处理
            QueueLogin.EnQueue(login);

            #region 发送登录结果消息

            //登录信息
            var loginInfo = ServicesProvider.Items.UserService.GetUserLoginInfo(login.UserID);
            //如果登录信息不存在，则表明当前用户为首次登录
            if (null == loginInfo)
            {
                loginInfo = new Td.Kylin.IM.Data.Model.UserLoginInfo
                {
                    UserID = login.UserID,
                    LastLoginAddress = login.AreaName,
                    LastLoginTime = login.LoginTime,
                    PrevLoginAddress = login.AreaName,
                    PrevLoginTime = login.LoginTime
                };
            }
            //如果获取的登录信息记录的最后登录时间与本次登录时间不一致，说明未更新到当前登录，则采用最后前置
            else if (loginInfo.LastLoginTime.ToString("yyyyMMddHHmmss") != login.LoginTime.ToString("yyyyMMddHHmmss"))
            {
                loginInfo.PrevLoginAddress = loginInfo.LastLoginAddress;
                loginInfo.PrevLoginTime = loginInfo.LastLoginTime;
                loginInfo.LastLoginAddress = login.AreaName;
                loginInfo.LastLoginTime = login.LoginTime;
            }

            //登录结果回传
            LoginPostBack backData = new LoginPostBack
            {
                Error = null,
                IsSuccess = true,
                LoginTime = login.LoginTime,
                PrevLoginAddress = loginInfo.PrevLoginAddress,
                PrevLoginTime = loginInfo.PrevLoginTime,
                UserID = login.UserID,
                UserName = login.UserName
            };

            Server.Send(backData, e.Channel);

            #endregion

            #region 发送离线消息

            int unreceivedCount = GetUnrecivedCount(login.UserID);

            //如果存在未接收的消息
            if (unreceivedCount > 0)
            {
                //未接收成功的消息包集合
                var unreceivedList = ServicesProvider.Items.UnsendMessageService.GetList(login.UserID);

                if (null != unreceivedList)
                {
                    //放入待发送消息队列中
                    foreach (var m in unreceivedList)
                    {
                        var waitMsg = new TextMessage
                        {
                            Content = m.Content,
                            MessageID = m.MessageID,
                            MessageType = m.MessageType,
                            ReceiverID = m.ReceiverID,
                            ReceiverName = string.Empty,
                            SenderID = m.SenderID,
                            SendTime = m.SendTime,
                            SernderName = m.SenderName
                        };

                        QueueWaitSend.EnQueue(waitMsg);
                    }

                    //更新未接收数量标识信息
                    AddUnrecived(login.UserID, unreceivedList.Count());

                    //将本次回传的消息从未发送记录数据库中移除
                    long[] delIDs = unreceivedList.Select(p => p.MessageID).ToArray();
                    ServicesProvider.Items.UnsendMessageService.DeleteMessage(delIDs);
                }
            }

            #endregion
        }

        /// <summary>
        /// 文本消息包接收后处理
        /// </summary>
        /// <param name="e"></param>
        void RecieveForTextMessage(PackageReceiveArgs e)
        {
            var msg = e.Message as TextMessage;

            if (null == msg) return;
            
            msg.MessageID = IDProvider.NewId();
            msg.SendTime = DateTime.Now;

            //加入写入历史消息队列
            QueueWriteToHistory.EnQueue(msg);

            IChannel channel = GetUserChannel(msg.ReceiverID);

            //如果消息接收者已连接
            if (null != channel)
            {
                //发送消息给接收者
                Server.Send(msg, channel);

                //放入发送队列
                //QueueWaitSend.EnQueue(msg);
            }
            else
            {
                //加入未发送消息队列
                QueueUnSendToDB.EnQueue(msg);

                //记录未发送消息标识
                AddUnrecived(msg.ReceiverID, 1);
            }
        }

        #endregion

        #endregion

        #region ////////////////////未接收消息数量Hashtable处理////////////////////////////

        /// <summary>
        /// 更新用户未接收的消息数量
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="count"></param>
        void UpdateUnreceived(long userID, int count)
        {
            if (count < 0) count = 0;

            if (UserUnreceived.ContainsKey(userID))
            {
                UserUnreceived[userID] = count;
            }
            else
            {
                UserUnreceived.Add(userID, count);
            }
        }

        /// <summary>
        /// 新增用户未接收的消息数量
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="addCount"></param>
        void AddUnrecived(long userID, int addCount)
        {
            int lastCount = addCount;

            if (UserUnreceived.ContainsKey(userID))
            {
                int count = int.Parse(UserUnreceived[userID].ToString());

                lastCount = count + addCount;

                if (lastCount < 0) lastCount = 0;

                UserUnreceived[userID] = lastCount;
            }
            else
            {
                UserUnreceived.Add(userID, lastCount);
            }
        }

        /// <summary>
        /// 获取用户未接收的消息数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        int GetUnrecivedCount(long userID)
        {
            if (UserUnreceived.ContainsKey(userID))
            {
                return int.Parse(UserUnreceived[userID].ToString());
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region /////////////获取连接通道//////////////////

        /// <summary>
        /// 获取用户的连接通道
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        IChannel GetUserChannel(long userID)
        {
            foreach (var channel in Server.GetOnlines())
            {
                if (channel.Name == userID.ToString())
                {
                    return channel;
                }
            }

            return null;
        }

        /// <summary>
        /// 检测用户是否存在通道连接
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        bool HasChannel(long userID)
        {
            var channelIds = Server.GetOnlines().Select(p => p.Name).ToArray();

            return channelIds != null ? channelIds.Contains(userID.ToString()) : false;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// 析构函数，当没有调用Dispose()时由GC完成资源的回收
        /// </summary>
        ~CallCenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    threadWriteToHistoryQueueWork.Abort();
                    threadLoginQueueWork.Abort();
                    threadUnsendQueueWork.Abort();
                    threadWaitSendQueueWork.Abort();

                    Server.Dispose();
                }

                m_disposed = true;
            }
        }

        #endregion
    }
}
