﻿using FIMAE.FIMAS.ExpertSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.DefiningFeatures
{
    [Serializable]
    public class DefiningFeature : IExpertVariable
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }

        public DefiningFeature()
        {
        }

        public DefiningFeature(string name, List<string> values)
        {
            Name = name;
            Values = values;
        }
    }
}
