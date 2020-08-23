using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestchesterApi.Models;

namespace WestchesterApi.Controllers
{
    [ApiController]
    public class DBRaidsController : ControllerBase
    {
        private readonly DataContext _context;

        
        public DBRaidsController(DataContext context)
        {
            _context = context;
        }

        [Route("api/raids")]
        public async Task<ActionResult<IEnumerable<DBRaid>>> GetDBRaidItems()
        {
            return await _context.Raids.ToListAsync();
            
        }

        [Route("api/raidevents")]
        public async Task<ActionResult<IEnumerable<string>>> GetRaidEvents()
        {
            List<DBRaid> raidList = await _context.Raids.ToListAsync();
            List<string> raidEvents = new List<string>();
            foreach (DBRaid r in raidList)
            {
                raidEvents.Add(r.eventName);
            }
            HashSet<string> hashList = new HashSet<string>(raidEvents);
            return hashList.ToArray<string>();

        }

        [Route("api/raids")]
        [HttpPost]
        public async Task<ActionResult<DBRaid>> PostDBRaid(DBRaid dBRaid)
        {
            _context.Raids.Add(dBRaid);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDBRaid", new { id = dBRaid.raidID }, dBRaid);
        }
        //// GET: api/DBRaids/5
        [Route("api/raids/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DBRaid>> GetDBRaid(long id)
        {
            var dBRaid = await _context.Raids.FindAsync(id);

            if (dBRaid == null)
            {
                return NotFound();
            }

            return dBRaid;
        }

        //// PUT: api/DBRaids/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDBRaid(long id, DBRaid dBRaid)
        //{
        //    if (id != dBRaid.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(dBRaid).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DBRaidExists(id))
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

        //// POST: api/DBRaids
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<DBRaid>> PostDBRaid(DBRaid dBRaid)
        //{
        //    _context.Raids.Add(dBRaid);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDBRaid", new { id = dBRaid.ID }, dBRaid);
        //}

        //// DELETE: api/DBRaids/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DBRaid>> DeleteDBRaid(long id)
        //{
        //    var dBRaid = await _context.Raids.FindAsync(id);
        //    if (dBRaid == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Raids.Remove(dBRaid);
        //    await _context.SaveChangesAsync();

        //    return dBRaid;
        //}

        //private bool DBRaidExists(long id)
        //{
        //    return _context.Raids.Any(e => e.ID == id);
        //}
    }
}
