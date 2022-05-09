using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class FileType
    {
        public FileType()
        {
            SikumFiles = new List<SikumFile>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual List<SikumFile> SikumFiles { get; set; }
    }
}
