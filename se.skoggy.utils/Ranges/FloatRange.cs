﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Ranges
{
    public class FloatRange
    {
        public float min, max;

        public FloatRange()
        {
        }

        public float Random()
        {
            return Rand.Next(min, max);
        }
    }
}
