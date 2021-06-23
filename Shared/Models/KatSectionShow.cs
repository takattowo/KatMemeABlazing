using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatSectionShow
    {
        private HttpClient _httpClient;

        public int Id { get; set; }
        public string SectionName { get; set; }
        public string SectionDescription { get; set; }
        public string SectionPicture { get; set; }

        public KatSectionShow()
        {

        }
        public KatSectionShow(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void LoadSection(KatSectionShow katsectionsh0w)
        {
            this.SectionName = katsectionsh0w.SectionName;
            this.SectionDescription = katsectionsh0w.SectionDescription;
            this.SectionPicture = katsectionsh0w.SectionPicture;
        }

        public async Task GetSection(int? id)
        {
            KatSection sect = await _httpClient.GetFromJsonAsync<KatSection>($"katsections/getsection/{id}");
            LoadSection(sect);
        }

        public async Task<int> GetPostCount(int secid)
        {
            return await _httpClient.GetFromJsonAsync<int>($"katposts/getpostcount/{secid}");
        }


        public static implicit operator KatSectionShow(KatSection katsection)
        {
            return new KatSectionShow
            {
                SectionName = katsection.SectionName,
                SectionDescription = katsection.SectionDescription,
                SectionPicture = katsection.SectionPicture
            };
        }

        public static implicit operator KatSection(KatSectionShow katsectionsh0w)
        {
            return new KatSection
            {
                SectionName = katsectionsh0w.SectionName,
                SectionDescription = katsectionsh0w.SectionDescription,
                SectionPicture = katsectionsh0w.SectionPicture
            };
        }
    }

}
