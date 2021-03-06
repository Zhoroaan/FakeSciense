﻿using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Scripts.Inventions;

public class Schools : MonoBehaviour {

    public RectTransform SchoolPrefab;
    List<School> currentSchools = new List<School>();
    static Schools instance;

    // Use this for initialization
    void Start () {
        instance = this;
        var dimensional = new School(School.Type.dimensional, "School of dimensional things") { TermProgressSpeed = 0.3f };
        var colonization = new School(School.Type.colonization, "Colonization ministry") {TermProgressSpeed = 0.1f };
        var space = new School(School.Type.space, "Colonization ministry") {
                                    TermProgressSpeed = 0.2f,
                                    LeedsTo = {dimensional, colonization },
                                    Requirements = {Requirement(ResourceType.Iron, 2600000000000900L),
                                                        Requirement(ResourceType.Coal, 540000000000000L),
                                                        Requirement(ResourceType.Electricity, 160000000000000000L),
                                                        Requirement(ResourceType.Silicon, 160000000000000L)}
        };
        var vehicles = new School(School.Type.vehicles, "Cars and stuff ltd") {
                                        TermProgressSpeed = 0.1f,
                                        LeedsTo = { space },
                                        Requirements = {Requirement(ResourceType.Iron, 260000000900L),
                                                        Requirement(ResourceType.Coal, 54000000000L),
                                                        Requirement(ResourceType.Electricity, 16000000000000L),
                                                        Requirement(ResourceType.Silicon, 16000000000L)}
        };

        var ai = new School(School.Type.ai, "Online univeristy") { TermProgressSpeed = 0.1f };
        var cyborgs = new School(School.Type.cyborgs, "School of cybornetics") {
                                    TermProgressSpeed = 0.15f,
                                    LeedsTo = { ai },
                                        Requirements = {Requirement(ResourceType.Electricity, 160000000000000L),
                                        Requirement(ResourceType.Silicon, 1600000000000L)
                                    }
        };

        var machines = new School(School.Type.machines, "Machineries") {
                                        TermProgressSpeed = 0.4f,
                                        LeedsTo = { cyborgs, vehicles },
                                        Requirements = { Requirement(ResourceType.Iron, 19000000000L),
                                                        Requirement(ResourceType.Coal, 4300000000000L),
                                                        Requirement(ResourceType.Electricity, 16000000000L),
                                                        Requirement(ResourceType.Silicon, 160000000L)}
        };
        var water = new School(School.Type.water, "Block: Water") {
                                    TermProgressSpeed = 0.25f,
                                    LeedsTo = { machines },
                                    Requirements = { Requirement(ResourceType.Iron, 20000000), Requirement(ResourceType.Coal, 8000000000), Requirement(ResourceType.Tree, 3600000000) }
        };

        var rocks = new School(School.Type.rocks, "Geology and stone things") {TermProgressSpeed = 0.1f };
        var buildings = new School(School.Type.buildings, "Tall block development") {TermProgressSpeed = 0.1f };
        var earth = new School(School.Type.earth, "Block: Earth") {
            TermProgressSpeed = 0.1f,
            LeedsTo = { rocks, buildings },
            Requirements = { Requirement(ResourceType.Stone, 2000000), Requirement(ResourceType.Coal, 6000000), Requirement(ResourceType.Tree, 4300000) }
        };

        var roundThings = new School(School.Type.roundThings, "School of round things") {
                                        TermProgressSpeed = 0.16f,
                                        LeedsTo = { water, earth },
                                        Requirements = { Requirement(ResourceType.Tree, 20000), Requirement(ResourceType.Stone, 50000) }
        };


        var computers = new School(School.Type.computers, "Silicon university") { TermProgressSpeed = 0.1f };
        var electricity = new School(School.Type.electricity, "Ark university") {
                                        TermProgressSpeed = 0.64f,
                                        LeedsTo = { computers },
                                        Requirements = { Requirement(ResourceType.Silicon, 100000),
                                                        Requirement(ResourceType.Iron, 200000000),
                                                        Requirement(ResourceType.Electricity, 53000000) }
        };

        var arts = new School(School.Type.arts, "Art schoool") { TermProgressSpeed = 0.1f };
        var fire = new School(School.Type.fire, "Block Fire") {
            TermProgressSpeed = 0.5f,
            LeedsTo = { arts, electricity },
            Requirements = { Requirement(ResourceType.Iron, 4000),
                             Requirement(ResourceType.Tree, 10000)}
        };

        var philosphy = new School(School.Type.philosphy, "Thinking university") { TermProgressSpeed = 0.1f };
        var wind = new School(School.Type.wind, "Block: Wind") {
                                    TermProgressSpeed = 0.23f,
                                    LeedsTo = { philosphy },
                                    Requirements = { Requirement(ResourceType.Stone, 500) }
        };

        var nonRoundThings = new School(School.Type.nonRoundThings, "Nonround university") {
                                            TermProgressSpeed = 0.12f,
                                            LeedsTo = { fire, wind },
                                            Requirements = { Requirement(ResourceType.Tree, 1000) }
        };

        currentSchools.Add(roundThings);
        currentSchools.Add(nonRoundThings);

        float startDegrees = 360.0f / currentSchools.Count;

        for(int a = 0; a < currentSchools.Count; ++a) {
            currentSchools[a].Rotation = Quaternion.Euler(0, 0, startDegrees * a + 45);
            currentSchools[a].Spawn(SchoolPrefab, transform);
        }
    }

