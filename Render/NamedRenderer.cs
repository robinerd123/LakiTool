using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Render
{
    class NamedRenderer
    {
        public NamedRenderer(Renderer rendererObj, string nameObj)
        {
            renderer = rendererObj;
            name = nameObj;
        }

        public Renderer renderer = new Renderer();
        public string name;
    }
}
