using KatMemeABlazing.Server.Models;
using KatMemeABlazing.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KatMemeABlazing.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KatCommentsController : ControllerBase
    {
        private readonly ILogger<KatCommentsController> _logger;

        private readonly KatMemeSocialNetworkingDbContext _context;

        public KatCommentsController(ILogger<KatCommentsController> logger, KatMemeSocialNetworkingDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        /*
                [HttpGet]
                public List<KatPost> Get()
                {
                    return _context.KatPosts.ToList();
                }*/   

        [HttpGet("getcomment")]
        public List<KatComment> GetPost()
        {
            return _context.KatComments.ToList();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] KatComment katComment)
        {
            _context.KatComments.Add(katComment);
            await _context.SaveChangesAsync();
            //LocalRedirect("login");
            return Ok(katComment);
        }

        [HttpGet("getcommentcount/{id}")]
        public async Task<int> GetCommentCount(int id)
        {
            var p = await _context.KatComments.Where(u => u.PostId == id).ToListAsync();
            return p.Count;
        }

        [HttpGet("getcomment/{id}")]
        public async Task<List<KatComment>> GetCommentFromPost(int id)
        {
            //return _context.KatPosts.Include(u => u.PostSection).Include(u => u.PostAuthorNavigation).ThenInclude(p => p.DisplayPicture).Where(u => u.PostSectionId == id).ToList();

            var katcmt = await _context.KatComments.Include(u => u.CommentAuthorNavigation).Where(u => u.PostId == id).ToListAsync();
            if (katcmt == null)
                return null;
            return katcmt;
        }
    }
}
