using System;
using System.Collections.Generic;

#nullable disable

namespace KatMemeABlazing.Server.Models
{
    public partial class KatReport
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? AuthorId { get; set; }
        public string Reason { get; set; }
        public bool? Isprocessed { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
