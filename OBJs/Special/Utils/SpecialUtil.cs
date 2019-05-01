using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.OBJs.Special.Utils
{
    class SpecialUtil
    {
        public static Special findSpecialFromPresetName(string presetName)
        {
            return findSpecialFromID(
                getPresetIdFromName(System.IO.File.ReadAllLines(MISC.Game.gamePath + "/" + SpecialConsts.specialIpFile), presetName),
                MISC.Game.gamePath
                );
        }

        public static Special findSpecialFromID(byte id, string mainPath)
        {
            foreach(Special item in getSpecialsFromFile(System.IO.File.ReadAllLines(mainPath + "/" + SpecialConsts.specialItFile)))
            {
                if (item.preset_id == id) return item;
            }
            return new Special();
        }

        public static Special[] getSpecialsFromFile(string[] fileLines)
        {
            string[][] fileLineData = LakiTool.C.Utils.CUtils.ParseFromStructInStruct(fileLines, "SpecialObjectPresets");
            Special[] specialOut = new Special[fileLineData.Length];
            for(int i = 0; i < specialOut.Length; i++)
            {
                specialOut[i] = new Special();
                specialOut[i].preset_id = (byte)MISCUtils.ParseInt(fileLineData[i][0]);
                specialOut[i].type = SpecialUtil.getTypeFromName(fileLineData[i][1]);
                specialOut[i].defParam = (byte)MISCUtils.ParseInt(fileLineData[i][2]);
                specialOut[i].modelName = fileLineData[i][3];
                specialOut[i].behaviorName = fileLineData[i][4];
            }
            return specialOut;
        }

        public static byte getTypeFromName(string typeName)
        {
            foreach (GSOUtil val in SpecialConsts.SpecialConstItems)
            {
                if (val.name == typeName)
                {
                    return val.value;
                }
            }
            return SpecialConsts.SPTYPE_NO_YROT_OR_PARAMS;
        }

        public static byte getPresetIdFromName(string[] fileLines, string modelName)
        {
            //todo change this with a sorta lut
            foreach(string line in fileLines)
            {
                string[] cont = MISCUtils.ParseAsmbd(line);
                if(cont.Length > 0)
                {
                    if (cont[0] == ".set")
                    {
                        if (cont[1] == modelName)
                        {
                            return (byte)MISCUtils.ParseInt(cont[2]);
                        }
                    }
                }
            }
            return 0x00;
        }

        public static SpecialObjectRenderStack getSpecialObjectsFromCollisionFile(string[] colData)
        {
            OBJs.Special.SpecialObjectRenderStack returnObj = new OBJs.Special.SpecialObjectRenderStack();
            string[] vals;
            foreach (string line in colData)
            {
                if (!(line.Contains("col") || line.Contains("special_object"))) continue;
                vals = MISCUtils.ParseAsmbd(line);
                if (vals[0] == "special_object")
                {
                    returnObj.addSpecialObject(vals[2], (short)MISCUtils.ParseInt(vals[4]), (short)MISCUtils.ParseInt(vals[5]), (short)MISCUtils.ParseInt(vals[6]));
                }
            }
            return returnObj;
        }
    }
}
