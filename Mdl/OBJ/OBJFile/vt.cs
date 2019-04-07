using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Mdl.OBJ.OBJFile
{
    class vt
    {
        public vt(short t, short c, int x, int y)
        {
            u = (((t / 1024f) / x)*32)*-1;
            v = (((c / 1024f) / y)*32)*-1;
        }
        public float u, v;
        public string val()
        {
            return "vt " + u.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture) + " " + v.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
