using UnityEngine;
using System.Collections.Generic;
using System;

public class Schools : MonoBehaviour {

    public RectTransform SchoolPrefab;
    List<School> currentSchools = new List<School>();

    // Use this for initialization
    void Start () {
        var dimensional = new School() { Name = "School of dimensional things", TermProgressSpeed = 0.3f };
        var colonization = new School() { Name = "Colonization ministry", TermProgressSpeed = 0.1f };
        var space = new School() { Name = "Colonization ministry", TermProgressSpeed = 0.2f, LeedsTo = {dimensional, colonization } };
        var vehicles = new School() { Name = "Cars and stuff ltd", TermProgressSpeed = 0.1f, LeedsTo = { space } };

        var ai = new School() { Name = "Online univeristy", TermProgressSpeed = 0.1f };
        var cyborgs = new School() { Name = "School of cybornetics", TermProgressSpeed = 0.1f, LeedsTo = { ai } };

        var machines = new School() { Name = "Machineries", TermProgressSpeed = 0.1f, LeedsTo = { cyborgs, vehicles } };
        var water = new School() { Name = "Block: Water", TermProgressSpeed = 0.1f, LeedsTo = { machines } };

        var rocks = new School() { Name = "Geology and stone things", TermProgressSpeed = 0.1f };
        var buildings = new School() { Name = "Tall block development", TermProgressSpeed = 0.1f };
        var earth = new School() { Name = "Block: Earth", TermProgressSpeed = 0.1f, LeedsTo = { rocks, buildings } };

        var roundThings = new School() { Name = "School of round things", TermProgressSpeed = 0.1f, LeedsTo = { water, earth } };


        var computers = new School() { Name = "Silicon university", TermProgressSpeed = 0.1f };
        var electricity = new School() { Name = "Ark university", TermProgressSpeed = 0.1f, LeedsTo = { computers } };

        var arts = new School() { Name = "Art schoool", TermProgressSpeed = 0.1f };
        var fire = new School() { Name = "Block Fire", TermProgressSpeed = 0.1f, LeedsTo = { arts, electricity } };

        var philosphy = new School() { Name = "Thinking university", TermProgressSpeed = 0.1f };
        var wind = new School() { Name = "Block: Wind", TermProgressSpeed = 0.1f, LeedsTo = { philosphy } };

        var nonRoundThings = new School() { Name = "Nonround university", TermProgressSpeed = 0.1f, LeedsTo = { fire, wind } };

        currentSchools.Add(roundThings);
        currentSchools.Add(nonRoundThings);

        float startDegrees = 360.0f / currentSchools.Count;

        for(int a = 0; a < currentSchools.Count; ++a) {
            currentSchools[a].Rotation = Quaternion.Euler(0, 0, startDegrees * a + 45);
            currentSchools[a].Spawn(SchoolPrefab, transform);
        }
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
            if(school.Name.Equals(name) && school.LeedsTo.Count > 0) {
                float startDegrees = 360.0f / currentSchools.Count;
                for(int b = 0; b < school.LeedsTo.Count; ++b) {
                    var newSchool = school.LeedsTo[b];
                    newSchool.Rotation = Quaternion.Euler(0, 0, startDegrees * a);
                    newSchool.Spawn(SchoolPrefab, transform);
                    currentSchools.Insert(a, newSchool);
                    transform.root.BroadcastMessage("AddText", "Started new institution: " + newSchool.Name);
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
