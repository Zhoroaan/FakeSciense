using UnityEngine;
using System.Collections.Generic;
using System;

public class Schools : MonoBehaviour {

    public RectTransform SchoolPrefab;
    List<School> currentSchools = new List<School>();

	// Use this for initialization
	void Start () {
        var dimensional = new School() { Name = "School of dimensional things", TermProgressSpeed = 0.03f };
        var colonization = new School() { Name = "Colonization ministry", TermProgressSpeed = 0.01f };
        var space = new School() { Name = "Colonization ministry", TermProgressSpeed = 0.02f, LeedsTo = {dimensional, colonization } };
        var vehicles = new School() { Name = "Cars and stuff ltd", TermProgressSpeed = 0.01f, LeedsTo = { space } };

        var ai = new School() { Name = "Online univeristy", TermProgressSpeed = 0.01f };
        var cyborgs = new School() { Name = "School of cybornetics", TermProgressSpeed = 0.01f, LeedsTo = { ai } };

        var machines = new School() { Name = "Machineries", TermProgressSpeed = 0.01f, LeedsTo = { cyborgs, vehicles } };
        var water = new School() { Name = "Block: Water", TermProgressSpeed = 0.01f, LeedsTo = { machines } };

        var rocks = new School() { Name = "Geology and stone things", TermProgressSpeed = 0.01f };
        var buildings = new School() { Name = "Tall block development", TermProgressSpeed = 0.01f };
        var earth = new School() { Name = "Block: Earth", TermProgressSpeed = 0.01f, LeedsTo = { rocks, buildings } };

        var roundThings = new School() { Name = "School of round things", TermProgressSpeed = 0.01f, LeedsTo = { water, earth } };


        var computers = new School() { Name = "Silicon university", TermProgressSpeed = 0.01f };
        var electricity = new School() { Name = "Ark university", TermProgressSpeed = 0.01f, LeedsTo = { computers } };

        var arts = new School() { Name = "Art schoool", TermProgressSpeed = 0.01f };
        var fire = new School() { Name = "Block Fire", TermProgressSpeed = 0.01f, LeedsTo = { arts, electricity } };

        var philosphy = new School() { Name = "Thinking university", TermProgressSpeed = 0.01f };
        var wind = new School() { Name = "Block: Wind", TermProgressSpeed = 0.01f, LeedsTo = { philosphy } };

        var nonRoundThings = new School() { Name = "Nonround university", TermProgressSpeed = 0.01f, LeedsTo = { fire, wind } };

        currentSchools.Add(roundThings);
        currentSchools.Add(nonRoundThings);

        float startDegrees = 360.0f / currentSchools.Count;

        for(int a = 0; a < currentSchools.Count; ++a) {
            currentSchools[a].Rotation = Quaternion.Euler(0, 0, startDegrees * a);
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
            currentSchools[a].UpdatePosition(Quaternion.Euler(0, 0, startDegrees * a));
        }
    }
}
