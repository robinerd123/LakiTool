using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace LakiTool.OBJs.Special
{
    class SpecialObjectRenderStack
    {
        List<LakiTool.Render.RenderStackObject> renderStack = new List<LakiTool.Render.RenderStackObject>();
        List<CubeData> cubeData = new List<CubeData>();
        Render.ModelHelper modelHelper = new Render.ModelHelper();

        public void addSpecialObject(string specialObjName, short transX, short transY, short transZ)
        {
            Render.Renderer specialObjRenderer = new Render.Renderer();
            if (!modelHelper.elemExists(specialObjName))
            {
                specialObjRenderer.SetRenderMode(LakiTool.Render.RenderMode.Geo);
                specialObjRenderer.rendererObject.Georenderer.SetInitialDataFromLabelName(OBJs.Special.Utils.SpecialUtil.findSpecialFromPresetName(specialObjName).GeoName());
                specialObjRenderer.initRenderer();
                modelHelper.rList.Add(new Render.NamedRenderer(specialObjRenderer, specialObjName));
            }
            specialObjRenderer = modelHelper.getRendererFromName(specialObjName);
            Obj.Translation translation = new Obj.Translation();
            translation.xTrans = transX;
            translation.yTrans = transY;
            translation.zTrans = transZ;
            renderStack.Add(new Render.RenderStackObject(specialObjRenderer, translation));
        }

        public void setUpSpecialObjCube(string[] lines, short transX, short transY, short transZ)
        {
            CubeData cube = OBJs.Util.ObjUtil.getCubeDataFromF3DFile(lines); 
            if (cube.d)
            {
                cube.Translate((short)-transX, transY, (short)-transZ);
                cube.setCubeVtxes(2);
            }
            else
            {
                cube.updateCubeData(0, 0, 0);
                cube.Translate((short)-transX, transY, (short)-transZ);
                cube.setCubeVtxes(20);
            }
            cubeData.Add(cube);
        }
        
        public string[] getF3DLinesFromRendererStackObject(Render.RenderStackObject renderStackObject)
        {
            foreach(Geo.GeoObject geoObject in renderStackObject.renderer.rendererObject.Georenderer.geoObjects)
            {
                if (geoObject.objType == Geo.GeoObjectTypes.RenderObject) return geoObject.f3d.lines;
            }
            return null;
        }

        bool f = false;

        public void Render()
        {
            GL.Enable(EnableCap.Texture2D);
            for (int i = 0; i < renderStack.Count; i++)
            {
                renderStack[i].Render();
                if (!f) setUpSpecialObjCube(getF3DLinesFromRendererStackObject(renderStack[i]), renderStack[i].objTranslation.xTrans, renderStack[i].objTranslation.yTrans, renderStack[i].objTranslation.zTrans);
            }
            f = true;

            OBJs.Util.ObjUtil.setUpCubeDataRendering(0.8f, 0.2f, 0.2f);
            GL.Begin(PrimitiveType.Lines);
            for (int i = 0; i<cubeData.Count; i++)
            {
                cubeData[i].Render();
            }
            GL.End();
        }
    }
}
