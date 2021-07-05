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
    public class KatReportsController : ControllerBase
    {
        private readonly KatMemeSocialNetworkingDbContext _context;

        public KatReportsController(KatMemeSocialNetworkingDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public List<KatReport> Get()
        {
            return _context.KatReports.ToList();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] KatReport katp9st)
        {
            _context.KatReports.Add(katp9st);
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
            var result = await _context.KatReports.Where(u => u.Id == id).FirstOrDefaultAsync();
            _context.KatReports.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }

        [HttpPut("ignore/{id}")]
        public async Task<KatReport> IgnoreReport(int id, [FromBody] KatReport katuselessstayheretodonothingeh)
        {
            KatReport postToUpdate = await _context.KatReports.Where(u => u.Id == id).FirstOrDefaultAsync();
            postToUpdate.Isprocessed = false;
            await _context.SaveChangesAsync();
            return await Task.FromResult(postToUpdate);
        }

        [HttpPut("process/{id}")]
        public async Task<KatReport> ProcessReport(int id, [FromBody] KatReport katuselessstayheretodonothingeh)
        {
            KatReport postToUpdate = await _context.KatReports.Where(u => u.Id == id).FirstOrDefaultAsync();
            postToUpdate.Isprocessed = true;
            await _context.SaveChangesAsync();
            return await Task.FromResult(postToUpdate);
        }

        [HttpGet("getreport")]
        public List<KatReport> GetReport()
        {
            return _context.KatReports.ToList();
        }

        [HttpGet("getreport/{id}")]
        public async Task<KatReport> GetReportFromId(int id)
        {
            return await _context.KatReports.Where(u => u.Id == id).FirstOrDefaultAsync();
        }
    }
}
