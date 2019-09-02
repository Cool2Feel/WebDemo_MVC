using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApplication1.Models;

namespace WebApplication1.Hubs
{
    public class BaseHub : Hub
    {
        /// <summary> 
        /// 在线用户列表 
        /// </summary> 
        public static IList<UserInfo> OnlineUserList = new List<UserInfo>();
    }
}