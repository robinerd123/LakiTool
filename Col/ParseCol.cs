using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Col
{
    class ParseCol
    {
        private string[] colData;

        public ParseCol(string[] lines)
        {
            colData = lines;
        }

        public void ParseColData()
        {
            int n = -1;
            string[] vals;
            Col cold = new Col();
            foreach (string line in colData)
            {
                n++;
                if (!(line.Contains("col") || line.Contains("special_object"))) continue;
                vals = MISCUtils.ParseAsmbd(line);
                if (vals[0] == "colVertex")
                {
                    cold.colVertex((short)MISCUtils.ParseInt(vals[1]), (short)MISCUtils.ParseInt(vals[2]), (short)MISCUtils.ParseInt(vals[3]));
                }
                if (vals[0] == "colTri")
                {
                    cold.colTri((short)MISCUtils.ParseInt(vals[1]), (short)MISCUtils.ParseInt(vals[2]), (short)MISCUtils.ParseInt(vals[3]));
                }
                if (vals[0] == "colTriSpecial")
                {
                    cold.colTriSpecial((short)MISCUtils.ParseInt(vals[1]), (short)MISCUtils.ParseInt(vals[2]), (short)MISCUtils.ParseInt(vals[3]), (short)MISCUtils.ParseInt(vals[4]));
                }
                if (vals[0] == "colWaterBox")
                {
                    cold.colWaterBox((short)MISCUtils.ParseInt(vals[1]), (short)MISCUtils.ParseInt(vals[2]), (short)MISCUtils.ParseInt(vals[3]), (short)MISCUtils.ParseInt(vals[4]), (short)MISCUtils.ParseInt(vals[5]), (short)MISCUtils.ParseInt(vals[6]));
                }
                if (vals[0] == "colTrisInit")
                {
                    cold.colTrisInit(LakiTool.Col.Util.ColUtil.getCmdFromName(vals[1]), (short)MISCUtils.ParseInt(vals[2]));
                }
            }
        }
        
    }
}
