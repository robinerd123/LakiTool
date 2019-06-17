using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Col.Util
{
    class ColUtil
    {
        public static string getNameFromCmd(short cmd)
        {
            foreach (GSUtil val in ColConsts.ColConstVals)
            {
                if (val.value == cmd)
                {
                    return val.name;
                }
            }
            return null;
        }

        public static short getCmdFromName(string name)
        {
            foreach (GSUtil val in ColConsts.ColConstVals)
            {
                if (val.name == name)
                {
                    return val.value;
                }
            }
            return 0;
        }
    }
}
