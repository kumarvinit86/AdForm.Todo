using Adform.Todo.Dto;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Manager;
using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SeriLogger.DbLogger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Adform.Todo.Api.Controllers
{
    [Authorize]
    [Route("todoitem")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        public TodoItemController(ITodoItemQueryManager todoItemQueryManager,
            ITodoItemCommandManager todoItemCommandManager,
            IDbLogger logger,
            IJsonWebTokenHandler jsonWebTokenHandler,
            IMapper mapper)
        {
            _todoItemQueryManager = todoItemQueryManager;
            _todoItemCommandManager = todoItemCommandManager;
            _logger = logger;
            _jsonWebTokenHandler = jsonWebTokenHandler;
            _mapper = mapper;
        }

        private readonly ITodoItemQueryManager _todoItemQueryManager;
        private readonly ITodoItemCommandManager _todoItemCommandManager;
        private readonly IJsonWebTokenHandler _jsonWebTokenHandler;
        private readonly IDbLogger _logger;
        private readonly IMapper _mapper;

        // GET: <TodoItemController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ItemPaged<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] PagingDataRequest pagingDataRequest)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is required" });
                }
                var result = await _todoItemQueryManager.Get(pagingDataRequest, userId ?? default);
                if (result.Count > 0)
                {
                    var pageData = new ItemPaged<Item>
                    {
                        data = _mapper.Map<List<Item>>(result),
                        pagingData = _todoItemQueryManager.pagingResponse
                    };
                    return Ok(pageData);
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

        // GET <TodoItemController>/5
        [HttpGet("getbyid/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is required" });
                }
                var result = await _todoItemQueryManager.GetbyId(id, userId ?? default);
                if (result != null)
                {
                    return Ok(_mapper.Map<Item>(result));
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

        // POST <TodoItemController>
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
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is required" });
                }
                item.UserId = userId ?? default;
                var result = await _todoItemCommandManager.Add(_mapper.Map<TodoItem>(item));
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

        // Put <TodoItemController>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] Item item)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is required" });
                }
                item.UserId = userId ?? default;
                var result = await _todoItemCommandManager.Update(_mapper.Map<TodoItem>(item));

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

        // Patch <TodoItemController>.
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Item> patchDoc)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is required" });
                }
                var item = _mapper.Map<Item>(await _todoItemQueryManager.GetbyId(id, userId ?? default));
                if (item == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "No record found for update." });
                }
                patchDoc.ApplyTo(item, ModelState);
                item.Id = id;
                item.UserId = userId ?? default;
                var result = await _todoItemCommandManager.Update(_mapper.Map<TodoItem>(item));
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponse() { Status = true, Message = "Record Not updated." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return BadRequest(new ApiResponse() { Status = false, Message = ex.Message });
            }
        }

        // Put <TodoItemController>
        [HttpPut("putupdatelable")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUpdateLable(int itemId, int lableId)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is required" });
                }
                var result = await _todoItemCommandManager.Updatelabel(itemId, lableId, userId ?? default);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponse() { Status = true, Message = "Record Not Updated." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return BadRequest(new ApiResponse() { Status = false, Message = ex.Message });
            }
        }

        // Put <TodoItemController>
        [HttpPut("putupdatelist")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUpdateList(int itemId, int listId)
        {
            try
            {
                var userId = _jsonWebTokenHandler.GetUserIdfromToken(HttpContext.Request.Headers["Authorization"].ToString());
                if (userId == null)
                {
                    return BadRequest(new ApiResponse() { Status = false, Message = "User Id is required" });
                }
                var result = await _todoItemCommandManager.UpdateList(itemId, listId, userId ?? default);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponse() { Status = true, Message = "Record Not Updated." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return BadRequest(new ApiResponse() { Status = false, Message = ex.Message });
            }
        }

        // DELETE <TodoItemController>/5
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
                var result = await _todoItemCommandManager.DeletebyId(id, userId ?? default);
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
