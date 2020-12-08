using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    public class Function
    {
        public string function;
        public bool valid;

        public Function(string function)
        {
            this.function = function;
            valid = true;
        }
    }
}
