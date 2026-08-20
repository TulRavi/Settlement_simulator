using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SettlementGame.Domain;

namespace SettlementGame.Web.Controllers
{
    [ApiController]
    [Route("api/world")]
    public class GameController : ControllerBase
    {
        private readonly DataWorld _world;
        private readonly WorldService _worldService;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly WebAuthService _webAuthService;

        public GameController(
            DataWorld world,
            WorldService worldService,
            IHostApplicationLifetime lifetime,
            WebAuthService webAuthService)
        {
            this._world = world;
            this._worldService = worldService;
            this._lifetime = lifetime; //Used to control the lifetime of the ASP.NET Core application.
            this._webAuthService = webAuthService;
        }

        [HttpGet("GetSettlementresourcesListDto")]
        public IActionResult GetSettlementresourcesListDto()
        {
            return Ok(_worldService.GetSettlementResourceListDto());
        }

        [HttpGet("GetPeopleLoayliyDto")]
        public IActionResult GetPeopleLoayliyDto()
        {
            return Ok(_worldService.GetPeopleLoyaltyDto());
        }

        [HttpGet("GetCrownLoyalityDto")]
        public IActionResult GetCrownLoyalityDto()
        {
            return Ok(_worldService.GetCrownLoyaltyDto());
        }

        [HttpGet("GetCurrentCrownTaskDto")]
        public IActionResult GetCurrentCrownTaskDto()
        {
            return Ok(_worldService.GetCurrentCrownTaskDto());
        }

        [HttpGet("GetCurrentGameState")]
        public IActionResult GetCurrentGameState()
        {
            return Ok(_worldService.updateGameState(_world));
        }

        [HttpPost("Create")]
        [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Admin")]
        public IActionResult CreateWorld()
        {
            //The [Authorize] attribute requires the request to contain a valid JWT.
            //The Roles parameter additionally requires the authenticated user to have the "Admin" role.
            _worldService.CreateWorld();

            return Ok(new
            {
                Workers = _worldService.GetWorkerDtoList(),
                Buildings = _worldService.GetBuildingDtoList()
            });
        }

        [HttpPut("Tick")]
        [Authorize]
        public IActionResult Tick()
        {
            //[Authorize] requires an authenticated user, but does not restrict the request to a specific role.
            try
            {
                _worldService.Tick();
                return Ok();
            }
            catch (Exception ex)
            {
                //HTTP 500 means that an unexpected error occurred while processing the request.
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest request)
        {
            //Login is not protected by [Authorize], because a user must be able to
            //authenticate before they have a JWT.
            string jwt = _webAuthService.Authenticate(request);

            if (jwt == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(jwt);
            }
        }
    }
}