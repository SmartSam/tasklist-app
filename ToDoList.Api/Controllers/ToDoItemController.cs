using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Data;
using ToDoList.Shared.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoRepository _repository;
        private readonly ILogger<ToDoItemController> _logger;

        public ToDoItemController(IToDoRepository repository, ILogger<ToDoItemController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        
        [HttpGet]
        public IActionResult GetToDoItems()
        {
            try
            {
                var toDoItems = _repository.GetAll();
                if (!toDoItems.Any())
                    return NotFound("No items found");
                return Ok(toDoItems);
            }
            catch (Exception ex)
            {
                //log ex
                _logger.LogError($"ToDoItemController.GetToDoItems: {ex.Message} {ex?.InnerException} Stack Trace: {ex.StackTrace}");
                return BadRequest("Error retrieving items");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(long id)       {
            try
            {
                var item = await _repository.GetItem(id);
                if (item == null)
                    return NotFound("Item not found");
                return Ok(item);
            }
            catch(Exception ex)
            {
                //log ex
                _logger.LogError($"ToDoItemController.GetItem: {ex.Message} {ex?.InnerException} Stack Trace: {ex.StackTrace}");
                return BadRequest("Error retrieving item");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ToDoItem toDoItem)
        {
            try
            {
                var createdItem = await _repository.AddAsync(toDoItem);

                return Ok(createdItem);

            }
            catch(Exception ex)
            {
                //log exception
                _logger.LogError($"ToDoItemController.CreateItem: {ex.Message} {ex?.InnerException} Stack Trace: {ex.StackTrace}");
                return BadRequest("Item could not be created");
            }
  
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(ToDoItem toDoItem)
        {
            try
            {
                var updatedItem = await _repository.UpdateAsync(toDoItem);
                return Ok(updatedItem);
            }
            catch(Exception ex)
            {
                //log exception
                _logger.LogError($"ToDoItemController.UpdateItem: {ex.Message} {ex?.InnerException} Stack Trace: {ex.StackTrace}");
                return BadRequest("Item could not be saved");
            }
 
        }
    }
}
