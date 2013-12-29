using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using Gui.Transitions;

namespace GuiBuilder.Screens
{
    public class TransTransition : Transition
    {
        public enum Directions
        {
            Left,
            Right,
            Up,
            Down
        };

        public Directions Direction { get; set; }

        public TransTransition(Transition.Types type, Directions dir)
            : base(type)
        {
            TotalTime = TimeSpan.FromMilliseconds(2000);
            Direction = dir;
        }

        public override void ApplyTransformation(OpenGL gl)
        {
            double mult = Avancement;
            if (Type == Types.Closing)
                mult = 1 - mult;

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            double trans = 0;
            if (Direction == Directions.Left || Direction == Directions.Up)
                trans = (1 - mult) * -GlobalValues.ViewportWidth / 2;
            if (Direction == Directions.Right || Direction == Directions.Down)
                trans = (1 - mult) * GlobalValues.ViewportWidth / 2;
            if (Direction == Directions.Left || Direction == Directions.Right)
                gl.Translate(trans, 0, 0);
            if (Direction == Directions.Up || Direction == Directions.Down)
                gl.Translate(0, trans, 0);

            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.Color(1, 1, 1, mult);
        }

        public override void UndoTransformation(OpenGL gl)
        {
            gl.Color(1, 1, 1, 1);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PopMatrix();
        }
    }
}
