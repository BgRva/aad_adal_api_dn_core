using System.Collections.Generic;
using System.Linq;
using AADx.Common.Models;
using AADx.TodoApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AADx.TodoApi.Controllers
{
  [Authorize]
  [Produces("application/json")]
  [Route("api/Todo")]
  public class TodoController : Controller
  {
    private readonly TodoContext _context;
    private readonly ILogger _logger;

    public TodoController(TodoContext context, ILogger<TodoController> logger)
    {
      _context = context;
      _logger = logger;

      _logger.LogDebug("Creating initial todo item ...");
      if (!_context.TodoItems.Any())
      {
        _context.TodoItems.Add(new TodoItem
        {
          Description = "Item1",
          Owner = "Barney Rubble",
          Team = TeamType.WaterBuffaloes,
          Faction = FactionType.Horde
        });
        _context.SaveChanges();
      }
    }

    // GET: api/Todo
    [HttpGet]
    public IEnumerable<TodoItem> Get()
    {
      _logger.LogDebug("returning all ...");
      return _context.TodoItems.ToList();
    }

    // GET: api/Todo/5
    [HttpGet("{id}", Name = "GetTodo")]
    public IActionResult Get(int id)
    {
      _logger.LogDebug("Getting todo {0}", id.ToString());
      var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
      if (item == null)
        return NotFound();
      return new ObjectResult(item);
    }

    // POST: api/Todo
    [HttpPost]
    public IActionResult Post([FromBody] TodoItem item)
    {
      if (item == null)
      {
        _logger.LogDebug("Item is null");
        return BadRequest();
      }

      var validator = new ItemValidator();
      if (!validator.isValid(item))
      {
        _logger.LogDebug("Item is not valid");
        return BadRequest("One or more properties is not valid");
      }

      _context.TodoItems.Add(item);
      _context.SaveChanges();
      _logger.LogDebug(string.Format("Created item with id {0}", item.Id.ToString()));

      return CreatedAtRoute("GetTodo", new {id = item.Id}, item);
    }

    // PUT: api/Todo/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] TodoItem item)
    {
      if (item == null || item.Id != id)
        return BadRequest();

      var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
      if (todo == null)
        return NotFound();

      if (string.IsNullOrWhiteSpace(todo.Description))
        return BadRequest("One or more properties is not valid");

      _logger.LogDebug(string.Format("Updating item with id {0}", item.Id.ToString()));

      // only update desc
      todo.Description = item.Description;

      _context.TodoItems.Update(todo);
      _context.SaveChanges();
      return Ok(todo);
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
      if (todo == null)
        return NotFound();

      _logger.LogDebug(string.Format("Deleting item with id {0}", todo.Id.ToString()));

      _context.TodoItems.Remove(todo);
      _context.SaveChanges();
      return Ok();
    }
  }
}
