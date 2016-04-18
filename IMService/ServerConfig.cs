using System.Net;

namespace IMService
{
    /// <summary>
    /// IM服务器配置
    /// </summary>
    class ServerConfig
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        public IPAddress ServerIP { get; set; }

        /// <summary>
        /// 服务器访问端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 最大连接数
        /// </summary>
        public int MaxConnectionCount { get; set; }
    }
}
