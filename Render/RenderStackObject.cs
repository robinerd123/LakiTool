using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace LakiTool.Render
{
    class RenderStackObject
    {
        public RenderStackObject(Renderer rObj, Obj.Translation tObj)
        {
            renderer = rObj;
            objTranslation = tObj;
        }

        public Renderer renderer = new Renderer();
        public Obj.Translation objTranslation;

        public void Render()
        {
            GL.PushMatrix();
            GL.Translate(objTranslation.GetVector()); //sorry for lazying this
            renderer.Render();
            GL.PopMatrix();
        }
    }
}
