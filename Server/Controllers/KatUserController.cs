using KatMemeABlazing.Server.Models;
using KatMemeABlazing.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost("loginuser")]
        public async Task<ActionResult<KatUser>> LoginUser (KatUser katUser)
        {
            KatUser loggedinUser = await _context.KatUsers.Where(u => u.Email == katUser.Email && u.Password == katUser.Password).FirstOrDefaultAsync();

            if (loggedinUser != null)
            {
                var claim = new Claim(ClaimTypes.Name, loggedinUser.Email);
                var claimsIndentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIndentity);
                await HttpContext.SignInAsync(claimsPrincipal);
            }
            return await Task.FromResult(loggedinUser);
        }

        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<KatUser>> GetCurrentUser()
        {
            KatUser currentUser = new KatUser();

            if (User.Identity.IsAuthenticated)
            {
                var email = User.FindFirstValue(ClaimTypes.Name);
                currentUser = await _context.KatUsers.Where(u => u.Email == email).FirstOrDefaultAsync();
            }

            return await Task.FromResult(currentUser);
        }

        [HttpGet("logoutuser")]
        public async Task<ActionResult<String>> LogoutUser()
        {
            await HttpContext.SignOutAsync();
            return "Fuck you and i will see you again.";
        }

        [HttpPut("updateprofile/{id}")]
        public async Task<KatUser> UpdateProfile(int id, [FromBody] KatUser katUser)
        {

            KatUser userToUpdate = await _context.KatUsers.Where(u => u.Id == id).FirstOrDefaultAsync();

            userToUpdate.DisplayName = katUser.DisplayName;
            userToUpdate.DisplayPicture = katUser.DisplayPicture;
            userToUpdate.Country = katUser.Country;
            userToUpdate.CustomStatus = katUser.CustomStatus;

            await _context.SaveChangesAsync();

            return await Task.FromResult(katUser);
        }

        [HttpGet("getprofile/{id}")]
        public async Task<KatUser> GetProfile(int id)
        {
            return await _context.KatUsers.Where(u => u.Id == id).FirstOrDefaultAsync();
        }
    }
}
