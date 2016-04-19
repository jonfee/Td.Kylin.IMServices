using IMService.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Td.Kylin.IM.Data;
using Td.Kylin.IM.Data.Entity;
using Td.Messenge.Protocols;
using TDQueue;
using XL.Sockets;

namespace IMService
{
    class CallCenter<Packet> : ICallCenter where Packet : PacketAnalyzer, new()
    {
        /// <summary>
        /// TCP IM 服务器
        /// </summary>
        TcpServer Server;

        /// <summary>
        /// 消息包
        /// </summary>
        PacketAnalyzer PacketAnalyzer;

        /// <summary>
        /// 待写入历史的消息队列
        /// </summary>
        LinkQueue<TextMessage> QueueWriteToHistory;

        /// <summary>
        /// 未发送出去的消息队列
        /// </summary>
        LinkQueue<TextMessage> QueueUnSend;

        /// <summary>
        /// 登录消息队列
        /// </summary>
        LinkQueue<Login> QueueLogin;

        /// <summary>
        /// 用户未接收到的消息数量Hash存储
        /// </summary>
        Hashtable UserUnreceived = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 初始化
        /// </summary>
        public CallCenter()
        {
            QueueWriteToHistory = new LinkQueue<TextMessage>();
            QueueUnSend = new LinkQueue<TextMessage>();
            QueueLogin = new LinkQueue<Login>();
            PacketAnalyzer = new Packet();

            TcpConfig.Setup(Startup.ServerConfig.MaxConnectionCount);

            Server = new TcpServer();
            Server.ChannelConnected += Server_ChannelConnected;
            Server.ChannelDisposed += Server_ChannelDisposed;
            Server.ChannelReceived += Server_ChannelReceived;
            Server.ChannelSent += Server_ChannelSent;
            Server.Error += Server_Error;
            Server.Open(Startup.ServerConfig.ServerIP, Startup.ServerConfig.Port, Startup.ServerConfig.MaxConnectionCount);

            //写入消息历史队列处理
            ThreadPool.QueueUserWorkItem(new WaitCallback(QueueWriteToHistoryStart));

            //登录消息队列处理
            ThreadPool.QueueUserWorkItem(new WaitCallback(QueueLoginStart));

            //未发送出去消息队列处理
            ThreadPool.QueueUserWorkItem(new WaitCallback(QueueUnsendStart));
        }

        #region 队列处理

        /// <summary>
        /// 待写入消息历史的队列处理
        /// </summary>
        /// <param name="state"></param>
        private void QueueWriteToHistoryStart(object state)
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

                    //检测用户之间是否存在关联
                    bool hasRelation = ServicesProvider.Items.UserRelationService.HasRelation(message.SenderID, message.ReceiverID);

                    //不存在则创建关联
                    if (!hasRelation)
                    {
                        ServicesProvider.Items.UserRelationService.CreateRelation(message.SenderID, message.ReceiverID);
                    }
                }

