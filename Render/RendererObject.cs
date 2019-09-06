using System;

namespace LakiTool.Render
{
    class RendererObject : IDisposable
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

        public void Dispose()
        {
            if (F3Drenderer != null) F3Drenderer.Dispose();
            if (Colrenderer != null) Colrenderer.Dispose();
            if (Georenderer != null) Georenderer.Dispose();
            if (Lvlrenderer != null) Lvlrenderer.Dispose();
            CleanRenderers();
        }

        public F3DRenderer F3Drenderer { get; private set; }
        public ColRenderer Colrenderer { get; private set; }
        public GeoRenderer Georenderer { get; private set; }
        public LvlRenderer Lvlrenderer { get; private set; }

        public void CreateF3DRenderer()
        {
            if (F3Drenderer != null) F3Drenderer.Dispose();
            CleanRenderers();
            F3Drenderer = new F3DRenderer();
        }

        public void CreateColRenderer()
        {
            if (Colrenderer != null) Colrenderer.Dispose();
            CleanRenderers();
            Colrenderer = new ColRenderer();
        }

        public void CreateGeoRenderer()
        {
            if (Georenderer != null) Georenderer.Dispose();
            CleanRenderers();
            Georenderer = new GeoRenderer();
        }

        public void CreateLvlRenderer()
        {
            if (Lvlrenderer != null) Lvlrenderer.Dispose();
            CleanRenderers();
            Lvlrenderer = new LvlRenderer();
        }

        void CleanRenderers()
        {
            Lvlrenderer = null;
            Colrenderer = null;
            Georenderer = null;
            Lvlrenderer = null;
        }
    }
}