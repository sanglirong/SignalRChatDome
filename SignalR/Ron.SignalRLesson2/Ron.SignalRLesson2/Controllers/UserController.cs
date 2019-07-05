using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Ron.SignalRLesson2.Models;
using Ron.SignalRLesson2.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ron.SignalRLesson2.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly WeChatHub chatHub;
        private readonly IDAL_OnlineClient _OnlineClient;
        public UserController(WeChatHub chatHub, IDAL_OnlineClient OnlineClient)
        {
            this.chatHub = chatHub;
            _OnlineClient = OnlineClient;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] UserViewModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.Role, "User")
            };
            var ids = new ClaimsIdentity("style");
            ids.AddClaims(claims);

            var principal = new ClaimsPrincipal(ids);
            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Chat", "Home");
        }

        [Authorize(Roles = "User")]
        [HttpPost("SendToUser")]
        public async Task<IActionResult> SendToUser([FromBody] UserInfoViewModel model)
        {
            var user = HttpContext.User;
            var message = new ChatMessage()
            {
                Type = 1,
                Content = model.Content,
                UserName = user.Identity.Name //model.UserName
            };

            //if (this.chatHub.UserList.ContainsKey(model.UserName))
            //{
            //    var connections = this.chatHub.UserList[model.UserName].First();
            //    await chatHub.Clients.Client(connections).SendAsync("Recv", message);
            //}

            var ulist = _OnlineClient.GetListByName(model.UserName);
            if (ulist.Count > 0)
            {
                foreach (var m in ulist)
                {
                    await chatHub.Clients.Client(m.ConnectionId).SendAsync("Recv", message);
                }
            }
            return Json(new { Code = 0 });
        }



        [Authorize(Roles = "User")]
        [HttpPost("Group-Join")]
        public async Task<IActionResult> Join([FromBody] GroupViewModel model)
        {
            await chatHub.AddToGroupAsync(model.Name);

            return Json(new { Code = 0 });
        }

        [Authorize(Roles = "User")]
        [HttpPost("Group-Leave")]
        public async Task<IActionResult> Leave([FromBody] GroupViewModel model)
        {
            await chatHub.RemoveFromGroupAsync(model.Name);

            return Json(new { Code = 0 });
        }

        [Authorize(Roles = "User")]
        [HttpPost("SendToGroup")]
        public async Task<IActionResult> SendToGroup([FromBody] GroupChatMessage model)
        {
            ChatMessage message = new ChatMessage()
            {
                Type = 1,
                Content = model.Content,
                UserName = User.Identity.Name
            };
            await this.chatHub.SendToGroupAsync(model.GroupName, message);

            return Json(new { Code = 0 });
        }
    }
}
