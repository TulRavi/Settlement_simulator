using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace SettlementGame.Web
{
    public class BasicAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock)
    : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization")) //читаем заголовки
            {
                return Task.FromResult(AuthenticateResult.Fail("No Authorization Header")); //Если нет Authorization - ошибка
            }
            var authHeader = Request.Headers["Authorization"].ToString();//смотрим что внутри заголовка запроса
            if (!authHeader.StartsWith("Basic "))//убеждаемся, что это Basic 
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Scheme"));
            }
            //блок декодирования из djfhsfh в admin123
            var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
            var decodedBytes = Convert.FromBase64String(encodedCredentials);
            var credentials = Encoding.UTF8.GetString(decodedBytes);
            //отделяем логин от пароля по двоеточию
            var parts = credentials.Split(':');
            var username = parts[0];
            var password = parts[1];

            //зашиваем пароль в код пока
            if (!((username == "Admin" && password == "123") || (username == "User" && password == "456")))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid login or password"));
            }
            //Создаем Claims (роли юзера и админа для начала)
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, username)
            };

            if (username == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else if(username == "User")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);//роль автора запроса
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);//пропуск
            return Task.FromResult(AuthenticateResult.Success(ticket));

        }
    }
}
