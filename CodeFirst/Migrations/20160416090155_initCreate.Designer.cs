using CodeFirst.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CodeFirst.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20160416090155_initCreate")]
    partial class initCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.LastMessage", b =>
                {
                    b.Property<long>("MessageID");

                    b.Property<string>("Content")
                        .HasAnnotation("Relational:ColumnType", "varchar(500)");

                    b.Property<bool>("IsRead");

                    b.Property<DateTime?>("ReadTime");

                    b.Property<long>("Receiver");

                    b.Property<string>("ReceiverName")
                        .HasAnnotation("Relational:ColumnType", "varchar(50)");

                    b.Property<DateTime>("SendTime");

                    b.Property<long>("UserID");

                    b.Property<string>("UserName")
                        .HasAnnotation("Relational:ColumnType", "varchar(50)");

                    b.HasKey("MessageID");

                    b.HasAnnotation("Relational:TableName", "LastMessage");
                });

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.MessageHistory", b =>
                {
                    b.Property<long>("MessageID");

                    b.Property<string>("Content")
                        .HasAnnotation("Relational:ColumnType", "varchar(500)");

                    b.Property<bool>("IsRead");

                    b.Property<DateTime?>("ReadTime");

                    b.Property<long>("Receiver");

                    b.Property<string>("ReceiverName")
                        .HasAnnotation("Relational:ColumnType", "varchar(50)");

                    b.Property<DateTime>("SendTime");

                    b.Property<long>("UserID");

                    b.Property<string>("UserName")
                        .HasAnnotation("Relational:ColumnType", "varchar(50)");

                    b.HasKey("MessageID");

                    b.HasAnnotation("Relational:TableName", "MessageHistory");
                });

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.UnreadMessage", b =>
                {
                    b.Property<long>("MessageID");

                    b.Property<string>("Content")
                        .HasAnnotation("Relational:ColumnType", "varchar(500)");

                    b.Property<long>("Receiver");

                    b.Property<DateTime>("SendTime");

                    b.Property<long>("UserID");

                    b.Property<string>("UserName")
                        .HasAnnotation("Relational:ColumnType", "varchar(50)");

                    b.HasKey("MessageID");

                    b.HasAnnotation("Relational:TableName", "UnreadMessage");
                });

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.User", b =>
                {
                    b.Property<long>("UserID");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("NickName")
                        .HasAnnotation("Relational:ColumnType", "varchar(50)");

                    b.Property<string>("Photo")
                        .HasAnnotation("Relational:ColumnType", "varchar(100)");

                    b.Property<int>("Status");

                    b.Property<int>("UnreadCount");

                    b.Property<int>("UserType");

                    b.HasKey("UserID");

                    b.HasAnnotation("Relational:TableName", "User");
                });
        }
    }
}
