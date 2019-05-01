using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Col.Util
{
    class GSUtil
    {
        public GSUtil(string constname, short constvalue)
        {
            name = constname;
            value = constvalue;
        }
        public short value;
        public string name;
    }
}
