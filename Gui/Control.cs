using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gui.Bounds;

namespace Gui
{
    public abstract class Control
    {
        public string Name { get; protected set; }
        public Control Parent { get; protected set; }
        public Dictionary<string, Control> Childs { get; protected set; }
        public Bound Bound { get; protected set; }

        public Control(){ }
    }
}
