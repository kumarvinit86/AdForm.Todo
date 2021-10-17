using Adform.Todo.Dto;
using Adform.Todo.Manager;
using Adform.Todo.Model.Models;
using Adform.Todo.Wireup.Authentication;
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
    [Route("todo/todolist")]
    [ApiController]
    public class TodoListController : ControllerBase
    {

        public TodoListController(ITodoListQueryManager todoListQueryManager, 
            ITodoListCommandManager todoLsitCommandManager, 
            IDbLogger logger,
            IJsonWebTokenHandler jsonWebTokenHandler)
        {
            _todoListQueryManager = todoListQueryManager;
            _todoListCommandManager = todoLsitCommandManager;
            _jsonWebTokenHandler = jsonWebTokenHandler;
            _logger = logger;
        }

        private readonly ITodoListQueryManager _todoListQueryManager;
        private readonly ITodoListCommandManager _todoListCommandManager;
        private readonly IJsonWebTokenHandler _jsonWebTokenHandler;
        private readonly IDbLogger _logger;

        // GET: todo/<TodoListController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Tuple<List<ItemList>, PagingDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] PagingDataRequest pagingDataRequest)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is reuired" });
                }
                var tupleResult = await _todoListQueryManager.Get(pagingDataRequest, userId ?? default);
                List<ItemList> result = tupleResult.Item1;
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

        // GET todo/<TodoListController>/getbyid/5
        [HttpGet("getbyid/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {
                var result = await _todoListQueryManager.GetbyId(id);
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

        // POST todo/<TodoListController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ItemList itemList)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is reuired" });
                }
                itemList.UserId = userId ?? default;
                var result = await _todoListCommandManager.Add(itemList);
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

        // Patch todo/<TodoListController>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch([FromBody] ItemList itemList)
        {
            try
            {
                var result = await _todoListCommandManager.Update(itemList);
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

        // Patch todo/<TodoItemController>/updatelabeltoitemlist
        [HttpPatch("updatelabeltoitemlist")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchUpdatelabel(int itemId, int labelId)
        {
            try
            {
                var result = await _todoListCommandManager.Updatelabel(itemId, labelId);
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
        // DELETE todo/<TodoListController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _todoListCommandManager.DeletebyId(id);
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
