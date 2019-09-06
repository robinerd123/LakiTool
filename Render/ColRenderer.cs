using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace LakiTool.Render
{
    enum ColMode
    {
        Full,
        OnlySpecial
    }

    class ColRenderer: IDisposable
    {
        private bool inited = false;

        public ColMode colMode = ColMode.Full;
        
        public string[] lines = new string[0];
        LakiTool.Col.ParseCol parse;
        OBJs.Special.SpecialObjectRenderStack specialObjects = new OBJs.Special.SpecialObjectRenderStack();

        bool displayListGenerated;
        int displayListObject;

        bool isDisposed;

        public void Init()
        {
            inited = true;
            parse = new LakiTool.Col.ParseCol(lines);
            specialObjects = OBJs.Special.Utils.SpecialUtil.getSpecialObjectsFromCollisionFile(lines);
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.ColorMaterial);

            displayListGenerated = false;
        }

        public void Dispose()
        {
            if (isDisposed) return;
            isDisposed = true;

            if (displayListGenerated)
            {
                GL.DeleteLists(displayListObject, 1);
                displayListGenerated = false;
            }
        }

        public void Render()
        {
            if (!inited) return;

            GL.Disable(EnableCap.Texture2D);

            if (colMode == ColMode.Full)
            {
                if (!displayListGenerated)
                {
                    displayListObject = GL.GenLists(1);
                    GL.NewList(displayListObject, ListMode.Compile);

                    GL.Begin(PrimitiveType.Triangles);
                    parse.ParseColData();
                    GL.End();

                    GL.EndList();

                    displayListGenerated = true;
                }

                GL.CallList(displayListObject);
            }

            specialObjects.Render();
        }
    }
}