using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;
using System.Text;
using LakiTool;
using System.Threading.Tasks;

namespace LakiTool
{
    class ParseF3D
    {
        private string[] dldata = new string[0];
        public ParseF3D(string[] input)
        {
            dldata = input;
        }

        string[] vals = new string[0];
        public void ParseDL(int offset, GBI GBIc, LUTs LUTc)
        {
            int n = -1;
            foreach (string line in dldata)
            {
                n++;
                if (n < offset-1) continue;
                if (!(line.Contains("gsSP") || line.Contains("gsDP"))) continue;
                vals = MISCUtils.ParseAsmbd(line);
                if (vals[0] == "gsSPEndDisplayList" && offset>0)
                {
                    return;
                }
                if (vals[0] == "gsSP1Triangle")
                {
                    GBIc.gsSP1Triangle(
                                        MISCUtils.ParseInt(vals[1]),
                                        MISCUtils.ParseInt(vals[2]), 
                                        MISCUtils.ParseInt(vals[3]), 
                                        MISCUtils.ParseInt(vals[4])
                                        );
                }
                if (vals[0] == "gsSP2Triangles")
                {
                    GBIc.gsSP2Triangles(
                                        MISCUtils.ParseInt(vals[1]), 
                                        MISCUtils.ParseInt(vals[2]), 
                                        MISCUtils.ParseInt(vals[3]), 
                                        MISCUtils.ParseInt(vals[4]), 
                                        MISCUtils.ParseInt(vals[5]), 
                                        MISCUtils.ParseInt(vals[6]), 
                                        MISCUtils.ParseInt(vals[7]), 
                                        MISCUtils.ParseInt(vals[8])
                                        );
                }
                if(vals[0] == "gsSPClearGeometryMode")
                {
                    GBIc.gsSPClearGeometryMode(F3DUtils.getGeomModeFromLineData(vals));
                }
                if (vals[0] == "gsSPSetGeometryMode")
                {
                    GBIc.gsSPSetGeometryMode(F3DUtils.getGeomModeFromLineData(vals));
                }
                if (vals[0] == "gsSPVertex")
                {
                    GBIc.gsSPVertex(F3DUtils.getVtxFromName(vals[1], LUTc.vtxLUT).ToArray(), (byte)MISCUtils.ParseInt(vals[2]), (byte)MISCUtils.ParseInt(vals[3]));
                }
                if (vals[0] == "gsSPLight")
                {
                    GBIc.gsSPLight(F3DUtils.getLightFromName(vals[1], LUTc.lightLUT), (byte)MISCUtils.ParseInt(vals[2]));
                }
                if(vals[0] == "gsDPSetEnvColor")
                {
                    GBIc.gsDPSetEnvColor(
                                            (byte)MISCUtils.ParseInt(vals[1]), //r
                                            (byte)MISCUtils.ParseInt(vals[2]), //g
                                            (byte)MISCUtils.ParseInt(vals[3]), //b
                                            (byte)MISCUtils.ParseInt(vals[4])  //a
                                            );
                }
                if (vals[0] == "gsDPSetTextureImage")
                {
                    GBIc.gsDPSetTextureImage(F3DUtils.getTexFromName(vals[4], LUTc.texLUT));
                }
                if (vals[0] == "gsDPSetTile")
                {
                    GBIc.gsDPSetTile(F3DUtils.getWarpSTFromLineData(vals));
                }
            }
            
        }
    }
}
