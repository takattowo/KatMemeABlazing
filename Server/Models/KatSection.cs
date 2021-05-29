using System;
using System.Collections.Generic;

#nullable disable

namespace KatMemeABlazing.Server.Models
{
    public partial class KatSection
    {
        public KatSection()
        {
            KatPosts = new HashSet<KatPost>();
        }

        public int Id { get; set; }
        public string SectionName { get; set; }
        public string SectionDescription { get; set; }
        public string SectionPicture { get; set; }

        public virtual ICollection<KatPost> KatPosts { get; set; }
    }
}
