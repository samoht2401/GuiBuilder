using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using Gui.Transitions;

namespace GuiBuilder.Screens
{
    public class XTransTransition : Transition
    {
        public XTransTransition(Transition.Types type)
            : base(type)
        {
            TotalTime = TimeSpan.FromMilliseconds(2000);
        }

        public override void ApplyTransformation(OpenGL gl)
        {
            double mult = Avancement;
            if (Type == Types.Closing)
                mult = 1 - mult;

            gl.Enable(OpenGL.GL_BLEND);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            gl.Translate((1 - mult) * GlobalValues.ViewportWidth / 2, 0, 0);

            gl.BlendFunc(OpenGL.GL_ONE, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.Color(mult, mult, mult, mult);
        }

        public override void UndoTransformation(OpenGL gl)
        {
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PopMatrix();
        }
    }
}
