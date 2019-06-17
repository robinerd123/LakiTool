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
            GL.Enable(EnableCap.Texture2D);
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
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0.05f);
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
                renderer.SetRenderMode(LakiTool.Render.RenderMode.F3D);
                renderer.rendererObject.F3Drenderer.lines = File.ReadAllLines(loadobj.FileName);
                renderer.rendererObject.F3Drenderer.fileName = loadobj.FileName;
                timer1.Enabled = true;
                renderer.rendererObject.F3Drenderer.seq = true;
                renderer.initRenderer();
            }
        }

        private void timer(object sender, EventArgs e)
        {
            timer1.Enabled ^= true;
        }
        
        private void regmousedown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mic = true;
        }

        private void remouseup(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mic = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cam.Position = Vector3.Zero;
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
                renderer.SetRenderMode(LakiTool.Render.RenderMode.Geo);
                renderer.rendererObject.Georenderer.lines = File.ReadAllLines(loadobj.FileName);
                renderer.initRenderer();
                timer1.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LakiTool.Mdl.OBJ.OBJ fullObj = new LakiTool.Mdl.OBJ.OBJ();
            fullObj.objects.Add(LakiTool.Mdl.OBJ.Utils.OBJFileUtil.getObjFromDL(renderer.rendererObject.F3Drenderer.LUTc, renderer.rendererObject.F3Drenderer.lines));
            File.WriteAllLines("outfull.obj", fullObj.val());
            MessageBox.Show("Successfully exported as OBJ, check the folder you have LakiTool inside of.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Environment.GetEnvironmentVariable("sm64gamepath", EnvironmentVariableTarget.User) != null)
            {
                levelToolStripMenuItem.Enabled = true;
                LakiTool.MISC.Game.gamePath = Environment.GetEnvironmentVariable("sm64gamepath", EnvironmentVariableTarget.User);
            }
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
                renderer.SetRenderMode(LakiTool.Render.RenderMode.Collision);
                renderer.rendererObject.Colrenderer.lines = File.ReadAllLines(loadobj.FileName);
                renderer.initRenderer();
                timer1.Enabled = true;
            }
        }
    }
}
