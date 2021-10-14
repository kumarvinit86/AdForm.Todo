using Adform.Todo.Dto;
using Adform.Todo.Manager;
using Adform.Todo.Model.Models;
using Adform.Todo.Service.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeriLogger.DbLogger;

namespace Adform.Todo.Service.Controllers
{
    [Route("todo/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController(IUserQueryManager userQueryManager, IDbLogger dbLogger, IConfiguration configuration)
        {
            _userQueryManager = userQueryManager;
            _dbLogger = dbLogger;
            _configuration = configuration;
        }

        private readonly IUserQueryManager _userQueryManager;
        private readonly IDbLogger _dbLogger;
        private readonly IConfiguration _configuration;

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] AppUser appUser)
        {
            var userdata = _userQueryManager.ValidateUser(appUser);
            if(userdata!=null)
            {
                var token = new JsonWebTokenHandler(_configuration)
                    .GenerateJSONWebToken(userdata.Id.ToString(),true);
                return Ok(new AutherizationResponse()
                {
                    IsValidUser = true,
                    AuthToken = token,
                    Message = "Success"
                });
            }
            else
            {
               return Unauthorized();            
            }
        }     
    }
}
