using System;
using System.Collections.Generic;
using SikumkumServerBL.DTO;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class SikumFile
    {
        public int FileId { get; set; }
        public string Username { get; set; }
        public int ChatBoxId { get; set; }
        public int SubjectId { get; set; }
        public int TypeId { get; set; }
        public int YearId { get; set; }
        public bool Approved { get; set; }
        public string Headline { get; set; }
        public string TextDesc { get; set; }
        public string Url { get; set; }

        public virtual Chat ChatBox { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual FileType Type { get; set; }
        public virtual StudyYear Year { get; set; }
        


        public SikumFile()
        {

        }
                
    }
}
