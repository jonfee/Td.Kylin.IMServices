using Td.Kylin.IM.Data.Context;
using Td.Kylin.IM.Data.IService;
using Td.Kylin.IM.Data.Service;

namespace Td.Kylin.IM.Data
{
    /// <summary>
    /// IM 数据操作服务提供
    /// </summary>
    public class ServicesProvider
    {
        private static ServicesProvider _instance;

        private readonly static object mylock = new object();

        /// <summary>
        /// 服务对象集
        /// </summary>
        public static ServicesProvider Items
        {
            get
            {
                if (null == _instance)
                {
                    lock (mylock)
                    {
                        if (null == _instance)
                        {
                            _instance = new ServicesProvider();
                        }
                    }
                }

                return _instance;
            }
        }

        private ServicesProvider()
        {
            switch (StartupConfig.SqlType)
            {
                case Enum.SqlProviderType.POSTGRESQL:
                    InitServiceProvider<PostgreSqlContext>();
                    break;
                case Enum.SqlProviderType.MSSQL:
                    InitServiceProvider<MsSqlContext>();
                    break;
            }
        }

        #region 服务

        public IErrorLogService ErrorLogService { get; private set; }

        public IUnsendMessageService UnsendMessageService { get; private set; }

        public IUserService UserService { get; private set; }

        #endregion

        /// <summary>
        /// 初始化数据操作服务
        /// </summary>
        /// <typeparam name="DbContext"></typeparam>
        private void InitServiceProvider<DbContext>() where DbContext : DataContext, new()
        {
            ErrorLogService = new ErrorLogService<DbContext>();
            UnsendMessageService = new UnsendMessageService<DbContext>();
            UserService = new UserService<DbContext>();
        }
    }
}
