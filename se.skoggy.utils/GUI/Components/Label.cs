using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.GUI.Components
{
    public class Label : GuiComponent
    {
        public Label(GuiContext context) 
            : base(context)
        {
        }

        public override float Height
        {
            get { return Font.MeasureString(Text).Y * scale.Y; }
        }
    }
}
