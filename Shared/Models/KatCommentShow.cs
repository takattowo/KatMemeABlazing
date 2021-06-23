using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatCommentShow
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

        private readonly HttpClient _httpClient;

        public List<KatComment> katPostse { get; set; }

        public KatCommentShow()
        {

        }

        public KatCommentShow(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void LoadCurrentObject(List<KatComment> katCmt)
        {
            this.katPostse = new List<KatComment>();
            foreach (KatComment post in katCmt)
            {
                this.katPostse.Add(post);
            }
        }

        public async Task<int> GetCommentCount(int id)
        {
            return await _httpClient.GetFromJsonAsync<int>($"katcomments/getcommentcount/{id}");
        }
        public async Task<List<KatComment>> GetComment(int pid)
        {
            List<KatComment> katcmts = await _httpClient.GetFromJsonAsync<List<KatComment>>($"katcomments/getcomment/{pid}");
            LoadCurrentObject(katcmts);
            return katPostse;
        }

        public static implicit operator KatCommentShow(KatComment katPost)
        {
            return new KatCommentShow
            {
                CommentAuthor = katPost.CommentAuthor,
                CommentContent = katPost.CommentContent,
                PositiveVoteCount = katPost.PositiveVoteCount,
                NegativeVoteCount = katPost.NegativeVoteCount,
                CreatedDate = katPost.CreatedDate
            };
        }

        public static implicit operator KatComment(KatCommentShow katPostShow)
        {
            return new KatComment
            {
                CommentAuthor = katPostShow.CommentAuthor,
                CommentContent = katPostShow.CommentContent,
                PositiveVoteCount = katPostShow.PositiveVoteCount,
                NegativeVoteCount = katPostShow.NegativeVoteCount,
                CreatedDate = katPostShow.CreatedDate
            };
        }
    }
}
