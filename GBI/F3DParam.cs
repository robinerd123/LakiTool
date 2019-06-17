using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.GBI
{

    enum F3DParamType
    {
        Numeric,
        String
    }

    class F3DParam
    {
        public F3DParamType ptype;

        private int nval;
        private string sval;

        public F3DParam(int numeric)
        {
            ptype = F3DParamType.Numeric;
            nval = numeric;
        }

        public F3DParam(string stringval)
        {
            ptype = F3DParamType.String;
            sval = stringval;
        }

        public dynamic GetVal()
        {
            if (ptype == F3DParamType.String) return sval;
            if (ptype == F3DParamType.Numeric) return nval;
            return null;
        }
    }    
}
