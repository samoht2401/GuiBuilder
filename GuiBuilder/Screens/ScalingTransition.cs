using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using Gui.Transitions;

namespace GuiBuilder.Screens
{
    public class ScalingTransition : Transition
    {
        public ScalingTransition(Transition.Types type)
            : base(type)
        {
            TotalTime = TimeSpan.FromMilliseconds(1000);
        }

        public override void ApplyTransformation(OpenGL gl)
        {
            double mult = Avancement;
            if (Type == Types.Closing)
                mult = 1 - mult;

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();

            gl.Scale(mult, mult, mult);
        }

        public override void UndoTransformation(OpenGL gl)
        {
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PopMatrix();
        }
    }
}
