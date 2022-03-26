using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.Models;

namespace SikumkumServerBL.DTO
{
    public class FileTypeDTO
    {
        public FileTypeDTO()
        {

        }
        public FileTypeDTO(int typeID, string typeName)
        {
            this.TypeId = typeID;
            this.TypeName = typeName;
        }
        public FileTypeDTO(FileType fileType)
        {
            this.TypeId = fileType.TypeId;
            this.TypeName = fileType.TypeName;
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
