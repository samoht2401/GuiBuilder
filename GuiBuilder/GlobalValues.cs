using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace GuiBuilder
{
    public static class GlobalValues
    {
        private static int[] iViewport = new int[4];

        public static int ViewportWidth { get { return iViewport[2]; } }
        public static int ViewportHeight { get { return iViewport[3]; } }

        public static void Init(OpenGL gl)
        {
            gl.GetInteger(OpenGL.GL_VIEWPORT, iViewport);
        }

        public static void Resized(OpenGL gl)
        {
            gl.GetInteger(OpenGL.GL_VIEWPORT, iViewport);
        }
    }
}
