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
                    GL.Color4(r, g, b, a);
                }
            }
            else
            {//no shading
                GL.Color4((byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF);
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

        public static Bitmap getTexFromName(string tname, List<TexLUT> lut)
        {
            foreach(TexLUT lutitem in lut)
            {
                if (tname == lutitem.name)
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
                if (vname == lutitem.name)
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
                if (lname == lutitem.name)
                {
                    return lutitem.col;
                }
            }
            return new float[]{0f,0f,0f,1f};
        }

        public static uint[] getWarpSTFromLineData(string[] linedata)
        {
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

    }
}
