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
using Gui.Controls;
using Gui.Bounds;
using Gui.Sprites;

namespace GuiBuilder.Screens
{
    public class MainMenuScreen : Screen
    {
        public MainMenuScreen(ScreenManager manager)
            : base(manager)
        {
            OpeningTransition = new TransTransition(Transition.Types.Opening, TransTransition.Directions.Up);
            ClosingTransition = new TransTransition(Transition.Types.Closing, TransTransition.Directions.Down);

            CompoundSprite sprites = new CompoundSprite();
            sprites.Add("Idle", new Sprite(SpriteLoader.LoadTextures(manager.OpenGL, "button_idle", Directory.GetCurrentDirectory() + "\\Sprites\\pieuvre.png"), 64, 64));
            sprites.Add("Overflew", new Sprite(SpriteLoader.LoadTextures(manager.OpenGL, "button_overflew", Directory.GetCurrentDirectory() + "\\Sprites\\pieuvre_ombre.png"), 64, 64));
            sprites.Add("Pressed", new Sprite(SpriteLoader.LoadTextures(manager.OpenGL, "button_pressed", Directory.GetCurrentDirectory() + "\\Sprites\\pieuvre2.png"), 64, 64));
            Controls.Add("button", new Button(sprites, RectangleBound.New(-100, -100, 100, 100), 500));
            ((Button)Controls["button"]).Click += button_MouseButtonDown;

            SpriteLoader.LoadTextures(manager.OpenGL, "test", Directory.GetCurrentDirectory() + "\\Sprites\\supernova.jpg");
        }

        void button_MouseButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Manager.CloseAllAndThenOpen(new MainMenuScreen(Manager));
        }

        public override void Draw(OpenGL gl, TimeSpan elapsed, bool isInForeground)
        {
            base.Draw(gl, elapsed, isInForeground);
            int size = Math.Max(GlobalValues.ViewportWidth, GlobalValues.ViewportHeight) / 2;
            DrawHelper.glEnable2D();

            ApplyTransitionTransformation(gl);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            gl.Scale(size, size, size);
            SpriteLoader.Bind(gl, "test");
            DrawHelper.glDraw2DSprite();
            gl.PopMatrix();

            DrawControls(gl, elapsed, isInForeground);

            UndoTransitionTransformation(gl);
            DrawHelper.glDisable2D();
        }
    }
}
