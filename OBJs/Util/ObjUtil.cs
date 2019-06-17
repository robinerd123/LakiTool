using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;

namespace LakiTool.OBJs.Util
{
    class ObjUtil
    {
        public static string getGeoNameFromModelName(string modelName)
        {
            string[] refLines = System.IO.File.ReadAllLines(MISC.Game.gamePath + "/" + GeneralObjConsts.mdlRefFile);
            //todo change this with a sorta lut
            foreach (string line in refLines)
            {
                string[] cont = MISCUtils.ParseAsmbd(line);
                if (cont.Length > 0)
                {
                    if (cont[0] == ".set")
                    {
                        if (cont[1] == modelName)
                        {
                            if (cont.Length > 4)
                            {
                                return cont[4]; //also todo do this better instead of reading comment lol
                            }
                            else
                            {
                                return "bubbly_tree_geo";
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static OBJs.CubeData getCubeDataFromF3DFile(string[] lines) //yeah sorry i make it check the whole file, just did this for convenience and also cause objects should be more accurately represented
        {
            CubeData data = new CubeData();
            string[] vs;
            if (lines == null) return data;
            foreach (string line in lines)
            {
                vs = MISCUtils.ParseAsmbd(line);
                if (vs[0] == "vertex")
                {
                    data.updateCubeData((short)(MISCUtils.ParseInt(vs[1]) >> 4), (short)(MISCUtils.ParseInt(vs[2]) >> 4), (short)(MISCUtils.ParseInt(vs[3]) >> 4));
                }
            }
            return data;
        }

        public static void setUpCubeDataRendering(float r, float g, float b)
        {
            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Texture2D);
            GL.Color3(r, g, b);
        }
    }
}
