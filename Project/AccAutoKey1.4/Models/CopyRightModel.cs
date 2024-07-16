using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccAutoKey4.Models
{
    public class CopyRightModel
    {
        public CopyRightModel() { }
        public string Title { get; set; }
        //public List<Row> Rows { get; set; }

    }

    public class Row
    {
        public string Item { get; set; }
    }
}
