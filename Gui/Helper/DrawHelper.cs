using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL;

namespace Gui.Helper
{
    public static class DrawHelper
    {
        public static void glEnable2D(OpenGL gl)
        {
            int[] iViewport = new int[4];

            // Get a copy of the viewport
            gl.GetInteger(OpenGL.GL_VIEWPORT, iViewport);

            // Save a copy of the projection matrix so that we can restore it 
            // when it's time to do 3D rendering again.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.PushMatrix();
            gl.LoadIdentity();

            // Set up the orthographic projection
            gl.Ortho(iViewport[0], iViewport[0] + iViewport[2], iViewport[1] + iViewport[3], iViewport[1], -1, 1);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            gl.LoadIdentity();

            // Make sure depth testing and lighting are disabled for 2D rendering until we are finished rendering in 2D
            gl.PushAttrib(OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_LIGHTING_BIT);
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            gl.Disable(OpenGL.GL_LIGHTING);
        }

        public static void glDisable2D(OpenGL gl)
        {
            gl.PopAttrib();
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.PopMatrix();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PopMatrix();
        }
    }
}
