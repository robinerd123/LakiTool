using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.C.Utils
{
    class GCUtil
    {
        public GCUtil(string defname, int defvalue)
        {
            name = defname;
            value = defvalue;
        }
        public int value;
        public string name;
    }
}
