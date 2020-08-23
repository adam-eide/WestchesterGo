using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestchesterApi.Models;

namespace WestchesterApi.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class CurrentEventsController : ControllerBase
    {
        private readonly DataContext _context;

        public CurrentEventsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CurrentEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrentEvent>>> GetCurrentEvent()
        {
            return await _context.CurrentEvent.ToListAsync();
        }

        //// GET: api/CurrentEvents/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<CurrentEvent>> GetCurrentEvent(long id)
        //{
        //    var currentEvent = await _context.CurrentEvent.FindAsync(id);

        //    if (currentEvent == null)
        //    {
        //        return NotFound();
        //    }

        //    return currentEvent;
        //}

        //// PUT: api/CurrentEvents/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCurrentEvent(long id, CurrentEvent currentEvent)
        //{
        //    if (id != currentEvent.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(currentEvent).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CurrentEventExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/CurrentEvents
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<CurrentEvent>> PostCurrentEvent(CurrentEvent currentEvent)
        //{
        //    _context.CurrentEvent.Add(currentEvent);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCurrentEvent", new { id = currentEvent.ID }, currentEvent);
        //}

        //// DELETE: api/CurrentEvents/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<CurrentEvent>> DeleteCurrentEvent(long id)
        //{
        //    var currentEvent = await _context.CurrentEvent.FindAsync(id);
        //    if (currentEvent == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CurrentEvent.Remove(currentEvent);
        //    await _context.SaveChangesAsync();

        //    return currentEvent;
        //}

        //private bool CurrentEventExists(long id)
        //{
        //    return _context.CurrentEvent.Any(e => e.ID == id);
        //}
    }
}
