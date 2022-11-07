using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndClassLibrary
{
    public class Class1
    {
        public int Id { get; set; }
        public Class2 class2 { get; set; }
        
        public string GenerateError()
        {
            var c = this;
            var output = c.class2.Data;
            return output;
        }
    }
}