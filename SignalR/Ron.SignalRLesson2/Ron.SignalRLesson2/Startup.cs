using Invio.Extensions.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Ron.SignalRLesson2.Auth;
using Ron.SignalRLesson2.Middleware;
using Ron.SignalRLesson2.Services;
using StackExchange.Redis;
using System;

namespace Ron.SignalRLesson2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddJwtBearerAuthentication();


            //services.AddAuthentication(auth =>
            //{
            //    auth.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    auth.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    auth.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddCookie(cookie =>
            //{
            //    cookie.LoginPath = "/Home";
            //    cookie.Cookie.Name = "ASPNETCORE_SESSION_ID";
            //    cookie.Cookie.Path = "/";
            //    //cookie.Cookie.HttpOnly = true;
            //    cookie.Cookie.Expiration = TimeSpan.FromMinutes(20);

            //});

            //数据库
            services.AddDbContext<WeChatContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));


            //依赖注入
            services.AddScoped<IDAL_OnlineClient, DAL_OnlieClient>();

            services.AddSingleton<WeChatHub>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSignalR()
            .AddStackExchangeRedis(options =>
             {
                 options.Configuration.ChannelPrefix = "MyApp";

                 options.ConnectionFactory = async writer =>
                 {
                     var config = new ConfigurationOptions
                     {
                         AbortOnConnectFail = false
                     };
                     config.EndPoints.Add("192.168.0.45");
                     // config.EndPoints.Add(IPAddress.Loopback, 0);

                     config.SetDefaultPorts();

                     var connection = await ConnectionMultiplexer.ConnectAsync(config, writer);
                     connection.ConnectionFailed += (_, e) =>
                     {
                         Console.WriteLine("Connection to Redis failed.");
                     };

                     if (!connection.IsConnected)
                     {
                         Console.WriteLine("Did not connect to Redis.");
                     }
                     return connection;
                 };
             });
            IocManager.Ic = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            #region 生命周期
            appLifetime.ApplicationStopping.Register(() =>
            {
                var chatHub = app.ApplicationServices.GetService<WeChatHub>();
                chatHub.Stop();
            });
            #endregion

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            //app.UseMiddleware<JwtBearerMiddleware>();
            //app.UseJwtBearerQueryString();

            app.Use(async (context, next) =>
            {
                //是否经过验证
                var isAuthenticated = context.User.Identity.IsAuthenticated;
                if (!isAuthenticated && context.Request != null && context.Request.Headers != null)
                {
                    if (context.Request.Headers.TryGetValue("Authorization", out StringValues jwt))
                    {
                        var clm = context.GetPrincipal(jwt.ToString());
                    }

                    //if (context.Request.Headers.Keys.Contains("Authorization"))
                    //{
                    //    var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                    //    var clm = JwtBearerAuthentication.GetPrincipal(jwtToken);
                    //    if (clm != null) context.User = clm;
                    //}
                }
                await next();
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<WeChatHub>("/wechatHub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
