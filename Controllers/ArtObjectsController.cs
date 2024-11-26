using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KSMWebApi.Models;
using System.Net;

namespace KSMWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtObjectsController : Controller
    {
        private readonly KsmartContext _context = new KsmartContext();
        private readonly ILogger<ArtObjectsController> _logger;

        public ArtObjectsController(ILogger<ArtObjectsController> logger)
        {
            _logger = logger;
        }

        //GET: ArtObjects
        [HttpGet("All")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.ArtObjects.ToListAsync());
        }

        // GET: ArtObjects/Details/5
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var result = await _context.ArtObjects.Where(m => m.Id == id).FirstOrDefaultAsync();


            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                return Ok(result);
            }
         }

        // POST: ArtObjects/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ArtObject artObject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artObject);
                await _context.SaveChangesAsync();
                return Ok(artObject);
            }

            return StatusCode(StatusCodes.Status404NotFound);

        }

        // PUT: ArtObjects/Update/5        
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] ArtObject artObject)
        {
            if (id != artObject.Id)              
             {
                    return NotFound();
             }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtObjectExists(artObject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(artObject);
            }
            return BadRequest("request is not valid");
        }


        // POST: ArtObjects/Delete/5
        [HttpPost("Delete/{id:int}")]        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artObject = await _context.ArtObjects.FindAsync(id);
            if (artObject != null)
            {
                _context.ArtObjects.Remove(artObject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtObjectExists(int id)
        {
            return _context.ArtObjects.Any(e => e.Id == id);
        }
    }
}
