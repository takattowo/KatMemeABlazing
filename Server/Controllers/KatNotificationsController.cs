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
    public class KatNotificationsController : ControllerBase
    {
        private readonly KatMemeSocialNetworkingDbContext _context;

        public KatNotificationsController(KatMemeSocialNetworkingDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public List<KatNotification> Get()
        {
            return _context.KatNotifications.ToList();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] KatNotification katp9st)
        {
            _context.KatNotifications.Add(katp9st);
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
            var result = await _context.KatNotifications.Where(u => u.Id == id).FirstOrDefaultAsync();
            _context.KatNotifications.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }

        [HttpGet("getnotification/{id}")]
        public async Task<List<KatNotification>> GetNotification(int id)
        {
            return await _context.KatNotifications.Where(u => u.UserId == id).ToListAsync();
        }

        [HttpGet("getnotificationseen/{id}")]
        public async Task<List<KatNotification>> GetNotificationSeen(int id)
        {
            return await _context.KatNotifications.Where(u => u.UserId == id).Where(u => u.Isread == true).ToListAsync();
        }

        [HttpGet("getnotificationunseen/{id}")]
        public async Task<List<KatNotification>> GetNotificationUnseen(int id)
        {
            return await _context.KatNotifications.Where(u => u.UserId == id).Where(u => u.Isread != true).ToListAsync();
        }

        [HttpGet("getnotificationcount/{id}")]
        public async Task<int> GetNotifCount(int id)
        {
            var p = await _context.KatNotifications.Where(u => u.UserId == id).ToListAsync();
            return p.Count;
        }
        [HttpGet("getnotificationcountunseen/{id}")]
        public async Task<int> GetNotifCountUnseen(int id)
        {
            var p = await _context.KatNotifications.Where(u => u.UserId == id).Where(u => u.Isread != true).ToListAsync();
            return p.Count;
        }
        [HttpGet("getnotificationcountseen/{id}")]
        public async Task<int> GetNotifCountSeen(int id)
        {
            var p = await _context.KatNotifications.Where(u => u.UserId == id).Where(u => u.Isread == true).ToListAsync();
            return p.Count;
        }

        [HttpPut("updatetoseen/{id}")]
        public async Task<KatNotification> UpdateNotif(int id, [FromBody] KatNotification katuselessstayheretodonothingeh)
        {
            KatNotification postToUpdate = await _context.KatNotifications.Where(u => u.Id == id).FirstOrDefaultAsync();
            postToUpdate.Isread = true;
            await _context.SaveChangesAsync();
            return await Task.FromResult(postToUpdate);
        }
    }
}