    public static bool CanSeeInvention(IInvention invention) {
        foreach(var cost in invention.Costs) {
            if(!FoundMatchingQualification(cost)) {
                return false;
            }
        }
        return true;
    }

    public static bool MeetsRequirement(GeniusCost cost) {
        Int64 count = 0;
        foreach(var s in instance.currentSchools) {
            foreach(var q in s.Qualifications) {
                if(q.SchoolType == cost.Type) {
                    count += s.NumberOfGeniusesInPool;
                }
            }
        }
        return count >= cost.Count;
    }

    private static bool FoundMatchingQualification(GeniusCost cost) {
        foreach(var s in instance.currentSchools) {
            foreach(var q in s.Qualifications) {
                if(q.SchoolType == cost.Type) {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool TryMakeInvention(IInvention invention) {
        Int64[] counters = new Int64[(int)School.Type._Count];
        foreach(var c in invention.Costs) {
            counters[(int)c.Type] = c.Count;
        }
        if(CanSeeInvention(invention) && HasEnoughGeniuses(invention)) {
            foreach(var s in instance.currentSchools) {
                foreach(var q in s.Qualifications) {
                    if(counters[(int)q.SchoolType] != 0) {
                        if(s.NumberOfGeniusesInPool > counters[(int)q.SchoolType]) {
                            s.NumberOfGeniusesInPool -= counters[(int)q.SchoolType];
                            counters[(int)q.SchoolType] = 0;
                        } else if(s.NumberOfGeniusesInPool != 0) {
                            var count = s.NumberOfGeniusesInPool;
                            s.NumberOfGeniusesInPool = 0;
                            counters[(int)q.SchoolType] -= count;
                        }
                    }
                }
            }
        }
        foreach(var c in invention.Costs) {
            if(counters[(int)c.Type] != 0) {
                return false;
            }
        }
        return true;
    }

    static bool HasEnoughGeniuses(IInvention invention) {
        Int64[] counters = new Int64[(int)School.Type._Count];

        foreach(var s in instance.currentSchools) {
            foreach(var q in s.Qualifications) {
                counters[(int)q.SchoolType] += s.NumberOfGeniusesInPool;
            }
        }

        foreach(var cost in invention.Costs) {
            if(counters[(int)cost.Type] < cost.Count) {
                return false;
            }
        }
        return true;
    }

    School.UpgradeRequirement Requirement(ResourceType type, Int64 count) {
        return new School.UpgradeRequirement(type, count);
    }

    // Update is called once per frame
    void Update () {
        for(int a = 0; a < currentSchools.Count; ++a) {
            currentSchools[a].Update();
        }
	}

    void UpdateSchool(string name) {
        for(int a = 0; a < currentSchools.Count; ++a) {
            School school = currentSchools[a];
            if(school.Name.Equals(name) && school.CanUpgrade()) {
                float startDegrees = 360.0f / currentSchools.Count;
                for(int b = 0; b < school.LeedsTo.Count; ++b) {
                    var newSchool = school.LeedsTo[b];
                    newSchool.Qualifications.AddRange(school.Qualifications);
                    newSchool.Rotation = Quaternion.Euler(0, 0, startDegrees * a);
                    newSchool.Spawn(SchoolPrefab, transform);
                    currentSchools.Insert(a, newSchool);
                    transform.root.BroadcastMessage("AddText", "Started new institution: " + newSchool.Name);
                }

                foreach(var requirement in school.Requirements) {
                    var resource = BuildResource.GetResource(requirement.ResourceType);
                    resource.Count -= requirement.Count;
                }

                Destroy(school.SchoolReference);
                currentSchools.Remove(school);
                UpdateSchoolPositions();
            }
        }
    }

    private void UpdateSchoolPositions() {
        float startDegrees = 360.0f / currentSchools.Count;

        for(int a = 0; a < currentSchools.Count; ++a) {
            currentSchools[a].UpdatePosition(Quaternion.Euler(0, 0, startDegrees * a + 45));
        }
    }
}
