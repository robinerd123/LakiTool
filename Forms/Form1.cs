using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LakiTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LakiToolGLControl1_Load(object sender, EventArgs e)
        {
            InitialiseView();
            ResetCamera();
        }

        private void LakiToolGLControl1_Resize(object sender, EventArgs e)
        {
            UpdateProjectionMatrix();
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
                renderer.rendererObject.F3Drenderer.seq = true;
                renderer.initRenderer();

                StartRenderLoop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetCamera();
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

                StartRenderLoop();
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

                StartRenderLoop();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopRenderLoop();
        }

        #region Input

        Vector2 PreviousMousePosition = new Vector2();
        bool mic = false;

        private void checkkeys(float deltaTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state[Key.Plus] || state[Key.KeypadPlus])
                if (trackBar1.Value < trackBar1.Maximum) trackBar1.Value++;
            if (state[Key.Minus] || state[Key.KeypadMinus])
                if (trackBar1.Value > trackBar1.Minimum) trackBar1.Value--;

            Vector3 movement = Vector3.Zero;
            if (state[Key.W]) movement += Vector3.UnitY;
            if (state[Key.S]) movement += -Vector3.UnitY;
            if (state[Key.D]) movement += Vector3.UnitX;
            if (state[Key.A]) movement += -Vector3.UnitX;
            if (state[Key.E]) movement += Vector3.UnitZ;
            if (state[Key.Q]) movement += -Vector3.UnitZ;

            if (movement.LengthSquared > 0.05f) movement.NormalizeFast();

            movement = Vector3.Multiply(movement, (trackBar1.Value + 1) * deltaTime * 60f);
            cam.Move(movement.X, movement.Y, movement.Z);
        }

        private void checkmouse()
        {
            MouseState mstate = Mouse.GetState();

            Vector2 CurrentMousePosition = new Vector2(mstate.X, mstate.Y);

            if (mstate.IsButtonDown(MouseButton.Left) && mic)
            {
                Vector2 delta = CurrentMousePosition - PreviousMousePosition;
                cam.AddRotation(-delta.X, -delta.Y);

                Rectangle glControlScreenRect = lakiToolGLControl1.RectangleToScreen(lakiToolGLControl1.ClientRectangle);
                if (Cursor.Position.X < glControlScreenRect.Left) Cursor.Position = new Point(glControlScreenRect.Right, Cursor.Position.Y);
                else if (Cursor.Position.X > glControlScreenRect.Right) Cursor.Position = new Point(glControlScreenRect.Left, Cursor.Position.Y);
                if (Cursor.Position.Y < glControlScreenRect.Top) Cursor.Position = new Point(Cursor.Position.X, glControlScreenRect.Bottom);
                else if (Cursor.Position.Y > glControlScreenRect.Bottom) Cursor.Position = new Point(Cursor.Position.X, glControlScreenRect.Top);
            }

            PreviousMousePosition = CurrentMousePosition;
        }

        private void LakiToolGLControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mic = true;
        }

        private void LakiToolGLControl1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mic = false;
        }

        #endregion

        #region Render Loop

        Render.Renderer renderer = new Render.Renderer();
        Camera cam = new Camera();

        bool isRenderLoopEnabled;

        private void StartRenderLoop()
        {
            if (isRenderLoopEnabled) return;
            isRenderLoopEnabled = true;

            Application.Idle += Application_Idle;
            InitializeTimer();
        }

        private void StopRenderLoop()
        {
            if (!isRenderLoopEnabled) return;
            isRenderLoopEnabled = false;

            renderer.Dispose();

            StopTimer();
            Application.Idle -= Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (!ContainsFocus) return;

            while (lakiToolGLControl1.IsIdle)
            {
                Update(deltaTime);
            }
        }

        private void Update(float deltaTime)
        {
            checkkeys(deltaTime);
            checkmouse();

            Render(lakiToolGLControl1);

            ProcessTimer();
        }

        private void Render(GLControl RenderPanel)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = cam.GetViewMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            Vector3 lookat = new Vector3();
            lookat.X = (float)(Math.Cos((float)cam.Orientation.X) * Math.Cos((float)cam.Orientation.Y));
            lookat.Y = (float)Math.Sin((float)cam.Orientation.Y);
            lookat.Z = (float)(Math.Sin((float)cam.Orientation.X) * Math.Cos((float)cam.Orientation.Y));
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { Math.Abs(lookat.X), Math.Abs(lookat.Y), Math.Abs(lookat.Z), 0.0f });

            renderer.Render();

            RenderPanel.SwapBuffers();
        }

        private void InitialiseView()
        {
            GL.ClearColor(Color.LightBlue);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0.05f);

            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.Texture2D);

            GL.ShadeModel(ShadingModel.Smooth);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

#if DEBUG
            InitializeDebugMessages();
#endif
        }

        private void UpdateProjectionMatrix()
        {
            Rectangle renderRect = lakiToolGLControl1.ClientRectangle;
            GL.Viewport(renderRect.X, renderRect.Y, renderRect.Width, renderRect.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(1.0f, renderRect.Width / (float)renderRect.Height, 1f, 1000.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        private void ResetCamera()
        {
            cam.Position = Vector3.Zero;
            cam.Orientation = Vector3.UnitZ;
        }

        #endregion

        #region Timers

        Stopwatch stopwatch;

        float totalElapsedTime;
        float deltaTime;

        float lastFPSUpdate;
        int framesCounted;

        const float FPS_UPDATE_INTERVAL = 0.5f;

        void InitializeTimer()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            label1.Visible = true;
        }

        void ProcessTimer()
        {
            float currentTime = stopwatch.ElapsedMilliseconds / 1000.0f;

            deltaTime = currentTime - totalElapsedTime;
            totalElapsedTime = currentTime;

            PrintFPS();
        }

        void StopTimer()
        {
            label1.Visible = false;
            stopwatch.Stop();
        }

        void PrintFPS()
        {
            framesCounted++;
            if (totalElapsedTime - lastFPSUpdate >= FPS_UPDATE_INTERVAL)
            {
                label1.Text = $"{framesCounted * (1 / FPS_UPDATE_INTERVAL)} FPS";
                framesCounted = 0;
                lastFPSUpdate = totalElapsedTime;
            }
        }

        #endregion

        #region Debug

#if DEBUG

        DebugProc DebugProcCallback;

        void InitializeDebugMessages()
        {
            GL.Enable(EnableCap.DebugOutput);
            DebugProcCallback = DebugMessage;
            GL.DebugMessageCallback(DebugProcCallback, IntPtr.Zero);
        }

        void DebugMessage(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
        {
            var msg = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(message, length);
            Console.WriteLine(string.Format("{0} {1} {2}", severity, type, msg));
        }

        //static System.Runtime.InteropServices.GCHandle _logCallbackHandle;
        //static readonly DebugProcArb debugDelegate = new DebugProcArb(DebugMessage);

        //void InitializeDebugMessages()
        //{
        //    GL.Enable(EnableCap.DebugOutput);
        //    GL.Enable(EnableCap.DebugOutputSynchronous);
        //    _logCallbackHandle = System.Runtime.InteropServices.GCHandle.Alloc(debugDelegate);
        //    var nullptr = new IntPtr(0);
        //    GL.Arb.DebugMessageCallback(debugDelegate, nullptr);
        //}

        //static void DebugMessage(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
        //{
        //    var msg = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(message, length);
        //    Console.WriteLine(string.Format("{0} {1} {2}", severity, type, msg));
        //}

#endif

        #endregion
    }
}