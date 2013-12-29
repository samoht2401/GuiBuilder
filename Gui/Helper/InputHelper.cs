using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpGL.WPF;
using Gui.Bounds;

namespace Gui.Helper
{
    public static class InputHelper
    {
        public static Point MousePosition { get; private set; }
        public static void Update(int width, int height, System.Windows.Point winMousePos)
        {
            MousePosition = Point.New((int)winMousePos.X - width / 2, (int)winMousePos.Y - height / 2);
        }

        public static event MouseEventHandler MouseMoveEvent;
        public static void MouseMove(object sender, MouseEventArgs e)
        {
            MouseMoveEvent(sender, e);
        }

        public static event MouseButtonEventHandler MouseButtonDownEvent;
        public static void MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseButtonDownEvent(sender, e);
        }

        public static MouseBinding AddMouseBinding(ICommand command, MouseAction action, params ModifierKeys[] modifiers)
        {
            ModifierKeys ms = ModifierKeys.None;
            foreach (ModifierKeys m in modifiers)
                ms |= m;
            return new MouseBinding(command, new MouseGesture(action, ms));
        }
    }
}
