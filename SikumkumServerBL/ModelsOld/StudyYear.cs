using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class StudyYear
    {
        public int YearId { get; set; }
        public string YearName { get; set; }

        public virtual SikumFile SikumFile { get; set; }
    }
}
