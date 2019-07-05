using Invio.Extensions.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ron.SignalRLesson2.Auth
{
    public static class JwtBearerAuthentication
    {
        static readonly byte[] skey = Encoding.ASCII.GetBytes("123456789987654321");

        /// <summary>
        /// 注册JWT Bearer认证服务的静态扩展方法
        /// </summary>
        /// <param name="services"></param>
        public static void AddJwtBearerAuthentication(this IServiceCollection services)
        {
            //使用应用密钥得到一个加密密钥字节数组
            //services
            //    .AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(skey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chathub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            })
            .AddJwtBearerQueryStringAuthentication(
                (options) =>
                {
                    options.QueryStringParameterName = "access_token";
                    options.QueryStringBehavior = QueryStringBehaviors.Redact;
                });
        }

        public static string BuildJwtToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(skey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public static ClaimsPrincipal ValidateJwtToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Parse JWT from the Base64UrlEncoded wire form 
            //(<Base64UrlEncoded header>.<Base64UrlEncoded body>.<signature>)
            var parsedJwt = tokenHandler.ReadJwtToken(jwtToken) as SecurityToken;

            TokenValidationParameters validationParams =
                 new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(skey),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                 };

            return tokenHandler.ValidateToken(jwtToken, validationParams, out parsedJwt);
        }

        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public static string GenerateToken(string username, int expireMinutes = 120)
        { // 此方法用来生成 Token 
            var symmetricKey = Convert.FromBase64String(Secret);  // 生成二进制字节数组
            var tokenHandler = new JwtSecurityTokenHandler(); // 创建一个JwtSecurityTokenHandler类用来生成Token
            var now = DateTime.UtcNow; // 获取当前时间
            var tokenDescriptor = new SecurityTokenDescriptor // 创建一个 Token 的原始对象
            {
                Subject = new ClaimsIdentity(new[] // Token的身份证，类似一个人可以有身份证，户口本
                        {
                            new Claim(ClaimTypes.Name, username) // 可以创建多个
                        }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)), // Token 有效期

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256)
                // 生成一个Token证书，第一个参数是根据预先的二进制字节数组生成一个安全秘钥，说白了就是密码，第二个参数是编码方式
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor); // 生成一个编码后的token对象实例
            var token = tokenHandler.WriteToken(stoken); // 生成token字符串，给前端使用
            return token;
        }

        /// <summary>
        /// 获取用户身份
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ClaimsPrincipal GetPrincipal(this HttpContext context, string token)
        {
            // 此方法用解码字符串token，并返回秘钥的信息对象
            try
            {
                token = token.Replace("Bearer ", string.Empty);

                var tokenHandler = new JwtSecurityTokenHandler(); // 创建一个JwtSecurityTokenHandler类，用来后续操作
                var jwtToken = tokenHandler.ReadToken(token); // 将字符串token解码成token对象
                if (jwtToken == null) return null;
                var symmetricKey = Convert.FromBase64String(Secret); // 生成编码对应的字节数组
                var validationParameters = new TokenValidationParameters() // 生成验证token的参数
                {
                    RequireExpirationTime = true, // token是否包含有效期
                    ValidateIssuer = false, // 验证秘钥发行人，如果要验证在这里指定发行人字符串即可
                    ValidateAudience = false, // 验证秘钥的接受人，如果要验证在这里提供接收人字符串即可
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey) // 生成token时的安全秘钥
                };
                // 接受解码后的token对象
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
                context.User = principal;
                return principal; // 返回秘钥的主体对象，包含秘钥的所有相关信息
            }

            catch (Exception)
            {
                return null;
            }
        }


        public static ClaimsPrincipal GetPrincipal2(string token)
        {
            try
            {
                var symmetricKey = Convert.FromBase64String(Secret);
                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var handler = new JwtSecurityTokenHandler();
                handler.InboundClaimTypeMap.Clear();

                SecurityToken securityToken;
                var principal = handler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }


}
