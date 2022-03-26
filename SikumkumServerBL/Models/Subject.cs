using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class Subject
    {
        public Subject()
        {
            SikumFiles = new HashSet<SikumFile>();
        }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<SikumFile> SikumFiles { get; set; }
    }
}
