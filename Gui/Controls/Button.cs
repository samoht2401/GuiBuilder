using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Gui.Bounds;
using Gui.Helper;
using Gui.Sprites;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.Drawing;
using SharpGL.SceneGraph;

namespace Gui.Controls
{
    public class Button : Control
    {
        public enum States
        {
            Idle,
            Overflew,
            Pressed
        };

        public TimeSpan PressedTime { get; set; }
        private TimeSpan timeRemainingPressed;
        public States State { get; protected set; }

        public event MouseButtonEventHandler Click;

        public Button(CompoundSprite sprites, Bound bound, double clickLenght)
            : base(sprites)
        {
            PressedTime = TimeSpan.FromMilliseconds(clickLenght);
            timeRemainingPressed = PressedTime;
            Bound = bound;
            base.MouseEnter += Button_MouseEnter;
            base.MouseLeave += Button_MouseLeave;
            base.MouseButtonDown += Button_MouseButtonDown;
        }

        void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (State != States.Pressed)
                State = States.Idle;
        }
        void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (State != States.Pressed)
                State = States.Overflew;
        }
        void Button_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && State != States.Pressed)
            {
                State = States.Pressed;
                if (Click != null)
                    Click(this, e);
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            if (State == States.Pressed)
            {
                timeRemainingPressed -= elapsed;
                if (timeRemainingPressed.TotalMilliseconds <= 0)
                {
                    if (Bound.Intersect(InputHelper.MousePosition))
                        State = States.Overflew;
                    else
                        State = States.Idle;
                    timeRemainingPressed = PressedTime;
                }
            }
            UpdateChilds(elapsed);
        }
        public override void Draw(OpenGL gl, TimeSpan elapsed)
        {
            Sprite currentSprite = Sprites.Sprites[State.ToString()];
            currentSprite.Texture.Bind(gl);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            int scaleX = Bound.getMaxWidth() / 2;
            int scaleY = Bound.getMaxHeight() / 2;
            double transX = Bound.getMinX() + scaleX;
            double transY = Bound.getMinY() + scaleY;
            gl.Translate(transX, transY, 0);
            gl.Scale(scaleX, scaleY, 0);

            DrawHelper.glDraw2DSprite();
            gl.PopMatrix();
        }
    }
}
