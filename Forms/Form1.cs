using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using LakiTool;

//apology for the really messy code here itll probably be fixed some day

namespace SM64LevelUp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Camera cam = new Camera();


        private void glcontrol1_Load(object sender, EventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
        }

        bool mic = false;

        

        private void doRenderStuff(object sender, EventArgs e)
        {
            //while (true)
            {
                if (this.ContainsFocus)
                {
                    checkkeys();
                    if (Mouse.GetState().LeftButton == OpenTK.Input.ButtonState.Pressed && mic)
                    {
                        checkmouse();
                    }
                }

                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                Render(new Rectangle(new Point(0, 0), new Size(432, 324)), 432, 324, glcontrol1);
                stopwatch.Stop();
                label1.Text = (1000f / (float)stopwatch.ElapsedMilliseconds).ToString() + " FPS";
            }
        }
        bool vertcolor = true;
        public void Render(Rectangle ClientRectangle, int Width, int Height, GLControl RenderPanel)
        {
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            Matrix4 projection = cam.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView(1.0f, Width / (float)Height, 1f, 1000.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            InitialiseView();
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Normalize);
            GL.ShadeModel(ShadingModel.Smooth);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            Vector3 lookat = new Vector3();
            lookat.X = (float)(Math.Cos((float)cam.Orientation.X) * Math.Cos((float)cam.Orientation.Y));
            lookat.Y = (float)Math.Sin((float)cam.Orientation.Y);
            lookat.Z = (float)(Math.Sin((float)cam.Orientation.X) * Math.Cos((float)cam.Orientation.Y));
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { Math.Abs(lookat.X), Math.Abs(lookat.Y), Math.Abs(lookat.Z), 0.0f });
            ParseF3D parse = new ParseF3D(lines);
            if (!textured) GL.Disable(EnableCap.Texture2D); else GL.Enable(EnableCap.Texture2D);
            GL.Begin(BeginMode.Triangles);
            if (!seq)
            {
                foreach (int torenderitems in torender)
                {
                    GL.Begin(BeginMode.Triangles);
                    parse.ParseDL(torenderitems, GBIc, LUTc);
                    GL.End();
                }
            }
            else
            {
                parse.ParseDL(0, GBIc, LUTc);
            }
            GL.End();
            RenderPanel.SwapBuffers();
        }
        GBI GBIc = new GBI();
        LUTs LUTc = new LUTs();
        LUTUtil lutmanager = new LUTUtil();
        public static void InitialiseView()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            GL.ClearColor(Color.LightBlue);
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.Texture2D);
            GL.CullFace(CullFaceMode.Back);
        }

        private void checkkeys()
        {
            KeyboardState state = Keyboard.GetState();
            cam.MoveSpeed = (float)(trackBar1.Value+1);
            if (state[Key.Plus]) cam.MoveSpeed+= 1f;
            if (state[Key.Minus]) cam.MoveSpeed -= 1f;
            if (cam.MoveSpeed < 1f)
            {
                cam.MoveSpeed = 1f;
            }else if(cam.MoveSpeed > 10f){
                cam.MoveSpeed = 10f;
            }
            trackBar1.Value = (int)(cam.MoveSpeed)-1;
            if (state[Key.W]) cam.Move(0f, 0.1f, 0f);
            if (state[Key.S]) cam.Move(0f, -0.1f, 0f);
            if (state[Key.A]) cam.Move(-0.1f, 0f, 0f);
            if (state[Key.D]) cam.Move(0.1f, 0f, 0f);
            if (state[Key.E]) cam.Move(0f, 0f, 0.1f);
            if (state[Key.Q]) cam.Move(0f, 0f, -0.1f);
        }
        
        private void checkmouse()
        {
            MouseState mstate = Mouse.GetState();
            Point p = glcontrol1.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
            float x = -(p.X - (glcontrol1.Width / 2));
            float y = -(p.Y - (glcontrol1.Height / 2));
            cam.AddRotation(x, y);
            Render(new Rectangle(new Point(0, 0), new Size(432, 324)), 320, 240, glcontrol1);
        }

        string[] lines = new string[0];

        private void openLevelFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadobj = new OpenFileDialog();
            loadobj.Filter = "dldata|*.s";
            loadobj.FilterIndex = 1;
            if (loadobj.ShowDialog() == DialogResult.OK)
            {
                checkedListBox1.Items.Clear();
                listBox1.Items.Clear();
                FolderBrowserDialog fb = new FolderBrowserDialog();
                fb.SelectedPath = (Path.GetDirectoryName(loadobj.FileName));
                fb.ShowDialog();
                lines = File.ReadAllLines(loadobj.FileName);
                int n = 0;
                string fname = "";
                foreach (string line in lines)
                {
                    string[] vs = MISCUtils.ParseAsmbd(line);
                    int k = 2;
                    if (vs[0] == "gsSPDisplayList")
                    {
                        foreach (string l in lines)
                        {
                            if (l.Contains(vs[1]))
                            {
                                if (l.Contains("glabel") || l.Contains(":"))
                                {
                                    checkedListBox1.Items.Add(k.ToString() + " " + vs[1]);
                                    break;
                                }
                            }
                            k++;
                        }
                    }
                    n++;

                }
                timer1.Enabled = true;
                lutmanager.dldata = lines;
                lutmanager.curFile = loadobj.FileName;
                lutmanager.fbpath = fb.SelectedPath;
                lutmanager.initF3D();
                LUTc = lutmanager.luts;

                button2.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 edititem = new Form2(LUTc, listBox1.GetItemText(listBox1.SelectedItem), comboBox1.GetItemText(comboBox1.SelectedItem)[0]);
            edititem.ShowDialog();
            /*
            string fp = "placeholder.png";
            OpenFileDialog loadobj = new OpenFileDialog();
            loadobj.Filter = "img|*.png";
            loadobj.FilterIndex = 1;
            if (loadobj.ShowDialog() == DialogResult.OK)
            {
                fp = loadobj.FileName;
            }
            listBox1.Items[listBox1.SelectedIndex] = (listBox1.Items[listBox1.SelectedIndex] as string).Split()[0] + " " + fp;*/
        }

        private void timer(object sender, EventArgs e)
        {
            timer1.Enabled ^= true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            vertcolor ^= true;
        }

        List<int> torender = new List<int>();
        private void changerender(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            torender = new List<int>();
            foreach (string itemschecked in checkedListBox1.CheckedItems)
            {
                torender.Add(int.Parse(MISCUtils.ParseAsmbd(itemschecked)[0]));
            }
        }
        
        private void regmousedown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mic = true;
        }

        private void remouseup(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mic = false;
        }
        bool unc = false;
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, !unc?true:false);
            }
            unc ^= true;
            button2.Text = !unc ? "C" +button2.Text.Substring(3):"Unc" + button2.Text.Substring(1);
            changerender(new object(), new System.Windows.Forms.MouseEventArgs(MouseButtons.Left,0,0,0,0));
        }
        bool seq = false;
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            seq ^= true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cam.Position = Vector3.Zero;
        }
        bool textured = true;
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textured ^= true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            if (listBox1.SelectedIndex < 0) button1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    //textures
                    listBox1 = lutmanager.getListBoxFromTextures(listBox1);
                    break;
                case 1:
                    //verteces
                    listBox1 = lutmanager.getListBoxFromVerteces(listBox1);
                    break;
                case 2:
                    //lights
                    listBox1 = lutmanager.getListBoxFromLights(listBox1);
                    break;
            }
            button1.Enabled = false;
        }
    }
}
