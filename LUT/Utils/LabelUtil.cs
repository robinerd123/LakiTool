using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace LakiTool.LUT.Utils
{
    public class LabelUtil
    {
        const int glabelWordSize = 6;

        public static Label findLabelFromName(string name, string oFile, string rootPath = "", bool searchGlobal = false, bool searchAll = false)
        {
            Label outLabel = new Label();
            outLabel.labelName = name;
            outLabel.isGlobal = false;
            int line = 0;
            outLabel.labelFound = false;
            foreach (string l in File.ReadAllLines(oFile)) //local label search
            {
                line++;
                Label search = labelSearch(name, l);
                if (search.labelFound)
                {
                    outLabel.labelFile = oFile;
                    outLabel.labelLine = (uint)line;
                    outLabel.isGlabel = search.isGlabel;
                    outLabel.labelFound = true;
                }
            }

            if ((!outLabel.labelFound) && searchGlobal) //global label search
            {
                line = 0;
                List<string> lines = new List<string>();
                Directory.EnumerateFiles(rootPath + "/bin/", "*.s").ToList().ForEach(x => lines.Add(x));
                foreach (string llist in lines)
                {
                    line = 0;
                    string[] lines2 = File.ReadAllLines(llist);
                    foreach (string l in lines2)
                    {
                        line++;
                        Label search = labelSearch(name, l);
                        if (search.labelFound)
                        {
                            outLabel.labelFile = llist;
                            outLabel.labelLine = (uint)line;
                            outLabel.isGlabel = search.isGlabel;
                            outLabel.isGlobal = true;
                            outLabel.labelFound = true;
                        }
                    }
                }
            }
            return outLabel;
        }

        private static Label labelSearch(string name, string line)
        {
            Label searchResult = new Label();
            searchResult.labelFound = false;
            if (line.Length > name.Length && line[name.Length] == ':') //label: search
            {
                if (name == line.Substring(0, name.Length))
                {
                    searchResult.labelFound = true;
                    searchResult.isGlabel = false;
                }
            }
            if (line.Length > (name.Length + glabelWordSize) && line.Substring(0, glabelWordSize) == "glabel") //glabel search
            {
                if (line.Substring(glabelWordSize + 1, name.Length) == name)
                {
                    searchResult.labelFound = true;
                    searchResult.isGlabel = true;
                }
            }
            return searchResult;
        }
    }
}
