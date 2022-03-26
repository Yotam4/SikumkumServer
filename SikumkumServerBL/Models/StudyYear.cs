using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class StudyYear
    {
        public StudyYear()
        {
            SikumFiles = new HashSet<SikumFile>();
        }

        public int YearId { get; set; }
        public string YearName { get; set; }

        public virtual ICollection<SikumFile> SikumFiles { get; set; }
    }
}
