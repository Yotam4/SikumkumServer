using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.Models;

namespace SikumkumServerBL.DTO
{
    public class StudyYearDTO
    {
        public StudyYearDTO()
        {

        }
        public StudyYearDTO(StudyYear sy)
        {
            this.YearId = sy.YearId;
            this.YearName = sy.YearName;
        }
        public StudyYearDTO(int id, string name)
        {
            this.YearId = id;
            this.YearName = name;
        }

        public int YearId { get; set; }
        public string YearName { get; set; }
    }
}
