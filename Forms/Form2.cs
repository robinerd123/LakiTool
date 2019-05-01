using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//very incomplete lol

namespace LakiTool
{
    public partial class Form2 : Form
    {
        LUTs luts;
        string name;
        char type;
        
        public Form2(LUTs lutc, string namein, char typet)
        {
            luts = lutc;
            type = typet;
            name = namein;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            panel2.Location = new Point(panel2.Location.X, panel1.Location.Y);
            panel3.Location = new Point(panel3.Location.X, panel1.Location.Y);
            this.Size = new Size(this.Width, 159);
            switch (type)
            {
                case 'T':
                    textureEdit();
                    break;
                case 'V':
                    vertexEdit();
                    break;
                case 'L':
                    lightEdit();
                    break;
            }
        }

        private void textureEdit()
        {
            this.Text = "Texture" + this.Text;
            panel1.Visible = true;
            TexLUT texlut = LUTUtil.getTexLUTFromName(name, luts.texLUT);
            textBox1.Text = texlut.incbinFile;
            textBox2.Text = texlut.texFileName;
            textBox3.Text = texlut.texSubData.label.labelName;
            textBox4.Text = texlut.texSubData.label.labelLine.ToString();
            textBox5.Text = texlut.texSubData.label.isGlobal ? "True" : "False";
            pictureBox1.Image = texlut.tex;
        }

        private void vertexEdit()
        {
            this.Text = "Vertex" + this.Text;
            panel2.Visible = true;
            VtxLUT vtxlut = LUTUtil.getVtxLUTFromName(name, luts.vtxLUT);
        }

        private void lightEdit()
        {
            this.Text = "Light" + this.Text;
            panel3.Visible = true;
            LightLUT lightlut = LUTUtil.getLightLUTFromName(name, luts.lightLUT);
            textBox6.Text = (lightlut.type == 1) ? "Diffuse" : "Ambient";
            textBox7.Text = lightlut.lightSubData.label.labelLine.ToString();
            textBox8.Text = lightlut.lightSubData.label.labelName;
            textBox9.Text = Color.FromArgb((byte)(lightlut.col[0] * 255), (byte)(lightlut.col[1] * 255), (byte)(lightlut.col[2] * 255)).ToString();
            textBox10.Text = lightlut.rawLightData;
            panel4.BackColor = Color.FromArgb((byte)(lightlut.col[0] * 255), (byte)(lightlut.col[1] * 255), (byte)(lightlut.col[2] * 255));
        }
    }
}
