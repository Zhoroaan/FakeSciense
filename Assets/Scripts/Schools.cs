using UnityEngine;
using System.Collections.Generic;
using System;

public class Schools : MonoBehaviour {

    public RectTransform SchoolPrefab;
    List<School> currentSchools = new List<School>();

    // Use this for initialization
    void Start () {
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
                                    TermProgressSpeed = 0.1f,
                                    LeedsTo = { ai },
                                        Requirements = {Requirement(ResourceType.Electricity, 160000000000000L),
                                        Requirement(ResourceType.Silicon, 1600000000000L)
                                    }
        };

        var machines = new School(School.Type.machines, "Machineries") {
                                        TermProgressSpeed = 0.1f,
                                        LeedsTo = { cyborgs, vehicles },
                                        Requirements = { Requirement(ResourceType.Iron, 19000000000L),
                                                        Requirement(ResourceType.Coal, 4300000000000L),
                                                        Requirement(ResourceType.Electricity, 16000000000L),
                                                        Requirement(ResourceType.Silicon, 160000000L)}
        };
        var water = new School(School.Type.water, "Block: Water") {
                                    TermProgressSpeed = 0.1f,
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
                                        TermProgressSpeed = 0.1f,
                                        LeedsTo = { water, earth },
                                        Requirements = { Requirement(ResourceType.Tree, 20000), Requirement(ResourceType.Stone, 50000) }
        };


        var computers = new School(School.Type.computers, "Silicon university") { TermProgressSpeed = 0.1f };
        var electricity = new School(School.Type.electricity, "Ark university") {
                                        TermProgressSpeed = 0.1f,
                                        LeedsTo = { computers },
                                        Requirements = { Requirement(ResourceType.Silicon, 100000),
                                                        Requirement(ResourceType.Iron, 200000000),
                                                        Requirement(ResourceType.Electricity, 53000000) }
        };

        var arts = new School(School.Type.arts, "Art schoool") { TermProgressSpeed = 0.1f };
        var fire = new School(School.Type.fire, "Block Fire") {
            TermProgressSpeed = 0.1f,
            LeedsTo = { arts, electricity },
            Requirements = { Requirement(ResourceType.Iron, 4000),
                             Requirement(ResourceType.Tree, 10000)}
        };

        var philosphy = new School(School.Type.philosphy, "Thinking university") { TermProgressSpeed = 0.1f };
        var wind = new School(School.Type.wind, "Block: Wind") {
                                    TermProgressSpeed = 0.1f,
                                    LeedsTo = { philosphy },
                                    Requirements = { Requirement(ResourceType.Stone, 500) }
        };

        var nonRoundThings = new School(School.Type.nonRoundThings, "Nonround university") {
                                            TermProgressSpeed = 0.1f,
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
