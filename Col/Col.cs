using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;

namespace LakiTool.Col
{
    class Col
    {
        List<ColVertex> verteces = new List<ColVertex>();
        byte colorValR, colorValG, colorValB;
        uint boxScale = 1000;
        public void colVertex(short x, short y, short z)
        {
            verteces.Add(new ColVertex(x, y, z));
        }

        public void colTri(short v0, short v1, short v2)
        {
            float r = (float)colorValR / 255f;
            float g = (float)colorValG / 255f;
            float b = (float)colorValB / 255f;
            float pseudoshade = ((float)((v0%255) / 2) + 127);
            GL.Color3((byte)(r * pseudoshade), (byte)(g * pseudoshade), (byte)(b * pseudoshade));
            GL.Vertex3(-verteces[v0].X >> 4, verteces[v0].Y >> 4, -verteces[v0].Z >> 4);
            GL.Vertex3(-verteces[v1].X >> 4, verteces[v1].Y >> 4, -verteces[v1].Z >> 4);
            GL.Vertex3(-verteces[v2].X >> 4, verteces[v2].Y >> 4, -verteces[v2].Z >> 4);
        }

        public void colTriSpecial(short v0, short v1, short v2, short flag)
        {
            colTri(v0, v1, v2);
        }

        public void colWaterBox(short id, short x1, short z1, short x2, short z2, short y)
        {
            GL.Color3((byte)127, (byte)127, (byte)255);
            GL.Vertex3(x1 >> 4, y >> 4, z1 >> 4);
            GL.Vertex3(x2 >> 4, y >> 4, z1 >> 4);
            GL.Vertex3(x1 >> 4, y >> 4, z2 >> 4);
            GL.Vertex3(x2 >> 4, y >> 4, z1 >> 4);
            GL.Vertex3(x1 >> 4, y >> 4, z2 >> 4);
            GL.Vertex3(x2 >> 4, y >> 4, z2 >> 4);
        }

        public void colTriInit(short color, short num)
        {
            colorValR = 127;
            colorValG = 127;
            colorValB = 127;
            colorValR += (((color % 8) & 0b100) != 0) ? (byte)127 : (byte)0;
            colorValG += (((color % 8) & 0b10) != 0) ? (byte)0 : (byte)127;
            colorValB += (((color % 8) & 0b1) != 0) ? (byte)127 : (byte)0;
        }

        public void special_object(short X, short Y, short Z)
        {
            GL.End();
            GL.Begin(PrimitiveType.Triangles);
        }
    }
}
