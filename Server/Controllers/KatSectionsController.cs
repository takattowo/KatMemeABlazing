using KatMemeABlazing.Server.Models;
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
    public class KatSectionsController : ControllerBase
    {
        private readonly ILogger<KatSectionsController> _logger;

        private readonly KatMemeSocialNetworkingDbContext _context;

        public KatSectionsController(ILogger<KatSectionsController> logger, KatMemeSocialNetworkingDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        [HttpGet]
        public List<KatSection> Get()
        {
            return _context.KatSections.ToList();
        }

     
        [HttpGet("getsection")]
        public List<KatSection> GetSection()
        {
            return _context.KatSections.ToList();
        }

        /*[HttpGet("getprofilepicture/{id}")]
        public async Task<Models.KatUser> GetProfilePicture(int id)
        {
            return await _context.KatSections.Where(u => u.Id == id).FirstOrDefaultAsync();
        }*/

        [HttpGet("getsection/{id}")]
        public async Task<KatSection> GetSectionFromId(int id)
        {
            //return _context.KatPosts.Include(u => u.PostSection).Include(u => u.PostAuthorNavigation).ThenInclude(p => p.DisplayPicture).Where(u => u.PostSectionId == id).ToList();

            /*var katsection =  _context.KatSections.Where(x => x.Id == id)
                .ToList();
            if (katsection == null)
                return null;
            return katsection;
*/

            return await _context.KatSections.Where(u => u.Id == id).FirstOrDefaultAsync();
        }
    }
}
