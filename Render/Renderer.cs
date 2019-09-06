using OpenTK.Graphics.OpenGL;
using System;

namespace LakiTool.Render
{
    enum RenderMode
    {
        F3D,
        Collision,
        Geo,
        Level
    }

    class Renderer : IDisposable
    {
        private RenderMode mode;
        public RendererObject rendererObject = new RendererObject();

        public void SetRenderMode(RenderMode renderMode)
        {
            mode = renderMode;

            switch (mode)
            {
                case RenderMode.F3D:
                    rendererObject.CreateF3DRenderer();
                    break;
                case RenderMode.Collision:
                    rendererObject.CreateColRenderer();
                    break;
                case RenderMode.Geo:
                    rendererObject.CreateGeoRenderer();
                    break;
                case RenderMode.Level:
                    rendererObject.CreateLvlRenderer();
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

        public void Dispose()
        {
            rendererObject.Dispose();
        }
    }
}
