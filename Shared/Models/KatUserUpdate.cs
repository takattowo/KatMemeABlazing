using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatUserUpdate
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(13)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        public string DisplayPicture { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [MaxLength(50)]
        public string CustomStatus { get; set; }
        public string Message { get; set; }

        private HttpClient _httpClient;

        public KatUserUpdate()
        {

        }

        public KatUserUpdate(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UpdateSucc()
        {
            this.Message = "Processing...";
            KatUser user = this;
            await _httpClient.PutAsJsonAsync("katuser/updateprofile/" + this.Id, user);
            
        }

        public async Task GetProfile()
        {
            this.Message = "Processing...";
            KatUser user = await _httpClient.GetFromJsonAsync<KatUser>("katuser/getprofile/" + this.Id);
            LoadUser(user);
            this.Message = "Profile loaded! (￣ ▽ ￣)╭ ";
        }

        public async Task GetProfile(int? id)
        {
            this.Message = "Processing...";
            KatUser user = await _httpClient.GetFromJsonAsync<KatUser>("katuser/getprofile/" + id);

            if (user != null)
                LoadUser(user);

            this.Message = "Profile loaded! (￣ ▽ ￣)╭ ";
        }

        private void LoadUser(KatUserUpdate katUserUpdate)
        {
            this.DisplayName = katUserUpdate.DisplayName;
            this.Email = katUserUpdate.Email;
            this.DisplayPicture = katUserUpdate.DisplayPicture;
            this.Country = katUserUpdate.Country;
            this.CustomStatus = katUserUpdate.CustomStatus;
        }

        public static implicit operator KatUserUpdate(KatUser katUser)
        {
            return new KatUserUpdate
            {
                DisplayName = katUser.DisplayName,
                Email = katUser.Email,
                DisplayPicture = katUser.DisplayPicture,
                Country = katUser.Country,
                CustomStatus = katUser.CustomStatus
            };
        }

        public static implicit operator KatUser(KatUserUpdate katUserUpdate)
        {
            return new KatUser
            {
                DisplayName = katUserUpdate.DisplayName,
                Email = katUserUpdate.Email,
                DisplayPicture = katUserUpdate.DisplayPicture,
                Country = katUserUpdate.Country,
                CustomStatus = katUserUpdate.CustomStatus
            };
        }
    }
}
