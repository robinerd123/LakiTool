using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

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

            switch (mode)
            {
                case RenderMode.F3D:
                    rendererObject.F3Drenderer = new F3DRenderer();
                    break;
                case RenderMode.Collision:
                    rendererObject.Colrenderer = new ColRenderer();
                    break;
                case RenderMode.Geo:
                    rendererObject.Georenderer = new GeoRenderer();
                    break;
                case RenderMode.Level:
                    rendererObject.Lvlrenderer = new LvlRenderer();
                    break;
            }
        }

        public void initRenderer()
        {
            rendererObject.Init(mode);
        }

        public void Render()
        {
            GL.Color3(1.0f, 1.0f, 1.0f); //reset color to white
            F3DUtils.setUpWrapST(0, 0); //reset wrap data
            rendererObject.Render();
        }
    }
}
