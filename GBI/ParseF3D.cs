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
    enum SpecialRendering
    {
        Off,
        SEQ
    }

    class ParseF3D
    {
        private Labels.LabelContainer JumpContainer;
        private List<GBI.F3DCommand> dldata = new List<GBI.F3DCommand>();

        public ParseF3D(List<GBI.F3DCommand> input, Labels.LabelContainer DLJumpContainer = null)
        {
            JumpContainer = DLJumpContainer;
            dldata = input;
        }
        
        public void ParseDL(int offset, LUTs LUTc, SpecialRendering specialRendering = SpecialRendering.Off, List<GBI.Utils.LSUtil> labelsearchers = null, GBI.GBI GBIc = null)
        {
            if(GBIc==null) GBIc = new GBI.GBI();
            GBI.F3DCommand dlelem;
            for (int n = offset; n < dldata.Count; n++)
            {
                dlelem = dldata[n];
                switch (dlelem.Command)
                {
                    case GBI.GBICommand.gsSPEndDisplayList:
                        if (specialRendering == SpecialRendering.SEQ) break;
                        return;

                    case GBI.GBICommand.gsSPDisplayList:
                        if (specialRendering == SpecialRendering.SEQ) break;
                        ParseDL(F3DUtils.getIndexFromLabelSearchersAndName(labelsearchers, dlelem.Params[0].GetVal()), LUTc, SpecialRendering.Off, null, GBIc);
                        break;

                    case GBI.GBICommand.gsSP1Triangle:
                        GBIc.gsSP1Triangle(
                                            dlelem.Params[0].GetVal(),
                                            dlelem.Params[1].GetVal(),
                                            dlelem.Params[2].GetVal(),
                                            dlelem.Params.Count > 3 ? dlelem.Params[3].GetVal() : 0
                                            );
                        break;

                    case GBI.GBICommand.gsSP2Triangles:
                        GBIc.gsSP2Triangles(
                                            dlelem.Params[0].GetVal(),
                                            dlelem.Params[1].GetVal(),
                                            dlelem.Params[2].GetVal(),
                                            dlelem.Params[3].GetVal(),
                                            dlelem.Params[4].GetVal(),
                                            dlelem.Params[5].GetVal(),
                                            dlelem.Params[6].GetVal(),
                                            dlelem.Params.Count > 7 ? dlelem.Params[7].GetVal() : 0
                                            );
                        break;

                    case GBI.GBICommand.gsSPClearGeometryMode:
                        GBIc.gsSPClearGeometryMode(F3DUtils.getGeomModeFromParams(dlelem.Params));
                        break;

                    case GBI.GBICommand.gsSPSetGeometryMode:
                        GBIc.gsSPSetGeometryMode(F3DUtils.getGeomModeFromParams(dlelem.Params));
                        break;

                    case GBI.GBICommand.gsSPVertex:
                        GBIc.gsSPVertex(F3DUtils.getVtxFromName(dlelem.Params[0].GetVal(), LUTc.vtxLUT).ToArray(), (byte)dlelem.Params[1].GetVal(), (byte)dlelem.Params[2].GetVal());
                        break;

                    case GBI.GBICommand.gsSPLight:
                        GBIc.gsSPLight(F3DUtils.getLightFromName(dlelem.Params[0].GetVal(), LUTc.lightLUT), (byte)dlelem.Params[1].GetVal());
                        break;

                    case GBI.GBICommand.gsDPSetEnvColor:
                        GBIc.gsDPSetEnvColor(
                                                (byte)dlelem.Params[0].GetVal(), //r
                                                (byte)dlelem.Params[1].GetVal(), //g
                                                (byte)dlelem.Params[2].GetVal(), //b
                                                (byte)dlelem.Params[3].GetVal()  //a
                                                );
                        break;

                    case GBI.GBICommand.gsDPSetTextureImage:
                        GBIc.gsDPSetTextureImage(F3DUtils.getTexFromName(dlelem.Params[3].GetVal(), LUTc.texLUT));
                        break;

                    case GBI.GBICommand.gsDPLoadTextureBlock:
                        GBIc.gsDPSetTextureImage(F3DUtils.getTexFromName(dlelem.Params[0].GetVal(), LUTc.texLUT));
                        break;

                    case GBI.GBICommand.gsDPSetTile:
                        GBIc.gsDPSetTile(F3DUtils.getWarpSTFromParams(dlelem.Params));
                        break;
                }
            }
            
        }
    }
}
