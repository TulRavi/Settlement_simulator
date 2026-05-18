using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SettlementGame.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SettlementGame.Web.Controllers
{
    [ApiController]

    [Route("api/world")]

    
    public class GameController : ControllerBase
    {
        private readonly DataWorld world;
        private readonly WorldService worldService;
        private string JwtKey { get; set; }
        public GameController(DataWorld world, WorldService worldService)
        {
            this.world = world;
            this.worldService = worldService;
        }
        //это пока уберем за ненадобностью
        //[HttpGet]
        //public ActionResult GetTime()
        //{
        //    //string gameAndWorkingTime = $"\"gameTime\":{world.GameTime}, \n \"workingHours\":{world.startWorkingDay}-{world.endWorkingDay}";
        //    var response = new
        //    {
        //        gameTime = world.GameTime,
        //        workingHours = $"{world.startWorkingDay}-{world.endWorkingDay}"
        //    };
        //    return Ok(response);
        //}

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult CreateWorld([FromBody]int numberOfWorkers)
        {
            //foreach (var h in Request.Headers)
            //{
            //    Console.WriteLine($"{h.Key}: {h.Value}");
            //}

            var auth = Request.Headers.Authorization.ToString();
            //return Ok(world);
            worldService.CreateWorld();
            //worldService.CreateWorkersByService(numberOfWorkers);

            return Ok(new
            {

                Workers = worldService.GetWorkerDtoList(),
                Buildings = worldService.GetBuildingDtoList()
            });
        }



        [HttpPost("tick")]
        [Authorize]
        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Tick()
        {
            worldService.Tick(world);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            JwtKey = Program.JwtKey;
            if (request.Email == "Admin" && request.Password == "321")
            {
                //return Ok("token321");
                var claims = new[]
        {
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "game",
                    audience: "game_client",
                    claims: claims,
                    expires: DateTime.Now.AddHours(9),
                    signingCredentials: creds
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                
                return Ok(jwt);
            }
            if (request.Email == "User" && request.Password == "123")
            {
                //return Ok("token321");
                var claims1 = new[]
                {
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Role, "User"),

                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims1,
                    expires: DateTime.Now.AddHours(9),//8+обед 
                    signingCredentials: creds
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(jwt);
                //if (request.Email == "user" && request.Password == "123")
                //{
                //    return Ok("token123");
                //}
            }
                return Unauthorized();
        }

    }
    

}
