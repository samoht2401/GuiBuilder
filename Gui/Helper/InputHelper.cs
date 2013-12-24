using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gui.Helper
{
    public static class InputHelper
    {
        public static MouseBinding AddMouseBinding(ICommand command, MouseAction action, params ModifierKeys[] modifiers)
        {
            ModifierKeys ms = ModifierKeys.None;
            foreach (ModifierKeys m in modifiers)
                ms |= m;
            return new MouseBinding(command, new MouseGesture(action, ms));
        }
    }
}
