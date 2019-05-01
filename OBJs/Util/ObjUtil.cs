using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
