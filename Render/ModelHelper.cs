using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Render
{
    class ModelHelper
    {
        public List<NamedRenderer> rList = new List<LakiTool.Render.NamedRenderer>();

        public Renderer getRendererFromName(string name)
        {
            foreach(NamedRenderer renderer in rList)
            {
                if (renderer.name == name) return renderer.renderer;
            }
            return null;
        }

        public bool elemExists(string name)
        {
            foreach(NamedRenderer renderer in rList)
            {
                if (renderer.name == name) return true;
            }
            return false;
        }
    }
}
