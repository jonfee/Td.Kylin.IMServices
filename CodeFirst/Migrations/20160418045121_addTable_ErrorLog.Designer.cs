using CodeFirst.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CodeFirst.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20160418045121_addTable_ErrorLog")]
    partial class addTable_ErrorLog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.ErrorLog", b =>
                {
                    b.Property<long>("LogID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("HelpLink")
                        .HasAnnotation("Relational:ColumnType", "varchar(200)");

                    b.Property<string>("Message")
                        .HasAnnotation("Relational:ColumnType", "varchar(200)");

                    b.Property<string>("Source")
                        .HasAnnotation("Relational:ColumnType", "varchar(200)");

                    b.Property<string>("StackTrace")
                        .HasAnnotation("Relational:ColumnType", "text");

                    b.HasKey("LogID");

                    b.HasAnnotation("Relational:TableName", "ErrorLog");
                });

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.LastMessage", b =>
                {
                    b.Property<long>("MessageID");

                    b.Property<string>("Content")
                        .HasAnnotation("Relational:ColumnType", "varchar(500)");

                    b.Property<int>("MessageType");

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

                    b.Property<int>("MessageType");

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

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.UnSendMessage", b =>
                {
                    b.Property<long>("MessageID");

                    b.Property<string>("Content")
                        .HasAnnotation("Relational:ColumnType", "varchar(500)");

                    b.Property<int>("MessageType");

                    b.Property<long>("Receiver");

                    b.Property<DateTime>("SendTime");

                    b.Property<long>("UserID");

                    b.Property<string>("UserName")
                        .HasAnnotation("Relational:ColumnType", "varchar(50)");

                    b.HasKey("MessageID");

                    b.HasAnnotation("Relational:TableName", "UnSendMessage");
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

                    b.Property<int>("UnReceivedCount");

                    b.Property<int>("UserType");

                    b.HasKey("UserID");

                    b.HasAnnotation("Relational:TableName", "User");
                });
        }
    }
}
