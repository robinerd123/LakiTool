using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Geo
{
    enum GeoType
    {
        ListOpener,
        ListCloser,
        Satisfied,
        SatisfiedNoParams
    }

    class GeoElem
    {
        public GeoElem(string name, List<GeoElem> geoelems)
        {
            type = GeoType.ListOpener;
            geoElems = geoelems;
            geoName = name;
        }

        public GeoElem(string name, List<GeoParam> geoparams)
        {
            type = GeoType.Satisfied;
            geoParams = geoparams;
            geoName = name;
        }

        public GeoElem(string name)
        {
            type = GeoType.SatisfiedNoParams;
            if (name == "geo_close_node") type = GeoType.ListCloser;
            geoName = name;
        }

        public GeoType type;

        public string geoName;
        public List<GeoElem> geoElems = new List<GeoElem>();
        public List<GeoParam> geoParams = new List<GeoParam>();
    }
}
