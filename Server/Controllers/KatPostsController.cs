using KatMemeABlazing.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatMemeABlazing.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KatPostsController : ControllerBase
    {
        private readonly ILogger<KatPostsController> _logger;

        private readonly KatMemeSocialNetworkingDbContext _context;

        public KatPostsController(ILogger<KatPostsController> logger, KatMemeSocialNetworkingDbContext context)
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

        [HttpGet("getpost")]
        public List<KatPost> GetPost()
        {
            return _context.KatPosts.ToList();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] KatPost katp9st)
        {
            _context.KatPosts.Add(katp9st);
            await _context.SaveChangesAsync();
            //LocalRedirect("p");
            //RedirectToAction($"/p/{katp9st.Id}");
            //RedirectToPage($"/p/{katp9st.Id}");
            //Response.Redirect(Url.Action($"/p/{katp9st.Id}"));
            return Ok(katp9st);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.KatPosts.Where(u => u.Id == id).FirstOrDefaultAsync();
            _context.KatPosts.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }


        [HttpPut("update/{id}")]
        public async Task<KatPost> UpdatePost(int id, [FromBody] KatPost katPost)
        {
            KatPost postToUpdate = await _context.KatPosts.Where(u => u.Id == id).FirstOrDefaultAsync();
            postToUpdate.PostTitle = katPost.PostTitle;
            postToUpdate.PostContent = katPost.PostContent;
            postToUpdate.PostImage = katPost.PostImage;
            await _context.SaveChangesAsync();
            return await Task.FromResult(postToUpdate);
        }

        [HttpGet("getpost/{id}")]
        public async Task<List<KatPost>> GetPostFromSection(int id)
        {
            //return _context.KatPosts.Include(u => u.PostSection).Include(u => u.PostAuthorNavigation).ThenInclude(p => p.DisplayPicture).Where(u => u.PostSectionId == id).ToList();

            var katuser = await _context.KatPosts.Include(u => u.PostAuthorNavigation)
                .Where(u => u.PostSectionId == id).ToListAsync();
            if (katuser == null)
                return null;
            return katuser;
        }

        [HttpGet("getpostfromuser/{id}")]
        public async Task<List<KatPost>> GetPostFromUser(int id)
        {
            //return _context.KatPosts.Include(u => u.PostSection).Include(u => u.PostAuthorNavigation).ThenInclude(p => p.DisplayPicture).Where(u => u.PostSectionId == id).ToList();

            var katuser = await _context.KatPosts
                .Where(u => u.PostAuthor == id).ToListAsync();
            if (katuser == null)
                return null;
            return katuser;
        }

        [HttpGet("getpostforshow/{id}")]
        public async Task<KatPost> GetPostForShow(int id)
        {
            return await _context.KatPosts.Include(u => u.PostAuthorNavigation).Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        [HttpGet("getlastestpost")]
        public async Task<KatPost> GetLastestPost()
        {
            return await _context.KatPosts.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        }

        /*[HttpGet("gethotpost")]
        public List<KatPost> GetHotPost()
        {
            return _context.KatPosts.Include(u => u.PostSection).Include(u => u.PostAuthorNavigation).Where(u => (u.PositiveVoteCount - u.NegativeVoteCount) >= 10).ToList();
        }*/



        [HttpGet("gethotpost")]
        public async Task<List<KatPost>> GetVisiblePosts(int startIndex, int count)
        {
            return await _context.KatPosts
                .Include(u => u.PostSection)
                .Include(u => u.PostAuthorNavigation)
                .Where(u => u.PostSectionId != 4)
                .Where(u => (u.PositiveVoteCount - u.NegativeVoteCount) >= 10)
                .Skip(startIndex)
                .Take(count)
                .ToListAsync();
        }


        [HttpGet("getfreshpost")]
        public async Task<List<KatPost>> GetVisiblePostsFresh(int startIndex, int count)
        {
            return await _context.KatPosts
                .Include(u => u.PostSection)
                .Include(u => u.PostAuthorNavigation)
                .Where(u => u.PostSectionId != 4)
                .Skip(startIndex)
                .Take(count)
                .ToListAsync();
        }

        [HttpGet("getpostfromto")]
        public async Task<List<KatPost>> GetVisiblePosts(int startIndex, int count, int section)
        {
            return await _context.KatPosts
                .Include(u => u.PostAuthorNavigation)
                .Where(u => u.PostSectionId == section)
                .Skip(startIndex)
                .Take(count)
                .ToListAsync();
        }

        [HttpGet("getpostcount")]
        public async Task<int> GetPostCount()
        {
            var p = await _context.KatPosts.ToListAsync();
            return p.Count;
        }

        [HttpGet("getpostcountnodiscuss")]
        public async Task<int> GetPostCountNoDiscuss()
        {
            var p = await _context.KatPosts.Where(u => u.PostSectionId != 4).ToListAsync();
            return p.Count;
        }

        [HttpGet("getpostcount/{id}")]
        public async Task<int> GetPostCount(int id)
        {
            var p = await _context.KatPosts.Where(u => u.PostSectionId == id).ToListAsync();
            return p.Count;
        }

        [HttpGet("gethotpostcount")]
        public async Task<int> GetHotPostCount()
        {
            var p = await _context.KatPosts.Where(u => u.PostSectionId != 4).Where(u => (u.PositiveVoteCount - u.NegativeVoteCount) >= 10).ToListAsync();
            return p.Count;
        }
    }
}
