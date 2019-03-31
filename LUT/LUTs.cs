using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LakiTool
{
    public class LUTs
    {
        public List<LightLUT> lightLUT = new List<LightLUT>();
        public List<VtxLUT> vtxLUT = new List<VtxLUT>();
        public List<TexLUT> texLUT = new List<TexLUT>();
    }

    public class LightLUT
    {
        public float[] col;
        public string rawLightData;
        public uint type;
        public LUTSub lightSubData = new LUTSub();
    }

    public class VtxLUT
    {
        public List<Vtx> vtces;
        public LUTSub vtxSubData = new LUTSub();
    }

    public class TexLUT
    {
        public Bitmap tex;
        public string texFileName;
        public string incbinFile;
        public LUTSub texSubData = new LUTSub();
    }
}
