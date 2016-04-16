using Microsoft.Data.Entity;

namespace Td.Kylin.IM.Data.Context
{
    /// <summary>
    /// PostgreSQL Data Context
    /// </summary>
    internal sealed class PostgreSqlContext : DataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseNpgsql(StartupConfig.DbConnectionString);
        }
    }
}
