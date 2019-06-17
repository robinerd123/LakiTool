using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Drawing;


namespace LakiTool
{
    class F3DUtils
    {
        public static void renderF3DFromFile(string filePath)
        {
            string[] contents = System.IO.File.ReadAllLines(filePath);
            LUTUtil lutmanager = new LUTUtil();
            lutmanager.dldata = contents;
            lutmanager.curFile = filePath;
            lutmanager.fbpath = LakiTool.MISC.Game.gamePath;
            lutmanager.initF3D();
            ParseF3D p = new ParseF3D(F3DUtils.getF3DCommandsFromLines(contents));
            p.ParseDL(0, lutmanager.luts);
        }

        public static void fixGeomModeStart(uint geommode)
        {
            GL.End();
            GL.Color3(1.0f, 1.0f, 1.0f);
            if ((geommode & GBIConsts.G_SHADE) != 0) //checks if shading is enabled
            {
                if ((geommode & GBIConsts.G_LIGHTING) != 0) //checks if lighting is enabled (vcolor calculation)
                {//normals
                    GL.Enable(EnableCap.Light0);
                    GL.Enable(EnableCap.Lighting);
                }
                else
                {//vertcolor
                    GL.Disable(EnableCap.Light0);
                    GL.Disable(EnableCap.Lighting);
                }
            }
            GL.Begin(BeginMode.Triangles);
        }

        public static void setUpVertColor(byte r, byte g, byte b, byte a, uint geommode)
        {
            if ((geommode & GBIConsts.G_SHADE) != 0) //checks if shading is enabled
            {
                if ((geommode & GBIConsts.G_LIGHTING) != 0) //checks if lighting is enabled (vcolor calculation)
                {//normals
                    GL.Normal3(Vector3.Normalize(new Vector3(r, g, b)));
                }
                else
                {//vertcolor
                    GL.Color3((float)r/255, (float)g/255, (float)b/255);
                }
            }
            else
            {//no shading
                GL.Color3(1.0f, 1.0f, 1.0f);
            }
        }

