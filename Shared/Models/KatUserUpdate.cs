using FluentValidation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatUserUpdate
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string DisplayPicture { get; set; }
        public string Country { get; set; }
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

        public void UpdateSucc()
        {
            //KatUser user = _katUserUpdate;
            //await Http.PutAsJsonAsync<KatUser>("katuser/updateprofile/1", user);
            this.Message = "Profile settings updated!";
        }

        public async Task GetProfile()
        {
            KatUser user = await _httpClient.GetFromJsonAsync<KatUser>("katuser/getprofile/" + Id);
            LoadUser(user);
            this.Message = "Profile looaded!";
        }

        private void LoadUser(KatUserUpdate katUserUpdate)
        {
            this.DisplayName = katUserUpdate.DisplayName;
            this.DisplayPicture = katUserUpdate.DisplayPicture;
            this.Country = katUserUpdate.Country;
            this.CustomStatus = katUserUpdate.CustomStatus;
            this.Email = katUserUpdate.Email;
        }

        public static implicit operator KatUserUpdate(KatUser katUser)
        {
            return new KatUserUpdate
            {
                DisplayName = katUser.DisplayName,
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
                DisplayPicture = katUserUpdate.DisplayPicture,
                Country = katUserUpdate.Country,
                CustomStatus = katUserUpdate.CustomStatus
            };
        }
    }

    public class KatUserUpdateValidator : AbstractValidator<KatUserUpdate>
    {
        public KatUserUpdateValidator()
        {
            RuleFor(x => x.DisplayName).Length(2, 13);
            RuleFor(x => x.CustomStatus).Length(0, 50);
        }
    }
}
