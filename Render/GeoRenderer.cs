using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace LakiTool.Render
{
    class GeoRenderer
    {
        private bool inited = false;

        public string geoLabelName = null;
        public string[] lines = new string[0];
        public List<Geo.GeoObject> geoObjects = new List<Geo.GeoObject>();
        public Geo.GeoUtil geoManager = new Geo.GeoUtil();

        public void SetInitialDataFromLabelName(string labelname)
        {
            Labels.Label geolabel = new Labels.Label();
            geolabel = Labels.Utils.LabelUtil.findLabelFromName(labelname, "", LakiTool.MISC.Game.gamePath, true);
            if (geolabel.labelFound)
            {
                lines = System.IO.File.ReadAllLines(geolabel.labelFile);
                geoLabelName = geolabel.labelName;
            }
        }

        public void Init()
        {
            inited = true;
            geoObjects = geoManager.GetGeoObjectsFromGeoElems(Geo.Util.GeoUtil.getGeoFromLines(lines), geoLabelName);
        }

        public void Render()
        {
            if (!inited) return;
            foreach (Geo.GeoObject geoObject in geoObjects)
            {
                switch (geoObject.objType)
                {
                    case Geo.GeoObjectTypes.PushMatrix:
                        //GL.PushMatrix();
                        break;
                    case Geo.GeoObjectTypes.RenderObject:
                        geoObject.Render();
                        break;
                    case Geo.GeoObjectTypes.PopMatrix:
                        //GL.PopMatrix();
                        break;
                }
            }
        }
    }
}
