using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventions {
    public class GeniusCost {
        public GeniusCost(School.Type type, Int64 count) {
            Type = type;
            Count = count;
        }
        public School.Type Type { get; set; }
        public Int64 Count { get; set; }
    }
}
