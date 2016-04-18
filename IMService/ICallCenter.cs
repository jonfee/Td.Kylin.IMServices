using XL.Sockets;

namespace IMService
{
    interface ICallCenter
    {
        /// <summary>
        /// 服务错误触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       void Server_Error(object sender, ErrorEventArgs e);

        /// <summary>
        /// 消息发送成功后触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Server_ChannelSent(object sender, ChannelSendEventArgs e);

        /// <summary>
        /// 成功接收到消息后触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Server_ChannelReceived(object sender, ChannelReceiveEventArgs e);

        /// <summary>
        /// 客户端中断连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Server_ChannelDisposed(object sender, ChannelEventArgs e);

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Server_ChannelConnected(object sender, ChannelConnectEventArgs e);

        /// <summary>
        /// 接收到消息后处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnReceiveMessage(object sender, PackageReceiveArgs e);
    }
}
