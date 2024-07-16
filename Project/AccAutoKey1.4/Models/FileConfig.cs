using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccAutoKey4.Models
{
    public class FileConfig
    {
        public FileConfig() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Show { get; set; }
        public string FileName { get; set; }
    }
}
