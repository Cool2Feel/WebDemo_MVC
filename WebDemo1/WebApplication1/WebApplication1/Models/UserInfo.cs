using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserInfo
    {
        public string ContextId { get; set; }
        public string Name { get; set; }

        public static string[] array = { "192.168.3.69", "192.168.4.61", "192.168.4.62", "192.168.41.132", "::1", "127.0.0.1" };
    }

}