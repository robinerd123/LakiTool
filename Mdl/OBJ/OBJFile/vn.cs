using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Mdl.OBJ.OBJFile
{
    class vn
    {
        public vn(byte xB, byte yB, byte zB)
        {
            x = xB / 255f;
            y = yB / 255f;
            z = zB / 255f;
        }
        public float x, y, z;
        public string val()
        {
            return "vn " + x.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture) + " " + y.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture) + " " + z.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
