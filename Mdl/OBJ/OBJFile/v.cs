using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Mdl.OBJ.OBJFile
{
    class v
    {
        public v(short xA, short yA, short zA)
        {
            x = xA;
            y = yA;
            z = zA;
        }
        public short x, y, z;
        public string val()
        {
            return "v " + x.ToString() + " " + y.ToString() + " " + z.ToString();
        }
    }
}
