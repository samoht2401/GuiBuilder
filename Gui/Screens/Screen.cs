using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using Gui.Transitions;

namespace Gui.Screens
{
    public abstract class Screen
    {
        public enum States
        {
            Sleeping,
            Opening,
            Opened,
            Closing,
            FullyClosed
        };

        public ScreenManager Manager { get; private set; }
        //Transition part
        public States State { get; protected set; }
        public Transition OpeningTransition;
        public Transition ClosingTransition;

        public Screen(ScreenManager manager)
        {
            Manager = manager;
            State = States.Sleeping;
        }

        public virtual void Open()
        {
            State = States.Opening;
            OpeningTransition.Start();
        }
        public virtual void Close()
        {
            State = States.Closing;
            ClosingTransition.Start();
        }

        public virtual void Update(TimeSpan elapsed, bool isInForeground)
        {
            switch (State)
            {
                case States.Sleeping: return;
                case States.Opening:
                    {
                        if (OpeningTransition.ActualState == Transition.States.Finish)
                            State = States.Opened;
                        break;
                    }
                case States.Opened: return;
                case States.Closing:
                    {
                        if (ClosingTransition.ActualState == Transition.States.Finish)
                            State = States.FullyClosed;
                        break;
                    }
                case States.FullyClosed: return;
            }
        }
        public virtual void Draw(OpenGL gl, TimeSpan elapsed, bool isInForeground)
        {
            if (State == States.Opening)
                OpeningTransition.Update(elapsed);
            else if (State == States.Closing)
                ClosingTransition.Update(elapsed);
        }
        protected void ApplyTransitionTransformation(OpenGL gl)
        {
            if (State == States.Opening || State == States.Opened || State == States.Sleeping)
                OpeningTransition.ApplyTransformation(gl);
            else if (State == States.Closing || State == States.FullyClosed)
                ClosingTransition.ApplyTransformation(gl);
        }
        protected void UndoTransitionTransformation(OpenGL gl)
        {
            if (State == States.Opening)
                OpeningTransition.UndoTransformation(gl);
            else if (State == States.Closing)
                ClosingTransition.UndoTransformation(gl);
        }
    }
}
