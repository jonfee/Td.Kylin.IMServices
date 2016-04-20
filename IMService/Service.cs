using System.ServiceProcess;

namespace IMService
{
    /// <summary>
    /// 服务
    /// </summary>
    public partial class Service : ServiceBase
    {
        CallCenter CallService;

        public Service()
        {
            InitializeComponent();

            //实例化服务
            CallService = new CallCenter();
        }

        protected override void OnStart(string[] args)
        {
            //启动服务
            CallService.Start();

            EventLog.WriteEntry(string.Format("IM服务已启动，IP：{0}，Port:{1}", Startup.ServerConfig.ServerIP, CallService.Server.Port));
        }

        protected override void OnStop()
        {
            //释放资源
            CallService.Dispose();
        }
    }
}
