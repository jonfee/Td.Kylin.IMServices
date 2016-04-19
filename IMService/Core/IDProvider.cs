namespace IMService.Core
{
    public class IDProvider
    {
        /// <summary>
        /// 生成一个新的ID
        /// </summary>
        /// <returns></returns>
        public static long NewId()
        {
            return (long)IDCreater.NewId(10, 10);
        }
    }
}
