using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.OBJs.Special
{
    class SpecialObjectRenderStack
    {
        List<LakiTool.Render.RenderStackObject> renderStack = new List<LakiTool.Render.RenderStackObject>();
        Render.ModelHelper modelHelper = new Render.ModelHelper();

        public void addSpecialObject(string specialObjName, short transX, short transY, short transZ)
        {
            Render.Renderer specialObjRenderer = new Render.Renderer();
            if (!modelHelper.elemExists(specialObjName))
            {
                specialObjRenderer.rendererObject.Georenderer.SetInitialDataFromLabelName(OBJs.Special.Utils.SpecialUtil.findSpecialFromPresetName(specialObjName).GeoName());
                specialObjRenderer.SetRenderMode(LakiTool.Render.RenderMode.Geo);
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

        public void Render()
        {
            foreach (Render.RenderStackObject renderObj in renderStack)
            {
                renderObj.Render();
            }
        }
    }
}
