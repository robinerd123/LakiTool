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
        string objs;
        int index;
        char type;
        public Form2(string objst, int indext, char typet)
        {
            objs = objst;
            index = indext;
            type = typet;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            switch (type)
            {
                case 'T':
                    this.Name = "Texture" + this.Name;
                    string texname = "/" + objs.Split(new[] { '/' }, 2)[1];
                    break;
            }
        }

    }
}
