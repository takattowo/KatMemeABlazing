using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KatMemeABlazing.Shared.Models;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatUserLogin
    {
        private HttpClient _httpClient;

        public KatUserLogin()
        {

        }
        public KatUserLogin(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoginUser()
        {
            await _httpClient.PostAsJsonAsync<KatUser>("katuser/loginuser", this);
        }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and confirmation one aint matched.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string AccountState { get; set; }


        public static implicit operator KatUserLogin(KatUser katUser)
        {
            return new KatUserLogin
            {
                Password = katUser.Password,
                Email = katUser.Email
            };
        }

        public static implicit operator KatUser(KatUserLogin katUserLogin)
        {
            return new KatUser
            {
                Password = katUserLogin.Password,
                Email = katUserLogin.Email
            };
        }
    }
}
