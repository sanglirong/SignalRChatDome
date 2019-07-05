using Microsoft.AspNetCore.Http;
using Ron.SignalRLesson2.Auth;
using System;
using System.Threading.Tasks;

namespace Ron.SignalRLesson2.Middleware
{
    /// <summary>
    /// 权限中间件
    /// </summary>
    public sealed class JwtBearerMiddleware
    {
        /// <summary>
        /// 管道代理对象
        /// </summary>
        private RequestDelegate _next { get; }


        /// <summary>
        /// 权限中间件构造
        /// </summary>
        /// <param name="next">管道代理对象</param>
        /// <param name="option">权限中间件配置选项</param>
        public JwtBearerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public void Invoke(HttpContext context)
        {
            //是否经过验证
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                if (context.Request.Headers.Keys.Contains("Authorization"))
                {
                    var jwt = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                    var clm = JwtBearerAuthentication.GetPrincipal(jwt);
                    if (clm != null) context.User = clm;
                }
            }
            _next(context);
        }
    }
}
