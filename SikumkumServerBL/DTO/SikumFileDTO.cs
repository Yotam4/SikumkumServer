﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SikumkumServerBL.DTO
{
    class SikumFileDTO
    {
        public string Username { get; set; }
        public string TypeName { get; set; }
        public string YearName { get; set; }
        public string Headline { get; set; }
        public string TextDesc { get; set; }
        public string Url { get; set; }

        public SikumFileDTO(string username, string headline, string url, string yearName, string typeName, string textDesc)
        {
            this.Username = username;
            this.Headline = headline;
            this.Url = url;
            this.YearName = yearName;
            this.TypeName = typeName;
            this.TextDesc = textDesc;
        }
    }
}
