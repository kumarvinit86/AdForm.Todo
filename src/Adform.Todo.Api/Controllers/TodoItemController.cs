using Adform.Todo.Dto;
using Adform.Todo.Manager;
using Adform.Todo.Model.Models;
using Adform.Todo.Wireup.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeriLogger.DbLogger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Adform.Todo.Api.Controllers
{
    [Authorize]
    [Route("todo/todoitem")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        public TodoItemController(ITodoItemQueryManager todoItemQueryManager,
            ITodoItemCommandManager todoItemCommandManager,
            IDbLogger logger,
            IJsonWebTokenHandler jsonWebTokenHandler)
        {
            _todoItemQueryManager = todoItemQueryManager;
            _todoItemCommandManager = todoItemCommandManager;
            _logger = logger;
            _jsonWebTokenHandler = jsonWebTokenHandler;
        }

        private readonly ITodoItemQueryManager _todoItemQueryManager;
        private readonly ITodoItemCommandManager _todoItemCommandManager;
        private readonly IJsonWebTokenHandler _jsonWebTokenHandler;
        private readonly IDbLogger _logger;

        // GET: todo/<TodoItemController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Tuple<List<Item>, PagingDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] PagingDataRequest pagingDataRequest)
        {
            try
            {
                var userId= _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is reuired" });
                }
                var tupleResult = await _todoItemQueryManager.Get(pagingDataRequest,userId ?? default);
                var result= tupleResult.Item1;
                if (result.Count > 0)
                {
                    return Ok(tupleResult);
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

        // GET todo/<TodoItemController>/5
        [HttpGet("getbyid/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {
                var result = await _todoItemQueryManager.GetbyId(id);
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

        // POST todo/<TodoItemController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Item item)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is reuired" });
                }
                item.UserId = userId ?? default;
                var result = await _todoItemCommandManager.Add(item);
                if (result >0)
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

        // Patch todo/<TodoItemController>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch([FromBody] Item item)
        {
            try
            {
                var result = await _todoItemCommandManager.Update(item);
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

        // Patch todo/<TodoItemController>
        [HttpPatch("updatelabeltoitem")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchUpdatelabel(int itemId, int labelId)
        {
            try
            {
                var result = await _todoItemCommandManager.Updatelabel(itemId,labelId);
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

        // DELETE todo/<TodoItemController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _todoItemCommandManager.DeletebyId(id);
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
