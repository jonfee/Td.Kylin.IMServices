using Microsoft.Data.Entity;
using Td.Kylin.IM.Data.Entity;

namespace Td.Kylin.IM.Data.Context
{
    /// <summary>
    /// DbContext抽象类
    /// </summary>
    public abstract partial class DataContext : DbContext
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

            modelBuilder.Entity<UnSendMessage>(entity =>
            {
                entity.Property(p => p.MessageID).ValueGeneratedNever();
                entity.HasKey(p => p.MessageID);
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.HasKey(p => p.LogID);
            });

            modelBuilder.Entity<UserLoginRecords>(entity =>
           {
               entity.Property(p => p.RecordID).ValueGeneratedNever();
               entity.HasKey(p => p.RecordID);
           });

            modelBuilder.Entity<UserRelation>(entity =>
           {
               entity.HasKey(p => new { p.FirstUser, p.SecondUser });
           });
        }
        #endregion

        #region DbSet

        public DbSet<User> User { get { return Set<User>(); } }

        public DbSet<MessageHistory> MessageHistory { get { return Set<MessageHistory>(); } }

        public DbSet<UnSendMessage> UnreadMessage { get { return Set<UnSendMessage>(); } }

        public DbSet<ErrorLog> ErrorLog { get { return Set<ErrorLog>(); } }

        public DbSet<UserLoginRecords> UserLoginRecords { get { return Set<UserLoginRecords>(); } }

        public DbSet<UserRelation> UserRelation { get { return Set<UserRelation>(); } }

        #endregion
    }
}
