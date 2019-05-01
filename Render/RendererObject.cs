using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Render
{
    class RendererObject
    {
        private RenderMode mode;

        public void Render()
        {
            switch (mode)
            {
                case RenderMode.F3D:
                    F3Drenderer.Render();
                    break;
                case RenderMode.Collision:
                    Colrenderer.Render();
                    break;
                case RenderMode.Geo:
                    Georenderer.Render();
                    break;
                case RenderMode.Level:
                    Lvlrenderer.Render();
                    break;
            }
        }

        public void Init(RenderMode renderMode)
        {
            mode = renderMode;

            switch (mode)
            {
                case RenderMode.F3D:
                    F3Drenderer.Init();
                    break;
                case RenderMode.Collision:
                    Colrenderer.Init();
                    break;
                case RenderMode.Geo:
                    Georenderer.Init();
                    break;
                case RenderMode.Level:
                    Lvlrenderer.Init();
                    break;
            }
        }

        public F3DRenderer F3Drenderer = new F3DRenderer();
        public ColRenderer Colrenderer = new ColRenderer();
        public GeoRenderer Georenderer = new GeoRenderer();
        public LvlRenderer Lvlrenderer = new LvlRenderer();
    }
}
