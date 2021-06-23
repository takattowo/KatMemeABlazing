using System;
using System.Collections.Generic;
using System.Net.Http;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatNotification
    {
        private HttpClient _httpClient;

        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? UserId { get; set; }
        public int? AuthorId { get; set; }
        public string PostContent { get; set; }
        public bool? Isread { get; set; }
        public DateTime? CreatedDate { get; set; }

        public KatNotification()
        {

        }

        public KatNotification(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
