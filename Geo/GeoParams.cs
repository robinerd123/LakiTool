using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Geo
{
    enum GeoParamType
    {
        Numeric,
        Label
    }

    class GeoParam
    {
        public GeoParamType ptype;

        private int nval;
        private string sval;

        public GeoParam(int numeric)
        {
            ptype = GeoParamType.Numeric;
            nval = numeric;
        }

        public GeoParam(string label)
        {
            ptype = GeoParamType.Label;
            sval = label;
        }

        public dynamic GetVal()
        {
            if (ptype == GeoParamType.Label) return sval;
            if (ptype == GeoParamType.Numeric) return nval;
            return null;
        }
    }
}
