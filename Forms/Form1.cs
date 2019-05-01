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

namespace LakiTool
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
        bool f3d = false, col = false;
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

            renderer.Render();

            RenderPanel.SwapBuffers();
        }

        LakiTool.Render.Renderer renderer = new LakiTool.Render.Renderer();

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

        private void openLevelFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadobj = new OpenFileDialog();
            loadobj.Filter = "dldata|*.s";
            loadobj.FilterIndex = 1;
            if (loadobj.ShowDialog() == DialogResult.OK)
            {
                radioButton2.Checked = true;
                radioButton1.Enabled = false;
                checkedListBox1.Items.Clear();
                listBox1.Items.Clear();
                renderer.rendererObject.F3Drenderer.lines = File.ReadAllLines(loadobj.FileName);
                int n = 0;
                renderer.rendererObject.F3Drenderer.fileName = loadobj.FileName;
                foreach (string line in renderer.rendererObject.F3Drenderer.lines)
                {
                    string[] vs = MISCUtils.ParseAsmbd(line);
                    int k = 2;
                    if (vs[0] == "gsSPDisplayList")
                    {
                        foreach (string l in renderer.rendererObject.F3Drenderer.lines)
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
                f3d = true;
                timer1.Enabled = true;
                renderer.rendererObject.F3Drenderer.manualRendering = true;
                renderer.SetRenderMode(LakiTool.Render.RenderMode.F3D);
                renderer.initRenderer();
                button2.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 edititem = new Form2(renderer.rendererObject.F3Drenderer.LUTc, listBox1.GetItemText(listBox1.SelectedItem), comboBox1.GetItemText(comboBox1.SelectedItem)[0]);
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

        private void changerender(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            renderer.rendererObject.F3Drenderer.torender = new List<int>();
            foreach (string itemschecked in checkedListBox1.CheckedItems)
            {
                renderer.rendererObject.F3Drenderer.torender.Add(int.Parse(MISCUtils.ParseAsmbd(itemschecked)[0]));
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            renderer.rendererObject.F3Drenderer.seq ^= true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cam.Position = Vector3.Zero;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            renderer.rendererObject.F3Drenderer.textured ^= true;
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
                    listBox1 = renderer.rendererObject.F3Drenderer.lutmanager.getListBoxFromTextures(listBox1);
                    break;
                case 1:
                    //verteces
                    listBox1 = renderer.rendererObject.F3Drenderer.lutmanager.getListBoxFromVerteces(listBox1);
                    break;
                case 2:
                    //lights
                    listBox1 = renderer.rendererObject.F3Drenderer.lutmanager.getListBoxFromLights(listBox1);
                    break;
            }
            button1.Enabled = false;
        }

        private void fixCollisionDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadobj = new OpenFileDialog();
            loadobj.Filter = "outdated collision data|*.s";
            loadobj.FilterIndex = 1;
            if (loadobj.ShowDialog() == DialogResult.OK)
            {
                string[] fixedCollision = LakiTool.Col.Util.ColUtil.colDataFix(File.ReadAllLines(loadobj.FileName));
                File.Delete(loadobj.FileName);
                File.WriteAllLines(loadobj.FileName, fixedCollision);
                MessageBox.Show("Collision file fixed succesfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void openGeoScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadobj = new OpenFileDialog();
            loadobj.Filter = "geo data|*.s";
            loadobj.FilterIndex = 1;
            if (loadobj.ShowDialog() == DialogResult.OK)
            {
                radioButton1.Checked = true;
                radioButton2.Enabled = false;
                renderer.rendererObject.Georenderer.lines = File.ReadAllLines(loadobj.FileName);
                renderer.SetRenderMode(LakiTool.Render.RenderMode.Geo);
                renderer.initRenderer();
                checkedListBox1.Items.Clear();
                listBox1.Items.Clear();
                timer1.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LakiTool.Mdl.OBJ.OBJ fullObj = new LakiTool.Mdl.OBJ.OBJ();
            if (!renderer.rendererObject.F3Drenderer.seq)
            {
                foreach (int torenderitems in renderer.rendererObject.F3Drenderer.torender)
                {
                    fullObj.objects.Add(LakiTool.Mdl.OBJ.Utils.OBJFileUtil.getObjFromDL(renderer.rendererObject.F3Drenderer.LUTc, renderer.rendererObject.F3Drenderer.lines, torenderitems, renderer.rendererObject.F3Drenderer.lines[torenderitems-2].Replace("glabel ", "").Replace(":", "").Split()[0]));
                }
            }
            else
            {
                fullObj.objects.Add(LakiTool.Mdl.OBJ.Utils.OBJFileUtil.getObjFromDL(renderer.rendererObject.F3Drenderer.LUTc, renderer.rendererObject.F3Drenderer.lines));
            }
            File.WriteAllLines("outfull.obj", fullObj.val());
            MessageBox.Show("Successfully exported as OBJ, check the folder you have LakiTool inside of.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void levelToolStripMenuItem_Click()
        {
            //System.Windows.Forms.MessageBox.Show("No game folder is selected!\nLoad your game folder via Set>Game folder path.", "No game folder selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Environment.GetEnvironmentVariable("sm64gamepath", EnvironmentVariableTarget.User) != null)
            {
                levelToolStripMenuItem.Enabled = true;
                LakiTool.MISC.Game.gamePath = Environment.GetEnvironmentVariable("sm64gamepath", EnvironmentVariableTarget.User);
            }

            //Clipboard.SetText(LakiTool.Geo.Util.GeoUtil.getLinesFromGeo(LakiTool.Geo.Util.GeoUtil.getGeoFromLines(File.ReadAllLines(@"C:\Users\RobiNDER\Downloads\sm64_source-master (6)\sm64_source-master\actors\goomba\geo.s"))));
        }

        private void gamePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.SelectedPath = (Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            fb.ShowDialog();
            LakiTool.MISC.Game.gamePath = fb.SelectedPath;
            Environment.SetEnvironmentVariable("sm64gamepath", fb.SelectedPath, EnvironmentVariableTarget.User);
            levelToolStripMenuItem.Enabled = true;
        }

        private void openCollisionDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadobj = new OpenFileDialog();
            loadobj.Filter = "collision data|*.s";
            loadobj.FilterIndex = 1;
            if (loadobj.ShowDialog() == DialogResult.OK)
            {
                radioButton1.Checked = true;
                radioButton2.Enabled = false;
                renderer.rendererObject.Colrenderer.lines = File.ReadAllLines(loadobj.FileName);
                renderer.SetRenderMode(LakiTool.Render.RenderMode.Collision);
                renderer.initRenderer();
                checkedListBox1.Items.Clear();
                listBox1.Items.Clear();
                timer1.Enabled = true;
            }
        }
    }
}
