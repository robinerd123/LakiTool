using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Geo
{
    enum GeoReturnModes
    {
        Irrelevant,
        Rotation,
        DefXZ,
        Translation,
        Scaling,
        GeoDL,
        OpensNode,
        ClosesNode,
        GeoSwitch,
        GeoBillBoard
    }

    class GeoUtil
    {
        public string[] dldata = new string[0];
        public string fbpath = "";
        public string curFile = "";
        
        string[] vals = new string[0];

        public List<GeoObject> GetGeoObjectsFromGeoElems(List<GeoElem> geoElems, string labelName, GeoObject prevObject = null)
        {
            List<GeoObject> geoObjects = new List<GeoObject>();
            GeoObject curObject;
            if (prevObject == null) curObject = new GeoObject(); else curObject = prevObject;

            foreach (GeoElem geoElem in geoElems)
            {
                if (labelName != null)
                {
                    string[] name = MISCUtils.ParseAsmbd(geoElem.geoName);
                    if (name[0] == "glabel")
                    {
                        if (name[1] != labelName) continue;
                    }
                }

                //if (geoElem.type == GeoType.ListCloser) curObject = new GeoObject();
                GeoCMDs CMD = new GeoCMDs();
                CMD.elem = geoElem;
                curObject = CMD.loadGeoObject(CMD.getRefObject(), curObject);
                if (curObject.f3d.fileName != null)
                {
                    geoObjects.Add(curObject);
                    curObject = new GeoObject(); //to be removed
                }
                if (geoElem.type == GeoType.ListOpener)
                {
                    geoObjects.AddRange(GetGeoObjectsFromGeoElems(geoElem.geoElems, labelName, curObject));
                }
            }

            return geoObjects;
        }
    }
}
