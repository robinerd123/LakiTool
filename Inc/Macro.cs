using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Include
{
    class Macro
    {
        public Macro(string macroName, dynamic macroValue)
        {
            name = macroName;
            value = macroValue;
        }
        public string name;
        public dynamic value;
    }
}
