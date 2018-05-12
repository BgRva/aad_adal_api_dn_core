using System;
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
  [Route("api/user")]
  public class UserController : Controller
  {
    private readonly UserContext _context;
    private readonly ILogger _logger;

    public UserController(UserContext context, ILogger<UserController> logger)
    {
      _context = context;
      _logger = logger;

      _logger.LogDebug("Instantiating a user ...");
      if (!_context.Users.Any())
      {
        var user = new UserProfile
        {
          Id = 1,
          Name = "Barney Rubble",
          OID = Guid.Empty,
          UPN = "barney.rubble@blah.com",
          Team = TeamType.WaterBuffaloes,
          Faction = FactionType.Horde
        };
        
        _context.Users.Add(user);
        _context.SaveChanges();
      }
    }

    // GET: api/User
    [HttpGet]
    public IEnumerable<UserProfile> Get()
    {
      _logger.LogDebug("returning all ...");
      return _context.Users.ToList();
    }

    // GET: api/User/5
    [HttpGet("{id}", Name = "GetUser")]
    public IActionResult Get(int id)
    {
      _logger.LogDebug("Getting user {0}", id.ToString());
      var item = _context.Users.FirstOrDefault(t => t.Id == id);
      if (item == null)
        return NotFound();
      return new ObjectResult(item);
    }

    // POST: api/User
    [HttpPost]
    public IActionResult Post([FromBody] UserProfile item)
    {
      if (item == null)
      {
        _logger.LogDebug("Item is null");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        _logger.LogDebug("Item is not valid");
        return BadRequest(ModelState);
      }

      _context.Users.Add(item);
      _context.SaveChanges();
      _logger.LogDebug(string.Format("Created item with id {0}", item.Id.ToString()));

      return CreatedAtRoute("GetUser", new {id = item.Id}, item);
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UserProfile item)
    {
      if (item == null || item.Id != id)
        return BadRequest();

      var user = _context.Users.FirstOrDefault(t => t.Id == id);
      if (user == null)
        return NotFound();

      _logger.LogDebug(string.Format("Updating user with id {0}", item.Id.ToString()));

      // can only change team or faction
      user.Team = item.Team;
      user.Faction = item.Faction;

      _context.Users.Update(user);
      _context.SaveChanges();
      return Ok(user);
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var user = _context.Users.FirstOrDefault(t => t.Id == id);
      if (user == null)
        return NotFound();
      _logger.LogDebug(string.Format("Deleting user with id {0}", user.Id.ToString()));

      _context.Users.Remove(user);
      _context.SaveChanges();
      return Ok();
    }
  }
}
