using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Data;
using ToDoList.Shared.Models;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoRepository _repository;
        //TODO: inject ILogger
        public ToDoItemController(IToDoRepository repository)
        {
            this._repository = repository;
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
                return BadRequest("Error retrieving items");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
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
                return BadRequest("Item could not be saved");
            }
 
        }
    }
}
