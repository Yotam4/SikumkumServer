using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public double Rating1 { get; set; }

        public virtual SikumFile File { get; set; }
        public virtual User User { get; set; }
    }
}
