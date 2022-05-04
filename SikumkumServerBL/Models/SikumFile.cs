using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class SikumFile
    {
        //public SikumFile()
        //{
        //    Messages = new HashSet<Message>();
        //    Ratings = new HashSet<Rating>();
        //}

        public int FileId { get; set; }
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public int TypeId { get; set; }
        public int YearId { get; set; }
        public bool Approved { get; set; }
        public bool Disapproved { get; set; }
        public string Headline { get; set; }
        public string TextDesc { get; set; }
        public string Url { get; set; }
        public double FileRating { get; set; }
        public int NumOfFiles { get; set; }
        public bool HasImage { get; set; }
        public bool HasPdf { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual FileType Type { get; set; }
        public virtual User User { get; set; }
        public virtual StudyYear Year { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
