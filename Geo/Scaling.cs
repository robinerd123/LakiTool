using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Geo
{
    class Scaling
    {
        public float scale = 1.0f;

        public void SetScale(UInt32 mScale)
        {
            scale = (float)mScale / (float)0x10000;
        }

        public OpenTK.Vector3 GetVector()
        {
            return new OpenTK.Vector3(scale, scale, scale);
        }
    }
}
