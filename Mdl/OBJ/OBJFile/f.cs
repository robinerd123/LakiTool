using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Mdl.OBJ.OBJFile
{
    class f
    {
        public f(fFmt fv0, fFmt fv1, fFmt fv2)
        {
            v0 = fv0;
            v1 = fv1;
            v2 = fv2;
        }
        public fFmt v0, v1, v2;
        public string val()
        {
            return "f " + v0.val() + " " + v1.val() + " " + v2.val();
        }
    }
}
