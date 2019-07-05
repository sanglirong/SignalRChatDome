using Microsoft.AspNetCore.SignalR;
using Ron.SignalRLesson2.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ron.SignalRLesson2.Services
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeChatHub : Hub
    {
        public static IServiceProvider ic;
        public Dictionary<string, List<string>> UserList { get; set; } = new Dictionary<string, List<string>>();

        public IDAL_OnlineClient Dal => IocManager.GetDAL();

        public void Send(ChatMessage body)
        {
            // Clients.All.SendAsync("Recv", body);
            Clients.All.SendAsync("ReceiveMessage", body.UserName, body.Content);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddToGroupAsync(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroupAsync(string groupName)
        {
            await Groups.RemoveFromGroupAsync(this.Context.ConnectionId, groupName);
        }

        public async Task SendToGroupAsync(string groupName, ChatMessage message)
        {
            await Clients.Group(groupName).SendAsync(groupName, new object[] { message });
        }


        public override Task OnConnectedAsync()
        {


            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;



            if (!UserList.ContainsKey(userName))
            {
                UserList[userName] = new List<string>
                {
                    connectionId
                };
            }
            else if (!UserList[userName].Contains(connectionId))
            {
                UserList[userName].Add(connectionId);
            }

            Dal.SaveAsync(connectionId, userName);
            Console.WriteLine("哇，有人进来了：{0},{1},{2}", this.Context.UserIdentifier, this.Context.User.Identity.Name, this.Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userName = this.Context.User.Identity.Name;
            var connectionId = this.Context.ConnectionId;
            if (UserList.ContainsKey(userName))
            {
                if (UserList[userName].Contains(connectionId))
                {
                    UserList[userName].Remove(connectionId);
                }
            }

            Dal.DeleteAsync(connectionId);
            Console.WriteLine("靠，有人跑路了：{0}", this.Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        readonly List<string> blackList = new List<string>();

        public async Task OtherSendAsync(ChatMessage body)
        {
            // 给当前连接到 Hub 上的所有连接发送消息，相当于广播
            await Clients.All.SendAsync("Recv", body);

            // 给当前连接对象发送消息
            await Clients.Caller.SendAsync("Recv", body);

            // 给其它所有连接的客户端发送消息，除了当前正在连接的客户端
            await Clients.Others.SendAsync("Recv", body);

            // 查找当前所有连接的客户端（排除自己），如果是已加入此分组，则给他们推送消息
            await Clients.OthersInGroup("groupName").SendAsync("Recv", body);

            // 给除了 blackList（黑名单）之外的所有人发送消息
            await Clients.AllExcept(blackList).SendAsync("Recv", body);
        }

        public void Stop()
        {
            if (UserList.Count > 0)
            {
                List<string> slist = new List<string>();
                foreach (var m in UserList)
                {
                    slist.AddRange(m.Value);
                }
            }
        }
    }
}
