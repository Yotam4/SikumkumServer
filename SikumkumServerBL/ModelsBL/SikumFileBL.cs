using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.DTO;

namespace SikumkumServerBL.Models
{
    partial class SikumFile
    {
        public SikumFile() { }

        public SikumFile(SikumFileDTO uploadFile)
        {
            this.Headline = uploadFile.Headline;
            this.Approved = false;
            
        }
    }
}
