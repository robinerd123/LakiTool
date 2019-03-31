using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;
using LakiTool;

namespace LakiTool
{
    class GBI
    {
        public Vtx[] vtxbuffer = new Vtx[81];
        uint wraps = 0, wrapt = 0;
        int x = 32;
        int y = 32;
        uint geommode = GBIConsts.G_LIGHTING | GBIConsts.G_SHADE;

        public void gsSP1Triangle(int v0, int v1, int v2, int flag)
        {
            F3DUtils.setUpVertColor(vtxbuffer[v0].R, vtxbuffer[v0].G, vtxbuffer[v0].B, vtxbuffer[v0].A, geommode);
            GL.TexCoord2((float)vtxbuffer[v0].S / 32 / x, (float)vtxbuffer[v0].T / 32 / y);
            GL.Vertex3(-vtxbuffer[v0].X >> 4, vtxbuffer[v0].Y >> 4, -vtxbuffer[v0].Z >> 4);
            F3DUtils.setUpVertColor(vtxbuffer[v1].R, vtxbuffer[v1].G, vtxbuffer[v1].B, vtxbuffer[v1].A, geommode);
            GL.TexCoord2((float)vtxbuffer[v1].S / 32 / x, (float)vtxbuffer[v1].T / 32 / y);
            GL.Vertex3(-vtxbuffer[v1].X >> 4, vtxbuffer[v1].Y >> 4, -vtxbuffer[v1].Z >> 4);
            F3DUtils.setUpVertColor(vtxbuffer[v2].R, vtxbuffer[v2].G, vtxbuffer[v2].B, vtxbuffer[v2].A, geommode);
            GL.TexCoord2((float)vtxbuffer[v2].S / 32 / x, (float)vtxbuffer[v2].T / 32 / y);
            GL.Vertex3(-vtxbuffer[v2].X >> 4, vtxbuffer[v2].Y >> 4, -vtxbuffer[v2].Z >> 4);
        }

        public void gsSP2Triangles(int v00, int v01, int v02, int flag0, int v10, int v11, int v12, int flag1)
        {
            gsSP1Triangle(v00, v01, v02, flag0);
            gsSP1Triangle(v10, v11, v12, flag1);
        }

        public void gsSPClearGeometryMode(uint mode)
        {
            geommode &= 0xFFFFFFFF ^ (mode);
        }

        public void gsSPSetGeometryMode(uint mode)
        {
            geommode |= mode;
        }

        public void gsDPSetTextureImage(Bitmap bitmap)
        {
            GL.End();
            int id;
            BitmapData data = new BitmapData();
            id = GL.GenTexture();
            data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.BindTexture(TextureTarget.Texture2D, id);
            x = data.Width;
            y = data.Height;
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
            GL.BindTexture(TextureTarget.Texture2D, id);
            F3DUtils.setUpWrapST(wraps, wrapt);
            GL.Begin(BeginMode.Triangles);
        }

        public void gsDPSetEnvColor(byte r, byte g, byte b, byte a)
        {
            //TODO: make this better
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { (float)r / 255f, (float)g / 255f, (float)g / 255f, (float)a / 255f });
            GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.Diffuse);
        }

        public void gsSPLight(float[] lightcolor, byte type)
        {
            GL.End();
            GL.Light(LightName.Light0, (type == 1) ? LightParameter.Diffuse : LightParameter.Ambient, lightcolor);
            GL.ColorMaterial(MaterialFace.Front, (type == 1) ? ColorMaterialParameter.Diffuse : ColorMaterialParameter.Ambient);
            GL.Begin(BeginMode.Triangles);
        }

        public void gsSPVertex(Vtx[] verteces, byte length, byte start)
        {
            Array.Copy(verteces, 0, vtxbuffer, start, length);
        }

        public void gsDPSetTile(uint[] wrapst)
        {
            wraps = wrapst[0];
            wrapt = wrapst[1];
        }
    }
}
