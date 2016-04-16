using Microsoft.Data.Entity;
using Td.Kylin.IM.Data.Entity;

namespace Td.Kylin.IM.Data.Context
{
    /// <summary>
    /// DbContext抽象类
    /// </summary>
    internal abstract partial class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(StartupConfig.DbConnectionString);
        }

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(p => p.UserID).ValueGeneratedNever();
                entity.HasKey(p => p.UserID);
            });

            modelBuilder.Entity<MessageHistory>(entity =>
            {
                entity.Property(p => p.MessageID).ValueGeneratedNever();
                entity.HasKey(p => p.MessageID);
            });

            modelBuilder.Entity<UnreadMessage>(entity =>
            {
                entity.Property(p => p.MessageID).ValueGeneratedNever();
                entity.HasKey(p => p.MessageID);
            });

            modelBuilder.Entity<LastMessage>(entity =>
            {
                entity.Property(p => p.MessageID).ValueGeneratedNever();
                entity.HasKey(p => p.MessageID);
            });
        }
        #endregion

        #region DbSet

        public DbSet<User> User { get { return Set<User>(); } }

        public DbSet<MessageHistory> MessageHistory { get { return Set<MessageHistory>(); } }

        public DbSet<UnreadMessage> UnreadMessage { get { return Set<UnreadMessage>(); } }

        public DbSet<LastMessage> LastMessage { get { return Set<LastMessage>(); } }

        #endregion
    }
}
