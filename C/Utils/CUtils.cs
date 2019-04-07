using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.C.Utils
{
    class CUtils
    {
        public static int getValFromDefineName(string[] fileLines, string name)
        {
            GCUtil[] nd = CUtils.GetDefinesFromFile(fileLines);
            foreach(GCUtil elem in nd)
            {
                if (elem.name == name) return elem.value;
            }
            return 0;
        }

        public static string getDefineNameFromVal(string[] fileLines, int val)
        {
            GCUtil[] nd = CUtils.GetDefinesFromFile(fileLines);
            foreach (GCUtil elem in nd)
            {
                if (elem.value == val) return elem.name;
            }
            return "";
        }

        public static GCUtil[] GetDefinesFromFile(string[] fileLines)
        {
            List<GCUtil> gcs = new List<GCUtil>();
            string[] lineData;
            foreach(string line in fileLines)
            {
                lineData = MISCUtils.ParseAsmbd(line);
                if (lineData[0] == C.CConsts.def)
                {
                    gcs.Add(new GCUtil(lineData[1], MISCUtils.ParseInt(lineData[2])));
                    System.Windows.Forms.MessageBox.Show(gcs[gcs.Count-1].name);
                }
            }

            return gcs.ToArray();
        }

        public static string[][] ParseFromStructInStruct(string[] fileLines, string structName)
        {
            List<string[]> outData = new List<string[]>();
            bool parsingStruct = false;
            foreach(string line in fileLines)
            {
                if (parsingStruct) {
                    if (line.Contains("{") && line.Contains("}"))
                    {
                        outData.Add(MISCUtils.ParseAsmbd(line.Split('{')[1].Split('}')[0]));
                    }
                    else parsingStruct = false;
                }
                if (line.Contains(structName))
                {
                    parsingStruct = true;
                }
            }
            return outData.ToArray();
        }
    }
}
