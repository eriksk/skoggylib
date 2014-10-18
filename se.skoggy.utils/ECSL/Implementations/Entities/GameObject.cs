using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using se.skoggy.ecsl.Core;
using se.skoggy.utils.MathUtils;

namespace se.skoggy.utils.ECSL.Implementations.Entities
{
    public class GameObject : Entity
    {
        private readonly Transform _transform;

        public GameObject()
        {
            _transform = new Transform();
        }

        public Transform Transform 
        {
            get { return _transform; }
        }
    }
}
