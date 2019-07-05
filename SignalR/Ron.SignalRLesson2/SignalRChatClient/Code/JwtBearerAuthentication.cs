using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalRChatClient
{
    public static class JwtBearerAuthentication
    {
        static readonly byte[] skey = Encoding.ASCII.GetBytes("123456789987654321");

        public static string GetJwtAccessToken(ClaimsIdentity claimsIdentity)
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

        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public static string BuildJwtToken(string username, int expireMinutes = 120)
        {
            // 此方法用来生成 Token 
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
    }
}

