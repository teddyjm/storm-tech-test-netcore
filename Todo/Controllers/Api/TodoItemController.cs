using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Models.Api;
using Todo.Models.TodoItems;
using Todo.Services;

namespace Todo.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TodoItemController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPut("Create")]
        public async Task<IActionResult> Create([FromBody] TodoItemCreateFields fields)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var item = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance);

            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("UpdateRank")]
        public async Task<IActionResult> UpdateRank([FromBody] UpdateRankRequest updateRankRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var todoItem = dbContext.SingleTodoItem(updateRankRequest.TodoItemId);
            todoItem.Rank = updateRankRequest.Rank;

            dbContext.Update(todoItem);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}