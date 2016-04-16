using Microsoft.Data.Entity;

namespace Td.Kylin.IM.Data.Context
{
    /// <summary>
    /// MS SQL Data Context
    /// </summary>
    internal sealed class MsSqlContext : DataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(StartupConfig.DbConnectionString);
        }
    }
}
