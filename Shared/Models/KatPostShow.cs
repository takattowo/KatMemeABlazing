using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatPostShow
    {
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

        public List<KatPost> katPostse { get; set; }
        private readonly HttpClient _httpClient;

        public KatPostShow()
        {

        }

        public KatPostShow(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        private void LoadCurrentObject(List<KatPost> katPosts)
        {
            this.katPostse = new List<KatPost>();
            foreach (KatPost post in katPosts)
            {
                this.katPostse.Add(post);
            }
        }

        private void LoadCurrentObject(KatPostShow katPosts)
        {
            this.Id = katPosts.Id;
            this.PostSectionId = katPosts.PostSectionId;
            this.PostAuthor = katPosts.PostAuthor;
            this.PostTitle = katPosts.PostTitle;
            this.PostImage = katPosts.PostImage;
            this.PostContent = katPosts.PostContent;
            this.PositiveVoteCount = katPosts.PositiveVoteCount;
            this.NegativeVoteCount = katPosts.NegativeVoteCount;
            this.CreatedDate = katPosts.CreatedDate;
        }

        public async Task<List<KatPost>> GetPost(int secid)
        {
            List<KatPost> katPosts = await _httpClient.GetFromJsonAsync<List<KatPost>>($"katposts/getpost/{secid}");
            List<KatPost> SortedList = katPosts.OrderByDescending(o => o.CreatedDate).ToList();

            LoadCurrentObject(SortedList);
            return katPostse;
        }

        public async Task<List<KatPost>> GetPost()
        {
            List<KatPost> katPosts = await _httpClient.GetFromJsonAsync<List<KatPost>>("katposts/getpost");

            LoadCurrentObject(katPosts);
            return katPostse;
        }

        public async Task<List<KatPost>> GetPostFromUser(int u)
        {
            List<KatPost> katPosts = await _httpClient.GetFromJsonAsync<List<KatPost>>($"katposts/getpostfromuser/{u}");
            List<KatPost> SortedList = katPosts.OrderByDescending(o => o.CreatedDate).ToList();

            LoadCurrentObject(SortedList);
            return katPostse;
        }

        public async Task<List<KatPost>> GetPostFromTo(int startIndex, int count, int section)
        {
            List<KatPost> katPosts = await _httpClient.GetFromJsonAsync<List<KatPost>>($"katposts/getpostfromto/?startIndex={startIndex}&count={count}&section={section}");
            List<KatPost> SortedList = katPosts.OrderByDescending(o => o.CreatedDate).ToList();

            LoadCurrentObject(SortedList);
            return katPostse;
        }

        public async Task<List<KatPost>> GetPostFromTo(int startIndex, int count)
        {
            List<KatPost> katPosts = await _httpClient.GetFromJsonAsync<List<KatPost>>($"katposts/gethotpost?startIndex={startIndex}&count={count}");
            List<KatPost> SortedList = katPosts.OrderByDescending(o => o.CreatedDate).ToList();

            LoadCurrentObject(SortedList);
            return katPostse;
        }

        public async Task<List<KatPost>> GetPostFromToFresh(int startIndex, int count)
        {
            List<KatPost> katPosts = await _httpClient.GetFromJsonAsync<List<KatPost>>($"katposts/getfreshpost?startIndex={startIndex}&count={count}");
            List<KatPost> SortedList = katPosts.OrderByDescending(o => o.CreatedDate).ToList();

            LoadCurrentObject(SortedList);
            return katPostse;
        }

        /*public async Task<List<KatPost>> GetPostForShow(int pidd)
        {
            KatPost katPosts = await _httpClient.GetFromJsonAsync<KatPost>($"katposts/getpostforshow/{pidd}");

            LoadCurrentObject(katPosts);
            return katPostse;
        }*/

        public async Task GetPostForShow(int id)
        {
            KatPost katPosts = await _httpClient.GetFromJsonAsync<KatPost>($"katposts/getpostforshow/{id}");
            LoadCurrentObject(katPosts);
        }

        public async Task GetLastestPost()
        {
            KatPost katPosts = await _httpClient.GetFromJsonAsync<KatPost>("katposts/getlastestpost");
            LoadCurrentObject(katPosts);
        }

        public async Task<String> GetSectionPicture(int secid)
        {
            KatSection katPosts = await _httpClient.GetFromJsonAsync<KatSection>($"katsection/getsectionpicture/{secid}");

            return katPosts.SectionPicture;
        }

        public KatPostShow(int id, int postsectionid, int postauthor, string postitle, string postimage, string postcontent, int posvote, int neggavote, DateTime createdate)
        {
            this.Id = id;
            this.PostSectionId = postsectionid;
            this.PostAuthor = postauthor;
            this.PostTitle = postitle;
            this.PostImage = postimage;
            this.PostContent = postcontent;
            this.PositiveVoteCount = posvote;
            this.NegativeVoteCount = neggavote;
            this.CreatedDate = createdate;
        }

        public async Task<int> GetPostCount()
        {
            return await _httpClient.GetFromJsonAsync<int>($"katposts/getpostcount");
        }

        public async Task<int> GetPostCountExceptDisscuss()
        {
            return await _httpClient.GetFromJsonAsync<int>($"katposts/getpostcountnodiscuss");
        }
        public async Task<int> GetHotPostCount()
        {
            return await _httpClient.GetFromJsonAsync<int>($"katposts/gethotpostcount");
        }

        //operators
        public static implicit operator KatPostShow(KatPost katPost)
        {
            return new KatPostShow
            {
                Id = katPost.Id,
                PostSectionId = katPost.PostSectionId,
                PostAuthor = katPost.PostAuthor,
                PostTitle = katPost.PostTitle,
                PostImage = katPost.PostImage,
                PostContent = katPost.PostContent,
                PositiveVoteCount = katPost.PositiveVoteCount,
                NegativeVoteCount = katPost.NegativeVoteCount,
                CreatedDate = katPost.CreatedDate
            };
        }

        public static implicit operator KatPost(KatPostShow katPostShow)
        {
            return new KatPost
            {
                Id = katPostShow.Id,
                PostSectionId = katPostShow.PostSectionId,
                PostAuthor = katPostShow.PostAuthor,
                PostTitle = katPostShow.PostTitle,
                PostImage = katPostShow.PostImage,
                PostContent = katPostShow.PostContent,
                PositiveVoteCount = katPostShow.PositiveVoteCount,
                NegativeVoteCount = katPostShow.NegativeVoteCount,
                CreatedDate = katPostShow.CreatedDate
            };
        }
    }

}
