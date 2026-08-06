using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SettlementGame.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SettlementGame.Web
{
    public class WebAuthService
    {
        
        private readonly GameDbContext _dbContext;
        

        public WebAuthService(GameDbContext dbContext)
        {
            this._dbContext = dbContext;//для считывания логина-пароля из БД
        }
        public string Authenticate(LoginRequest request)
        {
            
            string jwtKey = Program.JwtKey;

            UserEntity user = _dbContext.Users.FirstOrDefault(x => x.Email == request.Email);

            if (user == null)
            {
                
                return null;
            }

            bool passwordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!passwordCorrect)
            {
                return null;
            }
            //переходим на хранение в БД
            //if (request.Email == "Admin" && request.Password == "321")
            //{
            //return Ok("token321");
            var claims = new[]
                {
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Role, user.Role)            
            //new Claim(ClaimTypes.Role, "Admin")
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "game",
                audience: "game_client",
                claims: claims,
                expires: DateTime.Now.AddHours(9),//не будем выбивать человека каждые 4 часа
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return jwt;
        }
    }
}
