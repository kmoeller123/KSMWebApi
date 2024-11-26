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
        public async Task<IList<Comment>> Index()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: Comments/Details/5
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
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
       
        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