                //避免无数据操作时CPU空转
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 未发送出去消息队列处理
        /// </summary>
        /// <param name="state"></param>
        private void QueueUnsendStart(object state)
        {
            while (true)
            {
                //出队
                var item = QueueUnSend.DeQueue();

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
        /// <param name="state"></param>
        private void QueueLoginStart(object state)
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

                    //更新用户最后登录时间
                    ServicesProvider.Items.UserService.UpdateLastLoginTime(item.UserID, item.LoginTime);

                    //记录登录操作信息
                    ServicesProvider.Items.UserLoginRecordService.AddRecord(records);
                }

                //避免无数据操作时CPU空转
                Thread.Sleep(100);
            }
        }

        #endregion

        /// <summary>
        /// 服务错误触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Server_Error(object sender, ErrorEventArgs e)
        {
            //写入错误日志
            ServicesProvider.Items.ErrorLogService.AddLog(e.Error, e.Tag);
        }

        /// <summary>
        /// 消息发送成功后触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Server_ChannelSent(object sender, ChannelSendEventArgs e)
        {
            //TODO 
        }

        /// <summary>
        /// 通道成功接收到消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Server_ChannelReceived(object sender, ChannelReceiveEventArgs e)
        {
            if (e.Channel.IsDisposed)
            {
                e.Channel.Package.Import(e.Data.Array, e.Data.Offset, e.Data.Count);
            }
        }

        /// <summary>
        /// 客户端中断连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Server_ChannelDisposed(object sender, ChannelEventArgs e)
        {
            //TODO
        }

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Server_ChannelConnected(object sender, ChannelConnectEventArgs e)
        {
            e.Channel.Package = (IPackage)PacketAnalyzer.Clone();
            e.Channel.Package.PackageReceive = OnReceiveMessage;
            e.Channel.Package.Channel = e.Channel;
        }

        /// <summary>
        /// 服务端接收到消息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnReceiveMessage(object sender, PackageReceiveArgs e)
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
        private void ReceiveForLogin(PackageReceiveArgs e)
        {
            var login = e.Message as Login;

            if (null == login) return;

            login.LoginTime = DateTime.Now;

            //写入登录队列处理
            QueueLogin.EnQueue(login);

            int unreceivedCount = GetUnrecivedCount(login.UserID);

            //如果存在未接收的消息
            if (unreceivedCount > 0)
            {
                //未接收成功的消息包集合
                var unreceivedList = ServicesProvider.Items.UnsendMessageService.GetList(login.UserID);

                List<LoginPostBackItem> items = new List<LoginPostBackItem>();
                unreceivedList.ForEach((item =>
                {
                    items.Add(new LoginPostBackItem
                    {
                        Content = item.Content,
                        MessageID = item.MessageID,
                        MessageType = item.MessageType,
                        SenderID = item.SenderID,
                        SernderName = item.SenderName,
                        SendTime = item.SendTime
                    });
                }));

                LoginPostBack back = new LoginPostBack { ReceiverID = login.UserID, MessageList = items };

                //发送回传消息包
                Server.Send(back, e.Channel);

                //更新未接收数量标识信息
                AddUnrecived(login.UserID, back.MessageCount);

                //将本次回传的消息从未发送记录数据库中移除
                long[] delIDs = unreceivedList.Select(p => p.MessageID).ToArray();
                ServicesProvider.Items.UnsendMessageService.DeleteMessage(delIDs);
            }
        }

        /// <summary>
        /// 文本消息包接收后处理
        /// </summary>
        /// <param name="e"></param>
        private void RecieveForTextMessage(PackageReceiveArgs e)
        {
            var msg = e.Message as TextMessage;

            if (null == msg) return;

            msg.MessageID = IDProvider.NewId();
            msg.SendTime = DateTime.Now;

            IChannel channel = GetUserChannel(msg.ReceiverID);

            if (null != channel)
            {
                //加入写入历史消息队列
                QueueWriteToHistory.EnQueue(msg);

                //发送消息给接收者
                Server.Send(msg, channel);
            }
            else
            {
                //加入未发送消息队列
                QueueUnSend.EnQueue(msg);

                //记录未发送消息标识
                AddUnrecived(msg.ReceiverID, 1);
            }
        }

        #endregion

        /// <summary>
        /// 更新用户未接收的消息数量
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="count"></param>
        private void UpdateUnreceived(long userID, int count)
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

            ServicesProvider.Items.UserService.UpdateUserUnReceivedCount(userID, count);
        }

        /// <summary>
        /// 新增用户未接收的消息数量
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="addCount"></param>
        private void AddUnrecived(long userID, int addCount)
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

            ServicesProvider.Items.UserService.UpdateUserUnReceivedCount(userID, lastCount);
        }

        /// <summary>
        /// 获取用户未接收的消息数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        private int GetUnrecivedCount(long userID)
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

        /// <summary>
        /// 获取用户的连接通道
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        private IChannel GetUserChannel(long userID)
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
    }
}
