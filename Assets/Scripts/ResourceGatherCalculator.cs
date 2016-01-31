using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts {
    public class ResourceGatherCalculator {
        static ResourceGatherCalculator instance = new ResourceGatherCalculator();

        double[] modifiers = new double[(int)ResourceType._Count];

        private ResourceGatherCalculator() {
            for(int a = 0; a < modifiers.Length; ++a) {
                modifiers[a] = 1.0;
            }
        }

        public static void Add(ResourceType type, double modifier) {
            instance.modifiers[(int)type] += modifier;
        }

        public static double Calculate(ResourceType type) {
            return instance.modifiers[(int)type];
        }
    }
}
