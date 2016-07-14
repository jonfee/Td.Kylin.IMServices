using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Context
{
    /// <summary>
    /// DbContext抽象类
    /// </summary>
    internal partial class DataContext : Td.Kylin.IM.Data.Context.DataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            //optionBuilder.UseNpgsql(Startup.Configuration["Data:IMConnectionString"]);
            string conn = Startup.Configuration["Data:IMConnectionString"];
            optionBuilder.UseSqlServer(conn);
        }
    }
}
