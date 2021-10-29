using Adform.Todo.Api.SwaggerConfig;
using Adform.Todo.Dto;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Manager;
using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeriLogger.DbLogger;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Threading.Tasks;

namespace Adform.Todo.Api.Controllers
{
    [Route("login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IUserQueryManager userQueryManager,
            IUserCommandManager userCommandManager,
            IDbLogger logger,
            IJsonWebTokenHandler jsonWebTokenHandler,
            IMapper mapper)
        {
            _userQueryManager = userQueryManager;
            _logger = logger;
            _userCommandManager = userCommandManager;
            _jsonWebTokenHandler = jsonWebTokenHandler;
            _mapper = mapper;
        }

        private readonly IUserQueryManager _userQueryManager;
        private readonly IDbLogger _logger;
        private readonly IUserCommandManager _userCommandManager;
        private readonly IJsonWebTokenHandler _jsonWebTokenHandler;
        private readonly IMapper _mapper;

        // POST <LoginController>
        [HttpPost]
        [SwaggerRequestExample(typeof(AppUser), typeof(AppUserExample))]
        [ProducesResponseType(typeof(AutherizationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AutherizationResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AppUser appUser)
        {
            var userdata = _userQueryManager.ValidateUser(_mapper.Map<User>( appUser));
            if (userdata != null)
            {
                var token = _jsonWebTokenHandler.GenerateJSONWebToken(userdata.Id.ToString());
                return Ok(new AutherizationResponse()
                {
                    IsValidUser = true,
                    AuthToken = token,
                    Message = "Success"
                });
            }
            else
            {
                return BadRequest(new AutherizationResponse()
                {
                    IsValidUser = false,
                    AuthToken = string.Empty,
                    Message = "User name and Password are incorrect."
                });
            }
        }
        // POST <LoginController>/register
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostRegisterUser([FromBody] AppUser appUser)
        {
            try
            {
                var result = await _userCommandManager.Add(_mapper.Map<User>(appUser));
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponse() { Status = true, Message = "User not registered" });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return BadRequest(new ApiResponse() { Status = false, Message = ex.Message });
            }
        }
    }
}
