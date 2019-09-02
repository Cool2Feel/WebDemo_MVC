using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Hubs;
using System.Diagnostics;

namespace WebApplication1
{
    public class ChatHub : Hub
    {
        #region 
        private static readonly char[] Constant =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
            'W', 'X', 'Y', 'Z'
        };
        List<string> Iplist = new List<string>(UserInfo.array);

        #endregion
        /// <summary>
        /// 供客户端调用的服务器端代码
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            var name = GenerateRandomName(4);
            string ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            //Trace.WriteLine(ipAddress);
            if (Iplist.Contains(ipAddress))
            {
                // 调用所有客户端的sendMessage方法
                Clients.All.sendMessage(name, message);
            }
        }

        public void Send(string name, string message)
        {
            string ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            //Trace.WriteLine(ipAddress);
            if (Iplist.Contains(ipAddress))
            {
                // 调用所有客户端的sendMessage方法
                Clients.All.sendMessage(name, message);
            }
        }

        /// <summary> 
        /// 重写连接事件 
        /// </summary> 
        /// <returns></returns> 
        public override Task OnConnected()
        {
            /*
            // 查询用户 
            var user = OnlineUserList.SingleOrDefault(u => u.ContextId == Context.ConnectionId);

            if (user != null) return base.OnConnected();

            user = new UserInfo
            {
                Name = GenerateRandomName(4),
                ContextId = Context.ConnectionId
            };

            OnlineUserList.Add(user);
            */
            //Trace.WriteLine("客户端连接成功");
            return base.OnConnected();
        }

        /// <summary> 
        /// 重写断开连接事件 
        /// 用户断开连接后，需要移除在线用户列表 
        /// </summary> 
        /// <param name="stopCalled"></param> 
        /// <returns></returns> 
        public override Task OnDisconnected(bool stopCalled)
        {
            /*
            var user = OnlineUserList.FirstOrDefault(u => u.ContextId == Context.ConnectionId);

            //判断用户是否存在,存在则删除 
            if (user != null)
            {
                //删除用户 
                OnlineUserList.Remove(user);
            }

            //更新所有用户的列表 
            GetOnlineUserList();
            */
            //Trace.WriteLine("客户端断开");
            return base.OnDisconnected(stopCalled);
        }
        /*
        /// <summary> 
        /// 获取用户名和自己的唯一编码 
        /// </summary> 
        public void GetName()
        {
            // 查询用户。 
            var user = OnlineUserList.SingleOrDefault(u => u.ContextId == Context.ConnectionId);
            if (user != null)
            {
                Clients.Client(Context.ConnectionId).showNameAndId(user.Name, Context.ConnectionId);
            }

            GetOnlineUserList();
        }

        /// <summary> 
        /// 获取所有在线用户 
        /// </summary> 
        public void GetOnlineUserList()
        {
            var itme = from a in OnlineUserList
                       select new { a.Name, a.ContextId };
            var jsondata = JsonConvert.SerializeObject(itme.ToList());
            Clients.All.getOnlineUserlist(jsondata);// 调用客户端的getOnlineUserlist来获得在线用户列表 
        }
     
        /// <summary> 
        /// 发送消息 
        /// </summary> 
        /// <param name="contextId">发送给用户的ContextId</param> 
        /// <param name="message">发送的消息内容</param> 
        public void SendMessage(string contextId, string message)
        {
            var user = OnlineUserList.FirstOrDefault(u => u.ContextId == contextId);

            // 判断用户是否存在,存在则发送 
            if (user != null)
            {
                // 1V1 聊天，需要把消息往这2个客户端发送 
                // 给指定用户发送,把自己的ID传过去 
                Clients.Client(contextId).addMessage(message + " " + DateTime.Now, Context.ConnectionId);

                // 给自己发送,把用户的ID传给自己 
                Clients.Client(Context.ConnectionId).addMessage(message + " " + DateTime.Now, contextId);
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("该用户已离线");
            }
        }
        */
        /// <summary> 
        /// 产生随机用户名函数 
        /// </summary> 
        /// <param name="length">用户名长度</param> 
        /// <returns></returns> 
        private static string GenerateRandomName(int length)
        {
            var newRandom = new System.Text.StringBuilder(62);
            var rd = new Random();
            for (var i = 0; i < length; i++)
            {
                newRandom.Append(Constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
    }
}