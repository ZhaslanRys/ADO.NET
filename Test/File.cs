using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class File
    {
        public int Id_file { get; set; }
        public int Id_person { get; set; }
        public string Name_file { get; set; }
        public string Content_file { get; set; }
        public byte[] Data_file { get; set; }
    }
}
