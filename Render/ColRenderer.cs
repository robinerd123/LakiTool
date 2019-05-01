using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace LakiTool.Render
{
    class ColRenderer
    {
        private bool inited = false;

        public string[] lines = new string[0];
        LakiTool.Col.ParseCol parse;
        OBJs.Special.SpecialObjectRenderStack specialObjects = new OBJs.Special.SpecialObjectRenderStack();

        public void Init()
        {
            inited = true;
            parse = new LakiTool.Col.ParseCol(lines);
            specialObjects = OBJs.Special.Utils.SpecialUtil.getSpecialObjectsFromCollisionFile(lines);
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.ColorMaterial);
        }

        public void Render()
        {
            if (!inited) return;
            GL.Disable(EnableCap.Texture2D);
            GL.Begin(BeginMode.Triangles);
            parse.ParseColData();
            specialObjects.Render();
            GL.End();
        }
    }
}
