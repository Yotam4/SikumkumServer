﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public virtual SikumFile SikumFile { get; set; }

        public Subject() { }
    }
}
