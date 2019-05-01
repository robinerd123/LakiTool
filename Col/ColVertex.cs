using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Col
{
    struct ColVertex
    {
        public ColVertex(short x, short y, short z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public short X, Y, Z;
    }
}
