using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace LakiTool.Geo
{
    class GeoObject
    {
        public Geo.Rotation objRot = new Geo.Rotation();
        public Geo.Translation objTrans = new Geo.Translation();
        public Geo.Scaling objScale = new Geo.Scaling();
        public Render.F3DRenderer f3d = new Render.F3DRenderer();

        bool f3Dinited = false;

        public void Render()
        {
            SetRotation(objRot);
            SetTranslation(objTrans);
            SetScaling(objScale);
            if (!f3Dinited) { f3d.Init(); f3Dinited = true; }
            f3d.Render();
        }
        
        private void SetRotation(Geo.Rotation rotObj)
        {
            Quaternion rotQuat = rotObj.GetQuaternion();

            GL.Rotate(rotQuat.X, 1, 0, 0);
            GL.Rotate(rotQuat.Y, 0, 1, 0);
            GL.Rotate(rotQuat.Z, 0, 0, 1);
        }

        private void SetTranslation(Geo.Translation transObj)
        {
            GL.Translate(transObj.GetVector());
        }

        private void SetScaling(Geo.Scaling scaleObj)
        {
            GL.Scale(scaleObj.GetVector());
        }
    }
}
