using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LakiTool
{
    class LUTs
    {
        public List<LightLUT> lightLUT = new List<LightLUT>();
        public List<VtxLUT> vtxLUT = new List<VtxLUT>();
        public List<TexLUT> texLUT = new List<TexLUT>();
    }

    class LightLUT
    {
        public LightLUT(float[] color, string lightname)
        {
            col = color;
            name = lightname;
        }
        public float[] col;
        public string name;
    }

    class VtxLUT
    {
        public VtxLUT(List<Vtx> verteces, string vtxname)
        {
            vtces = verteces;
            name = vtxname;
        }
        public List<Vtx> vtces;
        public string name;
    }

    class TexLUT
    {
        public TexLUT(Bitmap teximage, string texname)
        {
            tex = teximage;
            name = texname;
        }
        public Bitmap tex;
        public string name;
    }
}
