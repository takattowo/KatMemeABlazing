using FluentValidation;
using System;
using System.Collections.Generic;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatUserUpdate
    {
        public string DisplayName { get; set; }
        public string DisplayPicture { get; set; }
        public string Country { get; set; }
        public string CustomStatus { get; set; }
        public string Message { get; set; }

        public void UpdateSucc()
        {
            //KatUser user = _katUserUpdate;
            //await Http.PutAsJsonAsync<KatUser>("katuser/updateprofile/1", user);
            this.Message = "Profile settings updated!";
        }

        public void GetProfile()
        {
            //KatUser user = await Http.GetFromJsonAsync<KatUser>("katuser/getprofile/1");
            //_katUserUpdate = user;
            this.Message = "Profile looaded!";
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
