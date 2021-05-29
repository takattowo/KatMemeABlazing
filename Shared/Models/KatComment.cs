using System;
using System.Collections.Generic;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatComment
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? CommentAuthor { get; set; }
        public string CommentContent { get; set; }
        public int? PositiveVoteCount { get; set; }
        public int? NegativeVoteCount { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual KatUser CommentAuthorNavigation { get; set; }
        public virtual KatPost Post { get; set; }
    }
}
