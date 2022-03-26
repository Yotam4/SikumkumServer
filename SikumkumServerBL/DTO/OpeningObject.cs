using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.Models;

namespace SikumkumServerBL.DTO
{
    public class OpeningObject
    {
        public List<SubjectDTO> SubjectsList { get; set; }
        public List<FileTypeDTO> FileTypeList { get; set; }
        public List<StudyYearDTO> StudyYearList { get; set; }

        public OpeningObject() { }

        public OpeningObject(List<SubjectDTO> subjects, List<FileTypeDTO> fileTypes, List<StudyYearDTO> studyYears)
        {
            this.SubjectsList = subjects;
            this.FileTypeList = fileTypes;
            this.StudyYearList = studyYears;
        }
    }
}
