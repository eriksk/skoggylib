using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using FarseerPhysics.Dynamics;

namespace se.skoggy.utils.Physics.Objects
{
    public interface IPhysicsBody
    {
        Body Body { get; set; }
    }
}
