using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.Models;

namespace SikumkumServerBL.DTO
{
    public class SikumFileDTO
    {
        public string Username { get; set; }
        public int UserID { get; set; }
        public string TypeName { get; set; }
        public string YearName { get; set; }
        public int TypeID { get; set; }
        public int YearID { get; set; }
        public int SubjectID { get; set; }
        public string Headline { get; set; }
        public string TextDesc { get; set; }
        public string Url { get; set; }
        public bool HasPdf { get; set; }
        public bool HasImage { get; set; }

        public SikumFileDTO() { }

        public SikumFileDTO(int userID, string username, string headline, string url, string typeName, string yearName, int yearID, int typeID, int subjectID, string textDesc, bool hasPDF, bool hasImage)
        {
            this.UserID = userID;
            this.Username = username;
            this.Headline = headline;
            this.TextDesc = textDesc;
            this.Url = url;
            this.TypeName = typeName;
            this.YearName = yearName;
            this.YearID = yearID;
            this.TypeID = typeID;
            this.SubjectID = subjectID;
            this.HasPdf = HasPdf;
            this.HasImage = HasImage;
        }

        public SikumFileDTO(SikumFile file)
        {

            this.UserID = file.UserId;
            this.Username = file.User.Username;
            this.Headline = file.Headline;
            this.TextDesc = file.TextDesc;
            this.TypeName = file.Type.TypeName;
            this.YearName = file.Year.YearName;
            this.Url = file.Url;
            this.YearID = file.YearId;
            this.TypeID = file.TypeId;
            this.SubjectID = file.SubjectId;
            this.HasImage = file.HasImage;
            this.HasPdf = file.HasPdf;
        }
    }
}
