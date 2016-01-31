using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventions {
    class NewResource : IInvention {
        public List<GeniusCost> Costs {
            get; set;
        }

        public string Name {
            get; set;
        }

        ResourceType type;
        double modifyCollectionRateBy;

        public NewResource(ResourceType type) {
            Costs = new List<GeniusCost>();
            this.type = type;
            this.modifyCollectionRateBy = modifyCollectionRateBy;
        }

        public void Add() {
            BuildResource.GetResource(type).Unlocked = true;
        }
    }
}
