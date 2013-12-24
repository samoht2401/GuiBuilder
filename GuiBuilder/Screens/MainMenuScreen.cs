using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SharpGL;
using Gui.Screens;
using Gui.Transitions;
using Gui.Helper;
using GuiBuilder.Sprites;

namespace GuiBuilder.Screens
{
    public class MainMenuScreen : Screen
    {
        public MainMenuScreen(ScreenManager manager)
            : base(manager)
        {
            OpeningTransition = new ScalingTransition(Transition.Types.Opening);
            ClosingTransition = new XTransTransition(Transition.Types.Closing);

            SpriteLoader.LoadTextures(manager.OpenGL, "test", Directory.GetCurrentDirectory() + "\\Sprites\\supernova.jpg");
        }

        public override void Draw(OpenGL gl, TimeSpan elapsed, bool isInForeground)
        {
            base.Draw(gl, elapsed, isInForeground);
            int size = Math.Max(GlobalValues.ViewportWidth, GlobalValues.ViewportHeight) / 2;
            DrawHelper.glEnable2D(gl);
            ApplyTransitionTransformation(gl);

            SpriteLoader.Bind(gl, "test");

            gl.Begin(OpenGL.GL_QUAD_STRIP);
            gl.TexCoord(0.0f, 0.0f, 0.0f);
            gl.Vertex(-size, -size, 0.0f);
            gl.TexCoord(0.0f, 1.0f, 0.0f);
            gl.Vertex(-size, size, 0.0f);
            gl.TexCoord(1.0f, 0.0f, 0.0f);
            gl.Vertex(size, -size, 0.0f);
            gl.TexCoord(1.0f, 1.0f, 0.0f);
            gl.Vertex(size, size, 0.0f);
            gl.End();

            UndoTransitionTransformation(gl);
            DrawHelper.glDisable2D(gl);
        }
    }
}
