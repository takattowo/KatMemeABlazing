using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatPost
    {
        private HttpClient _httpClient;

        public KatPost()
        {
            KatComments = new HashSet<KatComment>();
        }
    

        public KatPost(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public int Id { get; set; }
        [Required(ErrorMessage = "Section is required.")]
        public int? PostSectionId { get; set; } = 1;
        public int? PostAuthor { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100)]
        public string PostTitle { get; set; }
        [Required(ErrorMessage = "Whut! No image!?")]
        public string PostImage { get; set; }
        public string PostContent { get; set; }
        public int? PositiveVoteCount { get; set; }
        public int? NegativeVoteCount { get; set; }
        public bool? IsPromoted { get; set; }
        public DateTime? CreatedDate { get; set; }

        public KatUser PostAuthorNavigation { get; set; }
        public virtual KatSection PostSection { get; set; }
        public virtual ICollection<KatComment> KatComments { get; set; }
    }

}
