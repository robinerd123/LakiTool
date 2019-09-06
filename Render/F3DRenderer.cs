using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace LakiTool.Render
{
    class F3DRenderer: IDisposable
    {
        private bool inited = false;
        
        public bool seq = false;
        
        public string fileName = null;
        public string[] lines = new string[0];
        public int startCommand;
        public ParseF3D parser;
        public LUTs LUTc = new LUTs();
        public LUTUtil lutmanager = new LUTUtil();
        public List<int> torender = new List<int>();
        public List<GBI.Utils.LSUtil> labelsearchers = new List<GBI.Utils.LSUtil>();

        bool displayListGenerated;
        int displayListObject;

        bool isDisposed;

        public void SetInitialDataFromLabelName(string labelname)
        {
            Labels.Label f3dlabel = new Labels.Label();
            f3dlabel = Labels.Utils.LabelUtil.findLabelFromName(labelname, "", LakiTool.MISC.Game.gamePath, true);
            if (f3dlabel.labelFound) {
                fileName = f3dlabel.labelFile;
                lines = System.IO.File.ReadAllLines(f3dlabel.labelFile);
                startCommand = F3DUtils.getIndexFromLineData(f3dlabel.labelLine, lines);
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
            parser = new ParseF3D(F3DUtils.getF3DCommandsFromLines(lines), lutmanager.jumpContainer);
            labelsearchers = F3DUtils.getLabelSearchersFromLabelContainer(new Labels.LabelContainer(Labels.Utils.LabelUtil.getLabelListFromModelFile(fileName)), lines);

            GL.Enable(EnableCap.Texture2D);

            displayListGenerated = false;
        }

        public void Dispose()
        {
            if (isDisposed) return;
            isDisposed = true;

            if (displayListGenerated)
            {
                GL.DeleteLists(displayListObject, 1);
                displayListGenerated = false;
            }
        }

        public void Render()
        {
            if (!inited) return;

            if (!displayListGenerated)
            {
                displayListObject = GL.GenLists(1);
                GL.NewList(displayListObject, ListMode.Compile);

                GL.Begin(PrimitiveType.Triangles);

                if (!seq)
                {
                    parser.ParseDL(startCommand, LUTc, SpecialRendering.Off, labelsearchers);
                }
                else
                {
                    parser.ParseDL(0, LUTc, SpecialRendering.SEQ);
                }

                GL.End();

                GL.EndList();

                displayListGenerated = true;
            }

            GL.CallList(displayListObject);
        }
    }
}
