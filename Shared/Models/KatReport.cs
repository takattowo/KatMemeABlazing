using System;
using System.Collections.Generic;
using System.Net.Http;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatReport
    {
        private HttpClient _httpClient;

        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? AuthorId { get; set; }
        public string Reason { get; set; }
        public bool? Isprocessed { get; set; }
        public DateTime? CreatedDate { get; set; }

        public KatReport()
        {

        }

        public KatReport(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
