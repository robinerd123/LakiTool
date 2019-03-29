using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

//apology for the really messy code here itll probably be fixed some day

namespace LakiTool
{
    class F3DRender
    {
        private string[] dldata = new string[0];
        public LUTs luts = new LUTs();
        public F3DRender(string[] input)
        {
            dldata = input;
        }

        string[] vals = new string[0];

        public void initF3D(System.Windows.Forms.ListBox.ObjectCollection textureitems)
        {
            int n = -1;
            foreach (string line in dldata)
            {
                n++;
                if (!(line.Contains("gsSP") || line.Contains("gsDP"))) continue;
                vals = MISCUtils.ParseAsmbd(line);
                if (vals[0] == "gsSPVertex")
                {
                    int adressvert = 0;
                    bool loadingvtces = false;
                    byte vlength = (byte)vals[1].Length;
                    List<Vtx> verteces = new List<Vtx>();
                    Vtx twvtx = new Vtx();
                    int[] vert = new int[9];
                    string[] cs = new string[0];
                    foreach (string l in dldata)
                    {
                        adressvert++;
                        if (l.Length <= vlength) continue;
                        if (loadingvtces)
                        {
                            if (l[0] == 'v')
                            {
                                cs = MISCUtils.ParseAsmbd(dldata[adressvert-1]);
                                for (int v = 0; v < vert.Length; v++)
                                {
                                    string c = cs[v + 1];
                                    vert[v] = MISCUtils.ParseInt(c);
                                }
                                twvtx.X = (short)vert[0];
                                twvtx.Y = (short)vert[1];
                                twvtx.Z = (short)vert[2];
                                twvtx.S = (short)vert[3];
                                twvtx.T = (short)vert[4];
                                twvtx.R = (byte)vert[5];
                                twvtx.G = (byte)vert[6];
                                twvtx.B = (byte)vert[7];
                                twvtx.A = (byte)vert[8];
                                verteces.Add(twvtx);
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (l[vlength] == ':')
                        {
                            if (vals[1] == l.Substring(0, vlength))
                            {
                                loadingvtces = true;
                            }
                        }
                    }
                    luts.vtxLUT.Add(new VtxLUT(verteces, vals[1]));
                }
                if (vals[0] == "gsSPLight")
                {
                    int lline = 0;
                    foreach (string l in dldata)
                    {
                        lline++;
                        if (l.Contains(vals[1] + ":"))
                        {
                            break;
                        }
                    }
                    float[] light = new float[4];
                    for (int v = 0; v < light.Length; v++)
                    {
                        string c = MISCUtils.ParseAsmbd(dldata[lline])[v + 1];
                        light[v] = (float)MISCUtils.ParseInt(c) / 255f;
                    }
                    luts.lightLUT.Add(new LightLUT(light, vals[1]));
                }
                if (vals[0] == "gsDPSetTextureImage")
                {
                    foreach (string item in textureitems)
                    {
                        if (int.Parse(item.Split()[0]) == n)
                        {
                            luts.texLUT.Add(new TexLUT((Bitmap)Bitmap.FromFile(item.Split()[1]), vals[4]));
                            break;
                        }
                    }
                }
            }
        }

    }
}
