using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Mdl.OBJ.OBJFile
{
    class fFmt
    {
        public fFmt(uint vInd, uint vtInd, uint vnInd)
        {
            v = vInd;
            vt = vtInd;
            vn = vnInd;
        }
        public uint v, vt, vn;
        public string val()
        {
            return v.ToString() + "/" + vt.ToString() + "/" + vn.ToString();
        }
    }
}
