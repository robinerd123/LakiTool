using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Render
{
    enum RenderMode
    {
        F3D,
        Collision,
        Geo,
        Level
    }

    class Renderer
    {
        private RenderMode mode;
        public RendererObject rendererObject = new RendererObject();

        public void SetRenderMode(RenderMode renderMode)
        {
            mode = renderMode;
        }

        public void initRenderer()
        {
            rendererObject.Init(mode);
        }

        public void Render()
        {
            F3DUtils.setUpWrapST(0, 0); //reset wrap data
            rendererObject.Render();
        }
    }
}
