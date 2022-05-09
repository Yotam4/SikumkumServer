﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.DTO;

namespace SikumkumServerBL.Models
{
    partial class Rating
    {
        public Rating() { }

        public Rating(RatingDTO ratingDTO)
        {
            this.RatingId = ratingDTO.RatingId;
            this.RatingGiven = ratingDTO.RatingGiven;
            this.FileId = ratingDTO.FileId;
            this.UserId = ratingDTO.UserId;
        }
    }
}