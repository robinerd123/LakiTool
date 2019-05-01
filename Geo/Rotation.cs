using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Geo
{
    class Rotation
    {
        public short xRot, yRot, zRot;
        public OpenTK.Quaternion GetQuaternion()
        {
            return new OpenTK.Quaternion(xRot, yRot, zRot, 1.0f);
        }
    }
}
