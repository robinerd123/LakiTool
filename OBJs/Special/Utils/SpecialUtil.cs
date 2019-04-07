using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.OBJs.Special.Utils
{
    class SpecialUtil
    {
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
    }
}
