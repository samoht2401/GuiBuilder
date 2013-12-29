using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gui.Bounds;
using Gui.Helper;
using Gui.Sprites;
using SharpGL;

namespace Gui.Controls
{
    public abstract class Control
    {
        public string Name { get; protected set; }
        public Control Parent { get; protected set; }
        public Dictionary<string, Control> Childs { get; protected set; }
        public Bound Bound { get; protected set; }
        public CompoundSprite Sprites { get; protected set; }
        public bool HasFocus { get; protected set; }
        public bool ChildHasFocus { get; protected set; }

        protected bool wasMouseInside;
        public event MouseEventHandler MouseEnter;
        public event MouseEventHandler MouseLeave;
        public event MouseEventHandler MouseMove;
        public event MouseButtonEventHandler MouseButtonDown;

        public Control(CompoundSprite sprites)
        {
            Childs = new Dictionary<string, Control>();
            Sprites = sprites;
        }

        public void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (MouseMove != null)
                MouseMove(this, e);
            if (Bound.Intersect(InputHelper.MousePosition))
            {
                if (!wasMouseInside)
                {
                    wasMouseInside = true;
                    if (MouseEnter != null)
                        MouseEnter(this, e);
                }
            }
            else
            {
                if (wasMouseInside)
                {
                    wasMouseInside = false;
                    if (MouseLeave != null)
                        MouseLeave(this, e);
                }
            }
            foreach (Control c in Childs.Values)
                c.MouseMoveEvent(sender, e);
        }
        public void MouseButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            if (e.Handled)
                return;
            foreach (Control c in Childs.Values)
            {
                c.MouseButtonDownEvent(sender, e);
                if (e.Handled)
                    return;
            }
            if (Bound.Intersect(InputHelper.MousePosition) && MouseButtonDown != null)
                MouseButtonDown(this, e);
        }

        public virtual void Update(TimeSpan elapsed) { }
        public virtual void Draw(OpenGL gl, TimeSpan elapsed) { }
        public virtual void UpdateChilds(TimeSpan elapsed)
        {
            foreach (Control c in Childs.Values)
                Update(elapsed);
        }
        public virtual void DrawChilds(OpenGL gl, TimeSpan elapsed)
        {
            foreach (Control c in Childs.Values)
                Draw(gl, elapsed);
        }
    }
}
