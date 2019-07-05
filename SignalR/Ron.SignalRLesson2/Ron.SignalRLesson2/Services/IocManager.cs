using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ron.SignalRLesson2.Services
{
    /// <summary>
    /// 容器管理
    /// </summary>
    public class IocManager
    {

        public static IServiceProvider Ic { get; set; }

        public static IDAL_OnlineClient GetDAL() => Ic.GetService<IDAL_OnlineClient>();

    }
}
