using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatReportShow
    {
        private HttpClient _httpClient;

        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? AuthorId { get; set; }
        public string Reason { get; set; }
        public bool? Isprocessed { get; set; }
        public DateTime? CreatedDate { get; set; }

        public KatReportShow()
        {

        }

        public List<KatReport> katPostse { get; set; }

        public KatReportShow(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void LoadCurrentObject(List<KatReport> katPosts)
        {
            this.katPostse = new List<KatReport>();
            foreach (KatReport post in katPosts)
            {
                this.katPostse.Add(post);
            }
        }

        public async Task<List<KatReport>> GetReport()
        {
            List<KatReport> katPosts = await _httpClient.GetFromJsonAsync<List<KatReport>>($"katreports/getreport");
            List<KatReport> SortedList = katPosts.OrderByDescending(o => o.CreatedDate).ToList();

            LoadCurrentObject(SortedList);
            return katPostse;
        }

        public static implicit operator KatReportShow(KatReport katsection)
        {
            return new KatReportShow
            {
                AuthorId = katsection.AuthorId,
                PostId = katsection.PostId,
                Reason = katsection.Reason,
                Isprocessed = katsection.Isprocessed,
                CreatedDate = katsection.CreatedDate
            };
        }

        public static implicit operator KatReport(KatReportShow katsectionsh0w)
        {
            return new KatReport
            {
                AuthorId = katsectionsh0w.AuthorId,
                PostId = katsectionsh0w.PostId,
                Reason = katsectionsh0w.Reason,
                Isprocessed = katsectionsh0w.Isprocessed,
                CreatedDate = katsectionsh0w.CreatedDate
            };
        }
    }
}
