using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using se.skoggy.utils.ECSL.Rendering;

namespace se.skoggy.utils.ECSL.Interfaces
{
    public interface IRenderer
    {
        void Render(IRenderContext renderContext);
    }
}
