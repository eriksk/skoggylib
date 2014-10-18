using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using se.skoggy.ecsl.Core;
using se.skoggy.utils.ECSL.Implementations.Entities;
using se.skoggy.utils.MathUtils;

namespace se.skoggy.utils.ECSL.Implementations.Components
{
    public class GameObjectComponent : Component
    {
        private GameObject _gameObject;

        public GameObject GameObject { get { return (GameObject)Entity; } }
        public Transform Transform { get { return _gameObject.Transform; } }
        public override Entity Entity 
        {
            get { return _gameObject; }
            set { _gameObject = (GameObject)value; }
        }
    }
}
