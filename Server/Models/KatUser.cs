using System;
using System.Collections.Generic;

#nullable disable

namespace KatMemeABlazing.Server.Models
{
    public partial class KatUser
    {
        public KatUser()
        {
            KatComments = new HashSet<KatComment>();
            KatPosts = new HashSet<KatPost>();
        }

        public int Id { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string DisplayPicture { get; set; }
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
