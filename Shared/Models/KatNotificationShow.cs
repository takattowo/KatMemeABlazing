using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatNotificationShow
    {
        private HttpClient _httpClient;

        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? UserId { get; set; }
        public int? AuthorId { get; set; }
        public string PostContent { get; set; }
        public bool? Isread { get; set; }
        public DateTime? CreatedDate { get; set; }

        public KatNotificationShow()
        {

        }

        public KatNotificationShow(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<KatNotification> katPostse { get; set; }

        private void LoadCurrentObject(List<KatNotification> katPosts)
        {
            this.katPostse = new List<KatNotification>();
            foreach (KatNotification post in katPosts)
            {
                this.katPostse.Add(post);
            }
        }

        public async Task<List<KatNotification>> GetNotification(int id)
        {
            List<KatNotification> katPosts = await _httpClient.GetFromJsonAsync<List<KatNotification>>($"katnotifications/getnotification/{id}");
            List<KatNotification> SortedList = katPosts.OrderBy(o => o.Isread).ToList();

            LoadCurrentObject(SortedList);
            return katPostse;
        }

        public async Task<int> GetNotificationCount(int id)
        {
            return await _httpClient.GetFromJsonAsync<int>($"katnotifications/getnotificationcount/{id}");
        }

        public async Task<int> GetNotificationCountUnseen(int id)
        {
            return await _httpClient.GetFromJsonAsync<int>($"katnotifications/getnotificationcountunseen/{id}");
        }

        public static implicit operator KatNotificationShow(KatNotification katsection)
        {
            return new KatNotificationShow
            {
                PostId = katsection.PostId,
                UserId = katsection.UserId,
                AuthorId = katsection.AuthorId,
                PostContent = katsection.PostContent,
                Isread = katsection.Isread
            };
        }

        public static implicit operator KatNotification(KatNotificationShow katsectionsh0w)
        {
            return new KatNotification
            {
                PostId = katsectionsh0w.PostId,
                UserId = katsectionsh0w.UserId,
                AuthorId = katsectionsh0w.AuthorId,
                PostContent = katsectionsh0w.PostContent,
                Isread = katsectionsh0w.Isread
            };
        }
    }
}
