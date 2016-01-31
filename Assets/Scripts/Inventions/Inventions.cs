using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventions {
    public class Inventions {
        List<IInvention> inventions;
        static Inventions instance = new Inventions();

        static public List<IInvention> InventionList {
            get {
                return instance.inventions;
            }
        }

        private Inventions() {
            inventions = new List<IInvention>() {
                new ResourceCollectionModifier(ResourceType.Stone, 1.5) {
                    Name = "Sharpen Tools",
                    Costs = {
                        new GeniusCost(School.Type.nonRoundThings, 2) }
                },
                new ResourceCollectionModifier(ResourceType.Stone, 2.0) {
                    Name = "Sharpen Tools 2",
                    Costs = {
                        new GeniusCost(School.Type.nonRoundThings, 12) }
                }
            };
        }

        public static void Research(IInvention invention) {
            invention.Add();
            instance.inventions.Remove(invention);
        }
    }
}
