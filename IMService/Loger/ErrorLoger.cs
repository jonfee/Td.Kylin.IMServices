using System;
using System.Text;

namespace IMService.Loger
{
    public class ErrorLoger : BaseLoger
    {
        public ErrorLoger() : base(@"\logs\error.txt") { }

        /// <summary>
        /// 错误信息日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void Write(string title, string message)
        {
            StringBuilder sbContent = new StringBuilder();

            sbContent.Append("\r\n________________________________________________________________________________________________________________\r\n");
            sbContent.Append("日期：" + System.DateTime.Now.ToString() + "\r\n");
            sbContent.Append("错误标题：" + title + "\r\n");
            sbContent.Append("错误信息：" + message + "\r\n");
            sbContent.Append("\r\n________________________________________________________________________________________________________________\r\n");

            base.LogWrite(sbContent);
        }

        /// <summary>
        /// 错误信息日志
        /// </summary>
        /// <param name="ex"></param>
        public void Write(Exception ex)
        {
            if (null == ex) return;

            StringBuilder sbContent = new StringBuilder();

            sbContent.Append("\r\n________________________________________________________________________________________________________________\r\n");
            sbContent.Append("日期：" + System.DateTime.Now.ToString() + "\r\n");
            sbContent.Append("错误标题：" + ex.Message + "\r\n");
            sbContent.Append("错误信息：" + ex.StackTrace + "\r\n");
            sbContent.Append("\r\n________________________________________________________________________________________________________________\r\n");

            base.LogWrite(sbContent);
        }
    }
}
