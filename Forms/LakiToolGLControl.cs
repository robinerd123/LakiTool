using OpenTK;
using OpenTK.Graphics;

namespace LakiTool.Forms
{
    class LakiToolGLControl : GLControl
    {
#if DEBUG

        public LakiToolGLControl() : base(GraphicsMode.Default, 1, 0, GraphicsContextFlags.Debug)
        {

        }
#else

        public LakiToolGLControl() : base(GraphicsMode.Default, 1, 0, GraphicsContextFlags.Default)
        {

        }

#endif
    }
}