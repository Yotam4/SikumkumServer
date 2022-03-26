using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.Models;


namespace SikumkumServerBL.DTO
{
    public class SubjectDTO
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public SubjectDTO() { }

        public SubjectDTO(string name, int id)
        {
            this.SubjectId = id;
            this.SubjectName = name;
        }

        public SubjectDTO(Subject s)
        {
            this.SubjectId = s.SubjectId;
            this.SubjectName = s.SubjectName;
        }
    }
}
