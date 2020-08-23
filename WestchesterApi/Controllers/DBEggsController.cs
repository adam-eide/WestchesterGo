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
    
    [ApiController]
    public class DBEggsController : ControllerBase
    {
        private readonly DataContext _context;

        public DBEggsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DBEggs
        [Route("api/eggs")]
        public async Task<ActionResult<IEnumerable<DBEgg>>> GetEggs()
        {
            return await _context.Eggs.ToListAsync();
            
        }

        [Route("api/eggevents")]
        public async Task<ActionResult<IEnumerable<string>>> GetEggEvents()
        {
            List<DBEgg> eggList = await _context.Eggs.ToListAsync();
            List<string> eggEvents = new List<string>();
            foreach(DBEgg e in eggList)
            {
                eggEvents.Add(e.eventName);
            }
            HashSet<string> hashList = new HashSet<string>(eggEvents);
            return hashList.ToArray<string>();

        }

        [Route("api/eggs")]
        [HttpPost]
        public async Task<ActionResult<DBEgg>> PostDBEgg(DBEgg dBEgg)
        {
            _context.Eggs.Add(dBEgg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDBEgg", new { id = dBEgg.id }, dBEgg);
        }

        // GET: api/DBEggs/5
        [Route("api/eggs/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DBEgg>> GetDBEgg(long id)
        {
            var dBEgg = await _context.Eggs.FindAsync(id);

            if (dBEgg == null)
            {
                return NotFound();
            }

            return dBEgg;
        }

        //// PUT: api/DBEggs/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDBEgg(long id, DBEgg dBEgg)
        //{
        //    if (id != dBEgg.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(dBEgg).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DBEggExists(id))
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

        //// POST: api/DBEggs
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<DBEgg>> PostDBEgg(DBEgg dBEgg)
        //{
        //    _context.Eggs.Add(dBEgg);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDBEgg", new { id = dBEgg.ID }, dBEgg);
        //}

        //// DELETE: api/DBEggs/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DBEgg>> DeleteDBEgg(long id)
        //{
        //    var dBEgg = await _context.Eggs.FindAsync(id);
        //    if (dBEgg == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Eggs.Remove(dBEgg);
        //    await _context.SaveChangesAsync();

        //    return dBEgg;
        //}

        //private bool DBEggExists(long id)
        //{
        //    return _context.Eggs.Any(e => e.ID == id);
        //}
    }
}
