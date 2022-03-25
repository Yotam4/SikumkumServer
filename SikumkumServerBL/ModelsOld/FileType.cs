using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class FileType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual SikumFile SikumFile { get; set; }
    }
}
