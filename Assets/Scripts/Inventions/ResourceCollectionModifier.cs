using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventions {
    class ResourceCollectionModifier : IInvention {
        public List<GeniusCost> Costs {
            get; set;
        }

        public string Name {
            get; set;
        }

        ResourceType type;
        double modifyCollectionRateBy;

        public ResourceCollectionModifier(ResourceType type, double modifyCollectionRateBy) {
            Costs = new List<GeniusCost>();
            this.type = type;
            this.modifyCollectionRateBy = modifyCollectionRateBy;
        }

        public void Add() {
            ResourceGatherCalculator.Add(type, modifyCollectionRateBy);
        }
    }
}
