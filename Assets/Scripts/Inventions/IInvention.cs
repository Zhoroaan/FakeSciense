using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventions {
    public interface IInvention {
        string Name { get; set; }
        List<GeniusCost> Costs  { get; set; }
        void Add();
    }
}
