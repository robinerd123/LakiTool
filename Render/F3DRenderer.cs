using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace LakiTool.Render
{
    class F3DRenderer
    {
        private bool inited = false;

        public bool textured = true;
        public bool seq = false;
        public bool manualRendering = false;

        public string fileName = null;
        public string[] lines = new string[0];
        public uint startLine;
        public ParseF3D parser;
        public LUTs LUTc = new LUTs();
        public LUTUtil lutmanager = new LUTUtil();
        public List<int> torender = new List<int>();
        public List<Labels.Label> f3dlabels = new List<Labels.Label>();

        public void SetInitialDataFromLabelName(string labelname)
        {
            Labels.Label f3dlabel = new Labels.Label();
            f3dlabel = Labels.Utils.LabelUtil.findLabelFromName(labelname, "", LakiTool.MISC.Game.gamePath, true);
            if (f3dlabel.labelFound) {
                fileName = f3dlabel.labelFile;
                lines = System.IO.File.ReadAllLines(f3dlabel.labelFile);
                startLine = f3dlabel.labelLine;
            }
        }

        public void Init()
        {
            inited = true;
            lutmanager.dldata = lines;
            lutmanager.curFile = fileName;
            lutmanager.fbpath = LakiTool.MISC.Game.gamePath;
            lutmanager.initF3D();
            LUTc = lutmanager.luts;
            parser = new ParseF3D(lines, lutmanager.jumpContainer);
            f3dlabels = Labels.Utils.LabelUtil.getLabelListFromModelFile(fileName);
        }

        public void Render()
        {
            if (!inited) return;
            if (!textured) GL.Disable(EnableCap.Texture2D); else GL.Enable(EnableCap.Texture2D);
            GL.Begin(BeginMode.Triangles);
            if (!manualRendering)
            {
                parser.ParseDL(startLine, LUTc, SpecialRendering.Off, f3dlabels);
            }
            else
            {
                if (!seq)
                {
                    foreach (int torenderitems in torender)
                    {
                        GL.Begin(BeginMode.Triangles);
                        parser.ParseDL((uint)torenderitems, LUTc, SpecialRendering.SkipJumps);
                        GL.End();
                    }
                }
                else
                {
                    parser.ParseDL(0, LUTc, SpecialRendering.SkipJumps);
                }
            }
            GL.End();
        }
    }
}
