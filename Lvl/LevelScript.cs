using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Lvl
{
    class LevelScript
    {
        public Render.Renderer specialObjects = new Render.Renderer();

        public void Render()
        {
            specialObjects.Render();
        }
    }
}
