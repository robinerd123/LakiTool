using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Render
{
    class LvlRenderer
    {
        private bool inited = false;
        //private Lvl.LevelScript levelScript = new Lvl.LevelScript();

        public void Init()
        {
            inited = true;
            //levelScript.specialObjects.rendererObject.Colrenderer.lines = System.IO.File.ReadAllLines(loadobj.FileName);
            //levelScript.specialObjects.SetRenderMode(LakiTool.Render.RenderMode.Collision);
            //levelScript.specialObjects.rendererObject.Colrenderer.colMode = ColMode.OnlySpecial;
            //levelScript.specialObjects.initRenderer();
        }

        public void Render()
        {
            if (!inited) return;
            //still need to add shit here
        }
    }
}
