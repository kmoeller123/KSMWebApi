using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KSMWebApi.Models;
using Microsoft.VisualBasic;

namespace KSMWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemViewsController : Controller
    {
        private readonly KsmartContext _context = new KsmartContext();
        private readonly ILogger<ArtObjectsController> _logger;

        public ItemViewsController(ILogger<ArtObjectsController> logger)
        {
            _logger = logger;
        }

        // GET: ItemViews
        [HttpGet("All")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.ItemViews.ToListAsync());
        }

        // GET: ItemViews/Details/5
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status206PartialContent);
            }

            var itemViews = await _context.ItemViews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemViews == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(itemViews);
        }

        // POST: ItemViews/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ItemViews itemViews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemViews);
                await _context.SaveChangesAsync();
                return Ok(itemViews);
            }
            return StatusCode(StatusCodes.Status206PartialContent);
        }

        // GET: ItemViews/Edit/5
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> Update(int? id, [FromBody] ItemViews itemViews)
        {
            if (id != itemViews.Id)
            {
                return StatusCode(StatusCodes.Status206PartialContent);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemViews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemViewsExists(itemViews.Id))
                    {
                        return StatusCode(StatusCodes.Status404NotFound);
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(itemViews);
            }
            return StatusCode(StatusCodes.Status206PartialContent);
        }

        // POST: ArtObjects/Delete/5
        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var itemViews = await _context.ItemViews.FindAsync(id);
            if (itemViews != null)
            {
                _context.ItemViews.Remove(itemViews);
            }
            else return StatusCode(StatusCodes.Status404NotFound);

            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK);
        }

        private bool ItemViewsExists(int id)
        {
            return _context.ItemViews.Any(e => e.Id == id);
        }
    }
}
