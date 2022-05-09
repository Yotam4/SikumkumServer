using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SikumkumServerBL.DTO
{
    public class RatingDTO
    {
        public int RatingId { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public double RatingGiven { get; set; }

        public RatingDTO() { }
    }
}
