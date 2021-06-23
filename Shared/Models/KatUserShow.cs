using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace KatMemeABlazing.Shared.Models
{
    public partial class KatUserShow
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string DisplayPicture { get; set; }

    }
}
