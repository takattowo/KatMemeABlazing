using System;
using System.Collections.Generic;

#nullable disable

namespace KatMemeABlazing.Server.Models
{
    public partial class KatPost
    {
        public KatPost()
        {
            KatComments = new HashSet<KatComment>();
        }

        public int Id { get; set; }
        public int? PostSectionId { get; set; }
        public int? PostAuthor { get; set; }
        public string PostTitle { get; set; }
        public string PostImage { get; set; }
        public string PostContent { get; set; }
        public int? PositiveVoteCount { get; set; }
        public int? NegativeVoteCount { get; set; }
        public bool? IsPromoted { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual KatUser PostAuthorNavigation { get; set; }
        public virtual KatSection PostSection { get; set; }
        public virtual ICollection<KatComment> KatComments { get; set; }
    }
}
