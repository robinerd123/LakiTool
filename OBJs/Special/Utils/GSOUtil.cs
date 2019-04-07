using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.OBJs.Special.Utils
{
    class GSOUtil
    {
        public GSOUtil(string specialconstname, byte specialconstvalue)
        {
            name = specialconstname;
            value = specialconstvalue;
        }
        public byte value;
        public string name;
    }
}
