using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LakiTool.Geo.Util;

namespace LakiTool.Geo.Util
{
    class GeoUtil
    {
        static int tabspace = 0;

        public static string getLinesFromGeo(List<GeoElem> geo)
        {
            string outLines = "";
            foreach(GeoElem elem in geo)
            {
                if (elem.type == GeoType.ListCloser) tabspace--;
                outLines += geoLineAddLineFormatted(tabspace, geoLineGetLineData(elem));
                if (elem.type == GeoType.ListOpener) { tabspace++; outLines += getLinesFromGeo(elem.geoElems); }
            }
            return outLines;
        }

        private static string geoLineGetLineData(GeoElem elem)
        {
            string linedata = "";
            if (elem.type == GeoType.Satisfied)
            {
                string paramData = "";
                foreach(GeoParam param in elem.geoParams)
                {
                    if (param.ptype == GeoParamType.Label) paramData += param.GetVal();
                    if (param.ptype == GeoParamType.Numeric) paramData += param.GetVal().ToString();
                    paramData += ", ";
                }
                paramData = paramData.Substring(0, paramData.Length - 2);
                linedata += elem.geoName + " " + paramData;
            }
            else
            {
                linedata += elem.geoName;
            }
            return linedata;
        }

        private static string geoLineAddLineFormatted(int tabspace, string linedata)
        {
            string formatted = "";
            for(int i = 0; i<tabspace; i++)
            {
                formatted += "   "; //3 spaces
            }
            formatted += linedata;
            formatted += '\n';
            return formatted;
        }

        public static List<GeoElem> getGeoFromLines(string[] lines, int k = 0)
        {
            List<GeoElem> geoElems = new List<GeoElem>();
            bool inSubNode = false;

            for (int i = k; i < lines.Length && !inSubNode; i++)
            {
                string line = lines[i];
                string[] linedata = MISCUtils.ParseAsmbd(line);
                if (linedata[0].Length < 2) linedata = linedata.Skip(1).ToArray();
                if (linedata.Length == 0) continue;
                if (linedata[0] == "geo_open_node" || linedata[0] == "glabel")
                {
                    inSubNode = true;
                    geoElems.Add(new GeoElem(linedata[0] != "glabel" ? linedata[0] : line, getGeoFromLines(lines, i + 1)));
                }
                else
                {
                    if (linedata.Length > 1)
                    {
                        geoElems.Add(new GeoElem(linedata[0], getParamsFromLineData(linedata)));
                    }
                    else
                    {
                        geoElems.Add(new GeoElem(linedata[0]));
                    }
                }
            }
            

            return geoElems;
        }

        private static List<GeoParam> getParamsFromLineData(string[] linedata)
        {
            List<GeoParam> geoparams = new List<GeoParam>();

            for(int i = 1; i < linedata.Length; i++)
            {
                dynamic paramdata;
                
                try
                {
                    paramdata = MISCUtils.ParseInt(linedata[i]);
                }
                catch
                {
                    paramdata = linedata[i];
                }

                geoparams.Add(new GeoParam(paramdata));
            }

            return geoparams;
        }
    }
}
