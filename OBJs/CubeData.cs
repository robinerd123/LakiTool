using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;

namespace LakiTool.OBJs
{
    class CubeData
    {
        public bool d = false;
        private short x, X, y, Y, z, Z;
        private CubeVertex[] cubeVtxes = new CubeVertex[8];

        public void Translate(short transX, short transY, short transZ)
        {
            x += (short)(transX >> 4);
            X += (short)(transX >> 4);
            y += (short)(transY >> 4);
            Y += (short)(transY >> 4);
            z += (short)(transZ >> 4);
            Z += (short)(transZ >> 4);
        }

        private void initCubeData(short vx, short vy, short vz)
        {
            x = X = vx;
            y = Y = vy;
            z = Z = vy;
            d = true;
        }

        public void updateCubeData(short vx, short vy, short vz)
        {
            if (!d) initCubeData(vx, vy, vz);
            if (vx < x) x = vx;
            if (vx > X) X = vx;
            if (vy < y) y = vy;
            if (vy > Y) Y = vy;
            if (vz < z) z = vz;
            if (vz > Z) Z = vz;
        }

        public void setCubeVtxes(short sz)
        {
            x -= sz;
            X += sz;
            y -= sz;
            Y += sz;
            z -= sz;
            Z += sz;

            cubeVtxes[0] = new CubeVertex(x, y, z);
            cubeVtxes[1] = new CubeVertex(x, Y, z);
            cubeVtxes[2] = new CubeVertex(X, y, z);
            cubeVtxes[3] = new CubeVertex(X, Y, z);
            cubeVtxes[4] = new CubeVertex(x, y, Z);
            cubeVtxes[5] = new CubeVertex(x, Y, Z);
            cubeVtxes[6] = new CubeVertex(X, y, Z);
            cubeVtxes[7] = new CubeVertex(X, Y, Z);

        }

        public void Render()
        {
            //bottom square
            GL.Vertex3(cubeVtxes[00].X, cubeVtxes[00].Y, cubeVtxes[00].Z);
            GL.Vertex3(cubeVtxes[01].X, cubeVtxes[01].Y, cubeVtxes[01].Z);
            GL.Vertex3(cubeVtxes[01].X, cubeVtxes[01].Y, cubeVtxes[01].Z);
            GL.Vertex3(cubeVtxes[03].X, cubeVtxes[03].Y, cubeVtxes[03].Z);
            GL.Vertex3(cubeVtxes[03].X, cubeVtxes[03].Y, cubeVtxes[03].Z);
            GL.Vertex3(cubeVtxes[02].X, cubeVtxes[02].Y, cubeVtxes[02].Z);
            GL.Vertex3(cubeVtxes[02].X, cubeVtxes[02].Y, cubeVtxes[02].Z);
            GL.Vertex3(cubeVtxes[00].X, cubeVtxes[00].Y, cubeVtxes[00].Z);

            //vertical side edges
            GL.Vertex3(cubeVtxes[00].X, cubeVtxes[00].Y, cubeVtxes[00].Z);
            GL.Vertex3(cubeVtxes[04].X, cubeVtxes[04].Y, cubeVtxes[04].Z);
            GL.Vertex3(cubeVtxes[01].X, cubeVtxes[01].Y, cubeVtxes[01].Z);
            GL.Vertex3(cubeVtxes[05].X, cubeVtxes[05].Y, cubeVtxes[05].Z);
            GL.Vertex3(cubeVtxes[02].X, cubeVtxes[02].Y, cubeVtxes[02].Z);
            GL.Vertex3(cubeVtxes[06].X, cubeVtxes[06].Y, cubeVtxes[06].Z);
            GL.Vertex3(cubeVtxes[03].X, cubeVtxes[03].Y, cubeVtxes[03].Z);
            GL.Vertex3(cubeVtxes[07].X, cubeVtxes[07].Y, cubeVtxes[07].Z);

            //top square
            GL.Vertex3(cubeVtxes[04].X, cubeVtxes[04].Y, cubeVtxes[04].Z);
            GL.Vertex3(cubeVtxes[05].X, cubeVtxes[05].Y, cubeVtxes[05].Z);
            GL.Vertex3(cubeVtxes[05].X, cubeVtxes[05].Y, cubeVtxes[05].Z);
            GL.Vertex3(cubeVtxes[07].X, cubeVtxes[07].Y, cubeVtxes[07].Z);
            GL.Vertex3(cubeVtxes[07].X, cubeVtxes[07].Y, cubeVtxes[07].Z);
            GL.Vertex3(cubeVtxes[06].X, cubeVtxes[06].Y, cubeVtxes[06].Z);
            GL.Vertex3(cubeVtxes[06].X, cubeVtxes[06].Y, cubeVtxes[06].Z);
            GL.Vertex3(cubeVtxes[04].X, cubeVtxes[04].Y, cubeVtxes[04].Z);
        }
    }
}
