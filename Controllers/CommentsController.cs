using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KSMWebApi.Models;

namespace KSMWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly KsmartContext _context = new KsmartContext();
        private readonly ILogger<ArtObjectsController> _logger;

        public CommentsController(ILogger<ArtObjectsController> logger)
        {
          _logger = logger;
        }

        // GET: Comments
        [HttpGet("All")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Comments.ToListAsync());
        }

        // GET: Comments/Details/5
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]       
        public async Task<IActionResult> Create([FromBody] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return Ok(comment);
            }
            else return StatusCode(StatusCodes.Status206PartialContent);   
        }

        // PUT: Comments/Update/5        
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id)
            {
                return StatusCode(StatusCodes.Status206PartialContent);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return StatusCode(StatusCodes.Status404NotFound);
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(comment);
            }
            return StatusCode(StatusCodes.Status206PartialContent);
        }

        // POST: Comments/Delete/5
        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }
            else return StatusCode(StatusCodes.Status404NotFound);


            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK);
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
