using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class FileType
    {
        public FileType()
        {
            SikumFiles = new HashSet<SikumFile>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<SikumFile> SikumFiles { get; set; }
    }
}
