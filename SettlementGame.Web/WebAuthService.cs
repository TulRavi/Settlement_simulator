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
        private readonly IConfiguration _configuration; //ASP.NET Core's IConfiguration will provide this automatically via DI
        //allows to use JWT key without saving it directly and openning from Program, like it was before

        public WebAuthService(
            GameDbContext dbContext,
            IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this._configuration = configuration;
        }

        public string Authenticate(LoginRequest request)
        {
            string jwtKey = _configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException(
                    "JWT key is not configured.");
            }

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
            
            //now save it in BD
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
                expires: DateTime.Now.AddHours(9),//9 hours - normal working day
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return jwt;
        }
    }
}
