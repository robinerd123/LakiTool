using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Geo
{
    class Translation
    {
        public short xTrans, yTrans, zTrans;
        public OpenTK.Vector3 GetVector()
        {
            return new OpenTK.Vector3(xTrans >> 4, yTrans >> 4, zTrans >> 4);
        }
    }
}
