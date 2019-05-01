using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Mdl.OBJ.Utils
{
    class OBJFileUtil
    {
        public static OBJFile.o getObjFromDL(LUTs inputData, string[] lines, int offset = 0, string objName = "default")
        {
            string[] vals;
            uint vertSet = (uint)(vAdr==0?1:0);
            OBJFile.o outObjFile = new OBJFile.o();
            outObjFile.oname = objName;
            uint lastVertAdr = OBJFileUtil.vAdr;
            int n = -1;
            int x = 0, y = 0;
            foreach (string line in lines)
            {
                n++;
                if (n < offset - 1) continue;
                if (!(line.Contains("gsSP") || line.Contains("gsDP"))) continue;
                vals = MISCUtils.ParseAsmbd(line);
                if (vals[0] == "gsSPEndDisplayList" && offset > 0)
                {
                    break;
                }
                if (vals[0] == "gsSP1Triangle")
                {
                    uint v0 = (uint)MISCUtils.ParseInt(vals[1]);
                    uint v1 = (uint)MISCUtils.ParseInt(vals[2]);
                    uint v2 = (uint)MISCUtils.ParseInt(vals[3]);
                    v0 += lastVertAdr;
                    v1 += lastVertAdr;
                    v2 += lastVertAdr;
                    outObjFile.faces.Add(new OBJFile.f(new OBJFile.fFmt(v0, v0, v0), new OBJFile.fFmt(v1, v1, v1), new OBJFile.fFmt(v2, v2, v2)));
                }
                if (vals[0] == "gsSP2Triangles")
                {
                    uint v0 = (uint)MISCUtils.ParseInt(vals[1]);
                    uint v1 = (uint)MISCUtils.ParseInt(vals[2]);
                    uint v2 = (uint)MISCUtils.ParseInt(vals[3]);
                    v0 += lastVertAdr;
                    v1 += lastVertAdr;
                    v2 += lastVertAdr;
                    outObjFile.faces.Add(new OBJFile.f(new OBJFile.fFmt(v0, v0, v0), new OBJFile.fFmt(v1, v1, v1), new OBJFile.fFmt(v2, v2, v2)));
                    v0 = (uint)MISCUtils.ParseInt(vals[5]);
                    v1 = (uint)MISCUtils.ParseInt(vals[6]);
                    v2 = (uint)MISCUtils.ParseInt(vals[7]);
                    v0 += lastVertAdr;
                    v1 += lastVertAdr;
                    v2 += lastVertAdr;
                    outObjFile.faces.Add(new OBJFile.f(new OBJFile.fFmt(v0, v0, v0), new OBJFile.fFmt(v1, v1, v1), new OBJFile.fFmt(v2, v2, v2)));
                }
                if (vals[0] == "gsSPVertex")
                {
                    lastVertAdr += vertSet;
                    Vtx[] verteces = F3DUtils.getVtxFromName(vals[1], inputData.vtxLUT).ToArray();
                    for(int v = MISCUtils.ParseInt(vals[3]); v < MISCUtils.ParseInt(vals[2]) + MISCUtils.ParseInt(vals[3]); v++) {
                        outObjFile.verts.Add(new OBJFile.v(verteces[v].X, verteces[v].Y, verteces[v].Z));
                        outObjFile.coords.Add(new OBJFile.vt(verteces[v].S, verteces[v].T, x, y));
                        outObjFile.normals.Add(new OBJFile.vn(verteces[v].R, verteces[v].G, verteces[v].B));
                    }
                    vertSet = (uint)MISCUtils.ParseInt(vals[2]);
                }
                if (vals[0] == "gsDPSetTextureImage")
                {

                    x = F3DUtils.getTexFromName(vals[4], inputData.texLUT).Size.Width;
                    y = F3DUtils.getTexFromName(vals[4], inputData.texLUT).Size.Height;
                }
            }
            OBJFileUtil.vAdr = lastVertAdr + vertSet;
            return outObjFile;
        }

        public static uint vAdr = 0;
    }
}
