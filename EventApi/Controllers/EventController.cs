using System.Collections.Generic;
using System.Linq;
using AADx.Common.Models;
using AADx.EventApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AADx.EventApi.Controllers
{
  [Authorize]
  [Produces("application/json")]
  [Route("api/Event")]
  public class EventController : Controller
  {
    private readonly EventContext _context;
    private readonly ILogger _logger;

    public EventController(EventContext context, ILogger<EventController> logger)
    {
      _context = context;
      _logger = logger;

      _logger.LogDebug("Creating first event");
      if (!_context.EventItems.Any())
      {
        _context.EventItems.Add(new EventItem
        {
          Description = "Item1",
          Owner = "Barney Rubble",
          Team = TeamType.WaterBuffaloes,
          Faction = FactionType.Horde
        });
        _context.SaveChanges();
      }
    }

    [AllowAnonymous]
    // GET: api[/Event
    [HttpGet]
    public IEnumerable<EventItem> Get()
    {
      _logger.LogDebug("returning all ...");
      return _context.EventItems.ToList();
    }

    // GET: api/Event/5
    [HttpGet("{id}", Name = "GetEvent")]
    public IActionResult Get(int id)
    {
      _logger.LogDebug("Getting todo {0}", id.ToString());
      var item = _context.EventItems.FirstOrDefault(t => t.Id == id);
      if (item == null)
        return NotFound();
      return new ObjectResult(item);
    }

    // POST: api/Event
    [HttpPost]
    public IActionResult Post([FromBody] EventItem item)
    {
      if (item == null)
      {
        _logger.LogDebug("Item is null");
        return BadRequest();
      }

      var validator = new EventValidator();
      if (!validator.isValid(item))
      {
        _logger.LogDebug("Item is not valid");
        return BadRequest("One or more properties is not valid");
      }

      _context.EventItems.Add(item);
      _context.SaveChanges();
      _logger.LogDebug(string.Format("Created item with id {0}", item.Id.ToString()));

      return CreatedAtRoute("GetEvent", new {id = item.Id}, item);
    }

    // PUT: api/Event/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] EventItem item)
    {
      if (item == null || item.Id != id)
        return BadRequest();

      var todo = _context.EventItems.FirstOrDefault(t => t.Id == id);
      if (todo == null)
        return NotFound();


      if (string.IsNullOrWhiteSpace(todo.Description))
        return BadRequest("One or more properties is not valid");

      _logger.LogDebug(string.Format("Updating item with id {0}", item.Id.ToString()));

      // only update desc
      todo.Description = item.Description;

      _context.EventItems.Update(todo);
      _context.SaveChanges();
      return Ok(todo);
    }

    // DELETE: api/event/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var todo = _context.EventItems.FirstOrDefault(t => t.Id == id);
      if (todo == null)
        return NotFound();
      _logger.LogDebug(string.Format("Deleting item with id {0}", todo.Id.ToString()));

      _context.EventItems.Remove(todo);
      _context.SaveChanges();
      return Ok();
    }
  }
}
