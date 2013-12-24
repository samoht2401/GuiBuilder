using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace Gui.Screens
{
    public class ScreenManager
    {
        public OpenGL OpenGL { get; protected set; }

        private Stack<Screen> Screens;
        private Screen toOpenWhenCleared;

        public ScreenManager(OpenGL gl)
        {
            OpenGL = gl;
            Screens = new Stack<Screen>();
        }

        public void OpenScreen(Screen toOpen)
        {
            Screens.Push(toOpen);
            toOpen.Open();
        }

        public void CloseScreen()
        {
            Screens.Peek().Close();
        }

        public void CloseAllAndThenOpen(Screen toOpen)
        {
            foreach (Screen toClose in Screens)
                toClose.Close();
            toOpenWhenCleared = toOpen;
        }

        public void Update(TimeSpan elapsedTime)
        {
            Screen foregroundScreen = Screens.Peek();
            List<Screen> Temp = Screens.ToList();
            foreach (Screen screen in Temp)
            {
                if(screen.State == Screen.States.FullyClosed)
                {
                    if (screen == foregroundScreen)
                        Screens.Pop();
                    else
                        continue;
                }
                screen.Update(elapsedTime, screen == foregroundScreen);
            }
            if (toOpenWhenCleared != null && Screens.Count == 0)
            {
                OpenScreen(toOpenWhenCleared);
                toOpenWhenCleared = null;
            }
        }

        public void Draw(OpenGL gl, TimeSpan elapsed)
        {
            Screen foregroundScreen = Screens.Peek();
            Screens.Reverse();
            foreach (Screen screen in Screens)
            {
                screen.Draw(gl, elapsed, screen == foregroundScreen);
            }
            Screens.Reverse();
        }
    }
}
