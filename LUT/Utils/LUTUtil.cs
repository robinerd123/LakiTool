using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

//apology for the really messy code here itll probably be fixed some day

namespace LakiTool
{
    class LUTUtil
    {
        public string[] dldata = new string[0];
        public string fbpath = "";
        public string curFile = "";
        public LUTs luts = new LUTs();

        string[] vals = new string[0];

        public void initF3D()
        {
            int n = -1;
            foreach (string line in dldata)
            {
                n++;
                if (!(line.Contains("gsSP") || line.Contains("gsDP"))) continue;
                vals = MISCUtils.ParseAsmbd(line);
                if (vals[0] == "gsSPVertex")
                {
                    List<Vtx> verteces = new List<Vtx>();
                    Vtx twvtx = new Vtx();
                    VtxLUT vl = new VtxLUT();
                    int[] vert = new int[9];
                    string[] cs = new string[0];
                    vl.vtxSubData.label = LakiTool.LUT.Utils.LabelUtil.findLabelFromName(vals[1], curFile);
                    vl.vtxSubData.gData.GFile = curFile;
                    vl.vtxSubData.gData.GLine = (uint)n + 1;
                    uint l = vl.vtxSubData.label.labelLine;
                    while (true)
                    {
                        if (dldata[l].Length<6 || dldata[l][0] != 'v') break;
                        l++;
                        cs = MISCUtils.ParseAsmbd(dldata[l - 1]);
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
                    vl.vtces = verteces;
                    luts.vtxLUT.Add(vl);
                }
                if (vals[0] == "gsSPLight")
                {
                    LightLUT ll = new LightLUT();
                    ll.lightSubData.label = LakiTool.LUT.Utils.LabelUtil.findLabelFromName(vals[1], curFile);
                    ll.lightSubData.gData.GFile = curFile;
                    ll.lightSubData.gData.GLine = (uint)n + 1;
                    float[] light = new float[4];
                    for (int v = 0; v < light.Length; v++)
                    {
                        string c = MISCUtils.ParseAsmbd(dldata[ll.lightSubData.label.labelLine])[v + 1];
                        light[v] = (float)MISCUtils.ParseInt(c) / 255f;
                    }
                    ll.col = light;
                    ll.type = (uint)MISCUtils.ParseInt(vals[2]);
                    ll.rawLightData = dldata[ll.lightSubData.label.labelLine];
                    luts.lightLUT.Add(ll);
                }
                if (vals[0] == "gsDPSetTextureImage")
                {
                    TexLUT tl = new TexLUT();
                    tl.texSubData.label = LakiTool.LUT.Utils.LabelUtil.findLabelFromName(vals[4], curFile, fbpath, true);
                    tl.texSubData.gData.GFile = curFile;
                    tl.texSubData.gData.GLine = (uint)n + 1;
                    if (tl.texSubData.label.labelFound)
                    {
                        tl.incbinFile = MISCUtils.ParseAsmbd(File.ReadAllLines(tl.texSubData.label.labelFile)[tl.texSubData.label.labelLine])[1].Replace("\"", "");
                        string tp = fbpath + "/" + tl.incbinFile;
                        if (File.Exists(tp + ".png"))
                        {
                            tl.texFileName = tp + ".png";
                        }
                        else if (File.Exists(fbpath + "/" + "textures" + tl.incbinFile.Substring(3) + ".png"))
                        {
                            tl.texFileName = fbpath + "/" + "textures" + tl.incbinFile.Substring(3) + ".png";
                        }
                    }
                    else
                    {
                        tl.texFileName = "placeholder.png";
                    }
                    tl.tex = (Bitmap)Bitmap.FromFile(tl.texFileName);
                    luts.texLUT.Add(tl);
                }
            }

        }

        public System.Windows.Forms.ListBox getListBoxFromTextures(System.Windows.Forms.ListBox inputlb)
        {
            System.Windows.Forms.ListBox outputlb;
            outputlb = inputlb;
            outputlb.Items.Clear();
            foreach (TexLUT lutitem in luts.texLUT)
            {
                outputlb.Items.Add(lutitem.texSubData.label.labelName);
            }
            return outputlb;
        }

        public System.Windows.Forms.ListBox getListBoxFromVerteces(System.Windows.Forms.ListBox inputlb)
        {
            System.Windows.Forms.ListBox outputlb;
            outputlb = inputlb;
            outputlb.Items.Clear();
            foreach (VtxLUT lutitem in luts.vtxLUT)
            {
                outputlb.Items.Add(lutitem.vtxSubData.label.labelName);
            }
            return outputlb;
        }

        public System.Windows.Forms.ListBox getListBoxFromLights(System.Windows.Forms.ListBox inputlb)
        {
            System.Windows.Forms.ListBox outputlb;
            outputlb = inputlb;
            outputlb.Items.Clear();
            foreach (LightLUT lutitem in luts.lightLUT)
            {
                outputlb.Items.Add(lutitem.lightSubData.label.labelName);
            }
            return outputlb;
        }

        public static TexLUT getTexLUTFromName(string tname, List<TexLUT> lut)
        {
            foreach (TexLUT lutitem in lut)
            {
                if (tname == lutitem.texSubData.label.labelName)
                {
                    return lutitem;
                }
            }
            return new TexLUT();
        }

        public static VtxLUT getVtxLUTFromName(string vname, List<VtxLUT> lut)
        {
            foreach (VtxLUT lutitem in lut)
            {
                if (vname == lutitem.vtxSubData.label.labelName)
                {
                    return lutitem;
                }
            }
            return new VtxLUT();
        }

        public static LightLUT getLightLUTFromName(string lname, List<LightLUT> lut)
        {
            foreach (LightLUT lutitem in lut)
            {
                if (lname == lutitem.lightSubData.label.labelName)
                {
                    return lutitem;
                }
            }
            return new LightLUT();
        }
    }
}
