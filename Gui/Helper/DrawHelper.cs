using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL;
using System.Runtime.InteropServices;

namespace Gui.Helper
{
    public static class DrawHelper
    {
        public static OpenGL gl { get; private set; }

        public static void Init(OpenGL gl)
        {
            DrawHelper.gl = gl;
        }

        public static void glEnable2D()
        {
            int[] iViewport = new int[4];

            // Get a copy of the viewport
            gl.GetInteger(OpenGL.GL_VIEWPORT, iViewport);

            int width = iViewport[2];
            int height = iViewport[3];
            int left = -width / 2;
            int right = width / 2;
            int top = -height / 2;
            int bottom = height / 2;

            // Save a copy of the projection matrix so that we can restore it 
            // when it's time to do 3D rendering again.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.PushMatrix();
            gl.LoadIdentity();

            // Set up the orthographic projection
            gl.Ortho(left, right, bottom, top, -1, 1);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            gl.LoadIdentity();

            // Make sure depth testing and lighting are disabled for 2D rendering until we are finished rendering in 2D
            gl.PushAttrib(OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_LIGHTING_BIT | OpenGL.GL_TEXTURE_2D);
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Color(1f, 1f, 1f, 1f);
        }

        public static void glDisable2D()
        {
            gl.PopAttrib();
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.PopMatrix();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PopMatrix();
        }

        private static uint listIndex = uint.MaxValue;
        public static void glDraw2DSprite()
        {
            if (listIndex == uint.MaxValue)
            {
                listIndex = gl.GenLists(1);
                gl.NewList(listIndex, OpenGL.GL_COMPILE);
                gl.Begin(OpenGL.GL_QUAD_STRIP);
                gl.TexCoord(0.0f, 0.0f, 0.0f);
                gl.Vertex(-1, -1, 0.0f);
                gl.TexCoord(0.0f, 1.0f, 0.0f);
                gl.Vertex(-1, 1, 0.0f);
                gl.TexCoord(1.0f, 0.0f, 0.0f);
                gl.Vertex(1, -1, 0.0f);
                gl.TexCoord(1.0f, 1.0f, 0.0f);
                gl.Vertex(1, 1, 0.0f);
                gl.End();
                gl.EndList();
            }
            gl.CallList(listIndex);
        }
    }
}
