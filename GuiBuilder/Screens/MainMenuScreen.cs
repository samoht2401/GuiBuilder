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
            OpeningTransition = new XTransTransition(Transition.Types.Opening);
            ClosingTransition = new XTransTransition(Transition.Types.Closing);

            SpriteLoader.LoadTextures(manager.OpenGL, "test", Directory.GetCurrentDirectory() + "\\Sprites\\supernova.jpg");
        }

        public override void Draw(OpenGL gl, TimeSpan elapsed, bool isInForeground)
        {
            base.Draw(gl, elapsed, isInForeground);
            int size = Math.Max(GlobalValues.ViewportWidth, GlobalValues.ViewportHeight) / 2;
            DrawHelper.glEnable2D();

            ApplyTransitionTransformation(gl);
            gl.Scale(size, size, size);
            SpriteLoader.Bind(gl, "test");
            DrawHelper.glDraw2DSprite();
            UndoTransitionTransformation(gl);
            DrawHelper.glDisable2D();
        }
    }
}
