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
                new NewResource(ResourceType.Tree) {
                    Name = "Plant some trees",
                    Costs = {
                        new GeniusCost(School.Type.nonRoundThings, 20)
                    }
                },
                new NewResource(ResourceType.Coal) {
                    Name = "Dig up some coal",
                    Costs = {
                        new GeniusCost(School.Type.earth, 40)
                    }
                },
                new NewResource(ResourceType.Iron) {
                    Name = "Get iron",
                    Costs = {
                        new GeniusCost(School.Type.earth, 300),
                        new GeniusCost(School.Type.wind, 200)
                    }
                },
                new NewResource(ResourceType.Electricity) {
                    Name = "Lets store some sparks",
                    Costs = {
                        new GeniusCost(School.Type.electricity, 2000),
                        new GeniusCost(School.Type.machines, 4000)
                    }
                },
                new NewResource(ResourceType.Silicon) {
                    Name = "Lets store some sparks",
                    Costs = {
                        new GeniusCost(School.Type.electricity, 50000),
                        new GeniusCost(School.Type.machines, 230000),
                        new GeniusCost(School.Type.vehicles, 230000)
                    }
                },
                new NewResource(ResourceType.Silicon) {
                    Name = "Lets store some sparks",
                    Costs = {
                        new GeniusCost(School.Type.cyborgs, 50000),
                        new GeniusCost(School.Type.space, 162200),
                        new GeniusCost(School.Type.computers, 192100)
                    }
                },

                new ResourceCollectionModifier(ResourceType.Stone, 1.5) {
                    Name = "Strengthen tools",
                    Costs = {
                        new GeniusCost(School.Type.nonRoundThings, 2)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Stone, 2.0) {
                    Name = "Strengthen tools 2",
                    Costs = {
                        new GeniusCost(School.Type.nonRoundThings, 12)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Stone, 8.0) {
                    Name = "Strengthen tools 3",
                    Costs = {
                        new GeniusCost(School.Type.nonRoundThings, 200000)
                    }
                },

                new ResourceCollectionModifier(ResourceType.Tree, 5) {
                    Name = "Axes",
                    Costs = {
                        new GeniusCost(School.Type.nonRoundThings, 2)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Tree, 2.0) {
                    Name = "Chainsaws",
                    Costs = {
                        new GeniusCost(School.Type.machines, 810000),
                        new GeniusCost(School.Type.electricity, 240000)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Tree, 8.0) {
                    Name = "Lasers",
                    Costs = {
                        new GeniusCost(School.Type.computers, 1500000),
                        new GeniusCost(School.Type.space, 6200000),
                        new GeniusCost(School.Type.cyborgs, 750000)
                    }
                },

                new ResourceCollectionModifier(ResourceType.Coal, 5) {
                    Name = "Shovels",
                    Costs = {
                        new GeniusCost(School.Type.machines, 15000)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Coal, 2.0) {
                    Name = "Digger",
                    Costs = {
                        new GeniusCost(School.Type.vehicles, 810000),
                        new GeniusCost(School.Type.rocks, 24000000)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Coal, 60.0) {
                    Name = "Planetary mining",
                    Costs = {
                        new GeniusCost(School.Type.computers, 1500000),
                        new GeniusCost(School.Type.colonization, 6200000),
                        new GeniusCost(School.Type.ai, 750000)
                    }
                },

                new ResourceCollectionModifier(ResourceType.Iron, 5) {
                    Name = "Hammer",
                    Costs = {
                        new GeniusCost(School.Type.machines, 15000)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Iron, 3.0) {
                    Name = "Steel",
                    Costs = {
                        new GeniusCost(School.Type.vehicles, 810000),
                        new GeniusCost(School.Type.rocks, 24000000)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Iron, 60.0) {
                    Name = "Fusion",
                    Costs = {
                        new GeniusCost(School.Type.computers, 1500000),
                        new GeniusCost(School.Type.colonization, 6200000),
                        new GeniusCost(School.Type.dimensional, 750000)
                    }
                },

                new ResourceCollectionModifier(ResourceType.Silicon, 5) {
                    Name = "Fire",
                    Costs = {
                        new GeniusCost(School.Type.fire, 15000)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Silicon, 3.0) {
                    Name = "Dynamite",
                    Costs = {
                        new GeniusCost(School.Type.machines, 810000),
                        new GeniusCost(School.Type.philosphy, 24000000),
                        new GeniusCost(School.Type.rocks, 52000000)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Silicon, 60.0) {
                    Name = "Destory AI race",
                    Costs = {
                        new GeniusCost(School.Type.philosphy, 150000000),
                        new GeniusCost(School.Type.ai, 62000000),
                        new GeniusCost(School.Type.dimensional, 75008800)
                    }
                },

                new ResourceCollectionModifier(ResourceType.Beryllium, 5) {
                    Name = "Polish",
                    Costs = {
                        new GeniusCost(School.Type.fire, 7000),
                        new GeniusCost(School.Type.rocks, 15000)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Beryllium, 3.0) {
                    Name = "Trade routes",
                    Costs = {
                        new GeniusCost(School.Type.colonization, 81000000),
                        new GeniusCost(School.Type.philosphy, 24000000),
                        new GeniusCost(School.Type.rocks, 520000000)
                    }
                },
                new ResourceCollectionModifier(ResourceType.Beryllium, 60.0) {
                    Name = "Dimesional fissure",
                    Costs = {
                        new GeniusCost(School.Type.philosphy, 15000000000),
                        new GeniusCost(School.Type.rocks, 75008800088)
                    }
                }
            };
        }

        public static void Research(IInvention invention) {
            invention.Add();
            instance.inventions.Remove(invention);
        }
    }
}
