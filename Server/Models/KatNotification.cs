using System;
using System.Collections.Generic;

#nullable disable

namespace KatMemeABlazing.Server.Models
{
    public partial class KatNotification
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? UserId { get; set; }
        public int? AuthorId { get; set; }
        public string PostContent { get; set; }
        public bool? Isread { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
