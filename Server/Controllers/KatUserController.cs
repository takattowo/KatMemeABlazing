using KatMemeABlazing.Server.Models;
using KatMemeABlazing.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatMemeABlazing.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KatUserController : ControllerBase
    {
        private readonly ILogger<KatUserController> _logger;

        private readonly KatMemeSocialNetworkingDbContext _context;

        public KatUserController(ILogger<KatUserController> logger, KatMemeSocialNetworkingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public List<KatUser> Get()
        {
            return _context.KatUsers.ToList();
        }

        [HttpPut("updateprofile/{userId}")]
        public async Task<KatUser> UpdateProfile(int userId, [FromBody] KatUser katUser)
        {

            KatUser userToUpdate = await _context.KatUsers.Where(u => u.Id == userId).FirstOrDefaultAsync();

            userToUpdate.DisplayName = katUser.DisplayName;
            userToUpdate.DisplayPicture = katUser.DisplayPicture;
            userToUpdate.Country = katUser.Country;
            userToUpdate.CustomStatus = katUser.CustomStatus;

            await _context.SaveChangesAsync();

            return await Task.FromResult(katUser);
        }

        [HttpGet("getprofile/{userId}")]
        public async Task<KatUser> GetProfile(int userId)
        {
            return await _context.KatUsers.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }
    }
}
