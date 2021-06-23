using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatUser
    {
        private HttpClient _httpClient;

        public KatUser()
        {
            KatComments = new HashSet<KatComment>();
            KatPosts = new HashSet<KatPost>();
        }
        public KatUser(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and confirmation one aint matched.")]
        public string ConfirmPassword { get; set; }


        [MaxLength(13)]
        public string DisplayName { get; set; }
        public string DisplayPicture { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string Country { get; set; }
        public string CustomStatus { get; set; }
        public string AccountState { get; set; }
        public DateTime? JoinedDate { get; set; }
        public string JoinedSource { get; set; }

        public virtual ICollection<KatComment> KatComments { get; set; }
        public virtual ICollection<KatPost> KatPosts { get; set; }
    }
}
