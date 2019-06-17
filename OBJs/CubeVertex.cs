using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool
{
    struct CubeVertex
    {
        public CubeVertex(short x, short y, short z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public short X, Y, Z;
    }
}
