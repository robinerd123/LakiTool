using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Geo
{
    class GeoRefObject
    {
        private List<GeoReturnModes> returnModes = new List<GeoReturnModes>();

        public void Set(GeoReturnModes returnMode)
        {
            returnModes.Add(returnMode);
        }

        public bool hasReturnMode(GeoReturnModes returnMode)
        {
            foreach(GeoReturnModes geoReturnMode in returnModes)
            {
                if (geoReturnMode == returnMode) return true;
            }
            return false;
        }
    }
}
