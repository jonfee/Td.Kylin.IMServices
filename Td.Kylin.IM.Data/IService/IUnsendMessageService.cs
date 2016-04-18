﻿using System;
using System.Collections.Generic;
using Td.Kylin.IM.Data.Entity;

namespace Td.Kylin.IM.Data.IService
{
    /// <summary>
    /// 未发送消息数据服务接口
    /// </summary>
    public interface IUnsendMessageService
    {
        /// <summary>
        /// 获取用户未接收到的消息集合
        /// </summary>
        /// <param name="receiverID"></param>
        /// <returns></returns>
        List<UnSendMessage> GetList(long receiverID);
    }
}
