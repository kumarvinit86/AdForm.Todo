using Adform.Todo.Dto;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Manager;
using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeriLogger.DbLogger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Api.Controllers
{
    [Authorize]
    [Route("todolabel")]
    [ApiController]
    public class TodoLabelController : ControllerBase
    {
        public TodoLabelController(ILabelQueryManager labelQueryManager,
            ILabelCommandManager labelCommandManager,
            IDbLogger logger,
             IJsonWebTokenHandler jsonWebTokenHandler,
             IMapper mapper)
        {
            _labelQueryManager = labelQueryManager;
            _labelCommandManager = labelCommandManager;
            _logger = logger;
            _jsonWebTokenHandler = jsonWebTokenHandler;
            _mapper = mapper;
        }

        private readonly ILabelQueryManager _labelQueryManager;
        private readonly ILabelCommandManager _labelCommandManager;
        private readonly IJsonWebTokenHandler _jsonWebTokenHandler;
        private readonly IDbLogger _logger;
        private readonly IMapper _mapper;

        // GET: <TodolabelController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<Label>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _mapper.Map<List<Label>>(await _labelQueryManager.Get());
                if (result.Count > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponse() { Status = true, Message = "Result Not found." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return BadRequest(new ApiResponse() { Status = false, Message = ex.Message });
            }

        }

        // GET <TodolabelController>/5
        [HttpGet("getbyid/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {
                var result = await _labelQueryManager.GetbyId(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponse() { Status = true, Message = "Result Not found." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return BadRequest(new ApiResponse() { Status = false, Message = ex.Message });
            }
        }

        // POST <TodolabelController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Label label)
        {
            try
            {
                var result = await _labelCommandManager.Add(_mapper.Map<TodoLabel>(label));
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponse() { Status = true, Message = "Result Not found." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return BadRequest(new ApiResponse() { Status = false, Message = ex.Message });
            }
        }



        // DELETE <TodolabelController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is required" });
                }
                var result = await _labelCommandManager.DeletebyId(id, userId ?? default);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponse() { Status = true, Message = "Result Not found." });
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
