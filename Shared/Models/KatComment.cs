using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatComment
    {
        private HttpClient _httpClient;

        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? CommentAuthor { get; set; }
        [Required]
        [MaxLength(255)]
        public string CommentContent { get; set; }
        public int? PositiveVoteCount { get; set; }
        public int? NegativeVoteCount { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual KatUser CommentAuthorNavigation { get; set; }
        public virtual KatPost Post { get; set; }
        public KatComment()
        {

        }
        public KatComment(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
