using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using WebApplication1.Models;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class WSChatController : ApiController
    {
        private RoomDBContext db = new RoomDBContext();
        // GET: api/WSChat/5
        public string Get(int id)
        {
            return "value";
        }
        // GET: api/WSChat
        [HttpGet]
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }


        private async Task ProcessWSChat(AspNetWebSocketContext arg)
        {
            WebSocket socket = arg.WebSocket;
            while (true)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                if (socket.State == WebSocketState.Open)
                {
                    string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    string returnMessage = "You send :" + message + ". at" + DateTime.Now.ToLongTimeString();
                    buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(returnMessage));
                    await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    break;
                }
            }
        }


        [HttpGet]
        public string Get(string name,string page)
        {
            var room = db.RoomModels as IQueryable<RoomModels>;
            if (!String.IsNullOrEmpty(name))
            {
                room = room.Where(m => m.Name.Contains(name));
            }
            if (!String.IsNullOrEmpty(page))
            {
                room = room.Where(m => m.Page.Equals(page));
            }

            RoomModels Room = room.Where(m => m.Name.Equals(name)).First();
            
            string jsonData = JsonConvert.SerializeObject(Room);
            return jsonData;
        }
        


        // POST: api/WSChat
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WSChat/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WSChat/5
        public void Delete(int id)
        {
        }
    }

}
