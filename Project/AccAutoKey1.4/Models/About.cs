using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccAutoKey4.Models
{
    public class AboutModel
    {
        public AboutModel() { }
        public string Address { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }
        public string Record { get; set; }
        public string SN { get; set; }
    }
}
