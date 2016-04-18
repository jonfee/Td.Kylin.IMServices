using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using CodeFirst.Context;

namespace CodeFirst.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20160418052153_addColumnForErrorLog_Tag")]
    partial class addColumnForErrorLog_Tag
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

                    b.Property<string>("Tag");

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

                    b.Property<DateTime>("LastLoginTime");

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

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.UserLoginRecords", b =>
                {
                    b.Property<long>("RecordID");

                    b.Property<int>("AreaID");

                    b.Property<string>("AreaName");

                    b.Property<double>("Latitude");

                    b.Property<DateTime>("LoginTime");

                    b.Property<double>("Longitude");

                    b.Property<int>("TerminalDevice");

                    b.Property<long>("UserID");

                    b.HasKey("RecordID");

                    b.HasAnnotation("Relational:TableName", "UserLoginRecords");
                });
        }
    }
}