        public static void setUpWrapST(uint wraps, uint wrapt)
        {
            if (wraps == 0) //G_TX_WRAP/G_TX_NOMIRROR
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            }
            if ((wraps & GBIConsts.G_TX_MIRROR) != 0)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat);
            }
            if ((wraps & GBIConsts.G_TX_CLAMP) != 0)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            }

            if (wrapt == 0) //G_TX_WRAP/G_TX_NOMIRROR
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            }
            if ((wrapt & GBIConsts.G_TX_MIRROR) != 0)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat);
            }
            if ((wrapt & GBIConsts.G_TX_CLAMP) != 0)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            }
        }

        public static uint getGeomModeFromLineData(string[] linedata)
        {
            //deprecated, unused
            uint geommode = 0;
            for (int c = 1; c < linedata.Length; c += 2)
            {
                foreach(GMUtil util in GBIConsts.G_MODES)
                {
                    if (linedata[c] == util.modename) geommode |= util.mode;
                }
            }
            return geommode;
        }

        public static uint getGeomModeFromParams(List<GBI.F3DParam> f3dparams)
        {
            uint geommode = 0;
            for (int c = 0; c < f3dparams.Count; c += 2)
            {
                foreach (GMUtil util in GBIConsts.G_MODES)
                {
                    if (f3dparams[c].GetVal() == util.modename) geommode |= util.mode;
                }
            }
            return geommode;
        }

        public static Bitmap getTexFromName(string tname, List<TexLUT> lut)
        {
            foreach(TexLUT lutitem in lut)
            {
                if (tname == lutitem.texSubData.label.labelName)
                {
                    return lutitem.tex;
                }
            }
            return new Bitmap(1, 1);
        }

        public static List<Vtx> getVtxFromName(string vname, List<VtxLUT> lut)
        {
            foreach (VtxLUT lutitem in lut)
            {
                if (vname == lutitem.vtxSubData.label.labelName)
                {
                    return lutitem.vtces;
                }
            }
            return new List<Vtx>();
        }

        public static float[] getLightFromName(string lname, List<LightLUT> lut)
        {
            foreach (LightLUT lutitem in lut)
            {
                if (lname == lutitem.lightSubData.label.labelName)
                {
                    return lutitem.col;
                }
            }
            return new float[]{0f,0f,0f,1f};
        }

        public static uint[] getWarpSTFromLineData(string[] linedata)
        {
            //deprecated, unused
            uint wraps = 0, wrapt = 0;
            bool checkt = true, checktc = false, wasor = false;
            for (int c = 1; c < linedata.Length; c++)
            {
                if (checktc && wasor) checkt = false;
                if (linedata[c] == "|")
                {
                    wasor = true;
                    continue;
                }
                foreach (GMUtil util in GBIConsts.G_TX_MODES)
                {
                        if (linedata[c] == util.modename)
                        {
                            if (checkt)
                            {
                                wrapt |= util.mode;
                                checktc = true;
                            }
                            else
                            {
                                wraps |= util.mode;
                            }
                        }
                }
            }
            uint[] wrapst = new uint[] { wraps, wrapt};
            return wrapst;
        }

        public static uint[] getWarpSTFromParams(List<GBI.F3DParam> f3dparams)
        {
            uint wraps = 0, wrapt = 0;
            bool checkt = true, checktc = false, wasor = false;
            for (int c = 0; c < f3dparams.Count; c++)
            {
                if (checktc && wasor) checkt = false;
                if (f3dparams[c].GetVal().ToString() == "|")
                {
                    wasor = true;
                    continue;
                }
                foreach (GMUtil util in GBIConsts.G_TX_MODES)
                {
                    if (f3dparams[c].GetVal().ToString() == util.modename)
                    {
                        if (checkt)
                        {
                            wrapt |= util.mode;
                            checktc = true;
                        }
                        else
                        {
                            wraps |= util.mode;
                        }
                    }
                }
            }
            uint[] wrapst = new uint[] { wraps, wrapt };
            return wrapst;
        }

        public static List<GBI.F3DCommand> getF3DCommandsFromLines(string[] lines)
        {
            List<GBI.F3DCommand> F3DCommands = new List<GBI.F3DCommand>();
            foreach(string line in lines)
            {
                string[] vs = MISCUtils.ParseAsmbd(line);
                foreach(string command in Enum.GetNames(typeof(GBI.GBICommand)))
                {
                    if (vs[0] == command)
                    {
                        F3DCommands.Add(new GBI.F3DCommand((GBI.GBICommand)Enum.Parse(typeof(GBI.GBICommand), command), getParamsFromStringArray(vs, 1)));
                    }
                }
            }
            return F3DCommands;
        }

        public static List<GBI.F3DParam> getParamsFromStringArray(string[] paramdata, int offset = 0)
        {
            List<GBI.F3DParam> F3DParams = new List<GBI.F3DParam>();
            for (int i = offset; i < paramdata.Length; i++)
            {
                GBI.F3DParam param;
                if (MISCUtils.IsParsableInt(paramdata[i]))
                {
                    param = new GBI.F3DParam(MISCUtils.ParseInt(paramdata[i]));
                }
                else
                {
                    param = new GBI.F3DParam(paramdata[i]);
                }
                F3DParams.Add(param);
            }
            return F3DParams;
        }

        public static int getIndexFromLineData(uint line, string[] lineData)
        {
            int index = 0;
            int n = 0;
            foreach (string lineElem in lineData)
            {
                if (n >= line) break;
                string[] vs = MISCUtils.ParseAsmbd(lineElem);
                foreach (string command in Enum.GetNames(typeof(GBI.GBICommand)))
                {
                    if (vs[0] == command)
                    {
                        index++;
                    }
                }
                n++;
            }
            return index;
        }

        public static List<GBI.Utils.LSUtil> getLabelSearchersFromLabelContainer(Labels.LabelContainer labelContainer, string[] lines)
        {
            List<GBI.Utils.LSUtil> labelsearchers = new List<GBI.Utils.LSUtil>();
            foreach(Labels.Label label in labelContainer.labels)
            {
                GBI.Utils.LSUtil labelsearcher = new GBI.Utils.LSUtil();
                labelsearcher.labelName = label.labelName;
                labelsearcher.cmd = getIndexFromLineData(label.labelLine, lines);
                labelsearchers.Add(labelsearcher);
            }
            return labelsearchers;
        }

        public static int getIndexFromLabelSearchersAndName(List<GBI.Utils.LSUtil> labelsearchers, string name)
        {
            foreach(GBI.Utils.LSUtil labelsearcher in labelsearchers)
            {
                if (labelsearcher.labelName == name) return labelsearcher.cmd;
            }
            return 0;
        }
    }
}
