using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class Subject
    {
        public Subject()
        {
            SikumFiles = new List<SikumFile>();
        }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public virtual List<SikumFile> SikumFiles { get; set; }
    }
}
