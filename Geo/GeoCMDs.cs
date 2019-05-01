using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Geo
{
    class GeoCMDs
    {
        const short MiniGeoDLsz = 3, StandardGeoDLsz = 5, DoubleGeoDLsz = 8;

        public GeoObject loadGeoObject(GeoRefObject refObject, GeoObject geoObject)
        {
            GeoObject returnObject = geoObject;

            if (refObject.hasReturnMode(GeoReturnModes.ClosesNode))
            {
                //todo
            }
            if (refObject.hasReturnMode(GeoReturnModes.OpensNode))
            {
                //todo
            }
            if (refObject.hasReturnMode(GeoReturnModes.Scaling))
            {
                //geo scaling
                returnObject.objScale.SetScale((uint)elem.geoParams[1].GetVal());
            }
            if (refObject.hasReturnMode(GeoReturnModes.Translation) || refObject.hasReturnMode(GeoReturnModes.Rotation))
            {
                if (refObject.hasReturnMode(GeoReturnModes.Translation) && refObject.hasReturnMode(GeoReturnModes.Rotation))
                {
                    //geo translation and rotation
                    returnObject.objTrans.xTrans += (short)elem.geoParams[1].GetVal();
                    returnObject.objTrans.yTrans += (short)elem.geoParams[2].GetVal();
                    returnObject.objTrans.zTrans += (short)elem.geoParams[3].GetVal();

                    returnObject.objRot.xRot += (short)elem.geoParams[4].GetVal();
                    returnObject.objRot.yRot += (short)elem.geoParams[5].GetVal();
                    returnObject.objRot.zRot += (short)elem.geoParams[6].GetVal();
                }
                else
                {
                    if (refObject.hasReturnMode(GeoReturnModes.Translation))
                    {
                        //geo translation
                        returnObject.objTrans.xTrans += (short)elem.geoParams[1].GetVal();
                        returnObject.objTrans.yTrans += (short)elem.geoParams[2].GetVal();
                        returnObject.objTrans.zTrans += (short)elem.geoParams[3].GetVal();
                    }
                    if (refObject.hasReturnMode(GeoReturnModes.Rotation))
                    {
                        //geo rotation
                        returnObject.objRot.xRot += (short)elem.geoParams[1].GetVal();
                        returnObject.objRot.yRot += (short)elem.geoParams[2].GetVal();
                        returnObject.objRot.zRot += (short)elem.geoParams[3].GetVal();
                    }
                }
            }

            if (refObject.hasReturnMode(GeoReturnModes.GeoDL)) returnObject.f3d.SetInitialDataFromLabelName(elem.geoParams[elem.geoParams.Count-1].GetVal());
            return returnObject;
        }

        public GeoRefObject getRefObject()
        {
            GeoRefObject returnRefObject = new GeoRefObject();
            switch (elem.geoName)
            {
                case "geo_close_node":
                    returnRefObject.Set(GeoReturnModes.ClosesNode);
                    break;
                case "geo_open_node":
                    returnRefObject.Set(GeoReturnModes.OpensNode);
                    break;
                case "geo_dl_translated":
                case "geo_translate":
                case "geo_translate_node":
                    returnRefObject.Set(GeoReturnModes.Translation);
                    if (elem.geoParams.Count == StandardGeoDLsz) returnRefObject.Set(GeoReturnModes.GeoDL);
                    break;
                case "geo_translate_rotate":
                    returnRefObject.Set(GeoReturnModes.Translation);
                    returnRefObject.Set(GeoReturnModes.Rotation);
                    if (elem.geoParams.Count == DoubleGeoDLsz) returnRefObject.Set(GeoReturnModes.GeoDL);
                    break;
                case "geo_rotate_y":
                    returnRefObject.Set(GeoReturnModes.Rotation);
                    returnRefObject.Set(GeoReturnModes.DefXZ);
                    if (elem.geoParams.Count == MiniGeoDLsz) returnRefObject.Set(GeoReturnModes.GeoDL);
                    break;
                case "geo_rotate":
                case "geo_rotation_node":
                    returnRefObject.Set(GeoReturnModes.Rotation);
                    if (elem.geoParams.Count == StandardGeoDLsz) returnRefObject.Set(GeoReturnModes.GeoDL);
                    break;
                case "geo_scale":
                    returnRefObject.Set(GeoReturnModes.Scaling);
                    if (elem.geoParams.Count == MiniGeoDLsz) returnRefObject.Set(GeoReturnModes.GeoDL);
                    break;
                case "geo_display_list":
                    returnRefObject.Set(GeoReturnModes.GeoDL);
                    break;
                default:
                    returnRefObject.Set(GeoReturnModes.Irrelevant);
                    break;
            }
            return returnRefObject;
        }

        public GeoElem elem;
    }
}
