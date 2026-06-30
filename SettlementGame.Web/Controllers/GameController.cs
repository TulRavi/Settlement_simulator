using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly DataWorld _world;
        private readonly WorldService _worldService;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly GameDbContext _dbContext;
        private string JwtKey { get; set; }
        public GameController(DataWorld world, WorldService worldService, IHostApplicationLifetime lifetime, GameDbContext dbContext)
        {
            this._world = world;
            this._worldService = worldService;
            this._lifetime = lifetime; // для завершения раьоты сервера ASP
            this._dbContext = dbContext;//для считывания логина-пароля из БД
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
        [HttpGet("getSettlementresourcesList")]
        public IActionResult getSettlementresourcesList()
        {
            
            //List <SettlementGame.Domain.AnyResource> recivedSettlementresourcesList=_worldService.GetSettlementResourceList();
            return Ok(_worldService.GetSettlementResourceList());
        }

        [HttpGet("getPeopleLoayliy")]
        public IActionResult getPeopleLoayliy()
        {
            //List <SettlementGame.Domain.AnyResource> recivedSettlementresourcesList=_worldService.GetSettlementResourceList();
            return Ok(_worldService.GetPeopleLoyality());
        }



        [HttpGet("getCrownLoayliy")]
        public IActionResult getCrownLoayliy()
        {
            //List <SettlementGame.Domain.AnyResource> recivedSettlementresourcesList=_worldService.GetSettlementResourceList();
            return Ok(_worldService.GetCrownLoyality());
        }

        [HttpGet("getCurrentCrownTask")]
        public IActionResult getCurrentCrownTask()
        {
            //int temp = _world.GetHashCode();
            //List <SettlementGame.Domain.AnyResource> recivedSettlementresourcesList=_worldService.GetSettlementResourceList();

            //CrownTask crownTask = _worldService.GetCurrentCrownTask();
            //return Ok(crownTask);
            return Ok(_worldService.GetCurrentCrownTaskDto());

            //var task = _worldService.GetCurrentCrownTask();

            //return Ok(new
            //{
            //    Exists = task != null
            //});
        }

        [HttpGet("getCurrentGameState")]
        public IActionResult getCurrentGameState()
        {
            return Ok(_worldService.updateGameState(_world));

        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        //public IActionResult CreateWorld([FromBody]int numberOfWorkers)
        public IActionResult CreateWorld()
        {
            //foreach (var h in Request.Headers)
            //{
            //    Console.WriteLine($"{h.Key}: {h.Value}");
            //}

            var auth = Request.Headers.Authorization.ToString();
            //return Ok(world);
            _worldService.CreateWorld();
            
            //worldService.CreateWorkersByService(numberOfWorkers);
            int temp=_world.GetHashCode();
            return Ok(new
            {
                
                Workers = _worldService.GetWorkerDtoList(),
                Buildings = _worldService.GetBuildingDtoList(),

               
            });
        }



        [HttpPut("tick")]
        [Authorize]
        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Tick()
        {
            //int temp = _world.GetHashCode();
            try
            {
                _worldService.Tick();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
            try {  
            getCurrentCrownTask();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
            try{
                getCurrentGameState();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
            return Ok();
        }

        

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            JwtKey = Program.JwtKey;

            UserEntity user =_dbContext.Users.FirstOrDefault(x => x.Email == request.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            bool passwordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!passwordCorrect)
            {
                return Unauthorized();
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

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "game",
                    audience: "game_client",
                    claims: claims,
                    expires: DateTime.Now.AddHours(9),//не будем выбивать человека каждые 4 часа
                    signingCredentials: creds
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                
                return Ok(jwt);
            }
            //if (request.Email == "User" && request.Password == "123")
            //{
            //    //return Ok("token321");
            //    var claims1 = new[]
            //    {
            //new Claim(ClaimTypes.Email, request.Email),
            //new Claim(ClaimTypes.Role, "User"),

            //    };
            //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
            //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //    var token = new JwtSecurityToken(
            //        claims: claims1,
            //        expires: DateTime.Now.AddHours(9),//8+обед 
            //        signingCredentials: creds
            //    );

            //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            //    return Ok(jwt);
                //if (request.Email == "user" && request.Password == "123")
                //{
                //    return Ok("token123");
                //}
            
        }

    }
    


