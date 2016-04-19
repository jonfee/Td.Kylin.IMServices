using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using CodeFirst.Context;

namespace CodeFirst.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20160419035215_addColumnForUnSendMessage_SenderName")]
    partial class addColumnForUnSendMessage_SenderName
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

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.MessageHistory", b =>
                {
                    b.Property<long>("MessageID");

                    b.Property<string>("Content")
                        .HasAnnotation("Relational:ColumnType", "varchar(500)");

                    b.Property<int>("MessageType");

                    b.Property<DateTime?>("ReadTime");

                    b.Property<long>("ReceiverID");

                    b.Property<string>("ReceiverName")
                        .HasAnnotation("Relational:ColumnType", "varchar(50)");

                    b.Property<DateTime>("SendTime");

                    b.Property<long>("SenderID");

                    b.Property<string>("SenderName")
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

                    b.Property<long>("ReceiverID");

                    b.Property<DateTime>("SendTime");

                    b.Property<long>("SenderID");

                    b.Property<string>("SenderName")
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

            modelBuilder.Entity("Td.Kylin.IM.Data.Entity.UserRelation", b =>
                {
                    b.Property<long>("FirstUser");

                    b.Property<long>("SecondUser");

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("FirstDelete");

                    b.Property<string>("FirstGroupName");

                    b.Property<bool>("SecondDelete");

                    b.Property<string>("SecondGroupName");

                    b.HasKey("FirstUser", "SecondUser");

                    b.HasAnnotation("Relational:TableName", "UserRelation");
                });
        }
    }
}
