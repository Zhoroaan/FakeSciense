using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Schools : MonoBehaviour {
    class School {
        public string Name { get; set; }
        public List<School> LeedsTo {
            get; set;
        }
        public float CurrentTermProgress { get; set; }
        public GameObject SchoolReference { get; set; }
        public Int64 NumberOfGeniusesInPool { get; set; }
        public Int64 NumberOfGeniusesCurrentlySelected { get; set; }
        public Int64 NumberOfGStudentsInClass { get; set; }
        public float TermProgressSpeed { get; set; }
        public Quaternion Rotation { get; set; }

        Image progressImageControll;

        public void Spawn(Transform prefab, Transform transform) {
            var pos = prefab.transform.position;
            var rot = prefab.transform.rotation;
            var gameObject = ((Transform)Instantiate(prefab, pos, rot)).gameObject;
            gameObject.transform.SetParent(transform, false);
            var rectTrans = gameObject.GetComponent<RectTransform>();
            rectTrans.anchoredPosition = (Rotation * Vector2.up * 85.0f);
            rectTrans.anchoredPosition += Vector2.down * 20.0f;
            gameObject.GetComponentInChildren<Text>().text = Name;
            SchoolReference = gameObject;
            progressImageControll = gameObject.GetComponent<Image>();
        }

        public void Update() {
            CurrentTermProgress += TermProgressSpeed * Time.deltaTime;
            if(CurrentTermProgress > 1) {
                NewClass();
            }
            progressImageControll.fillAmount = CurrentTermProgress;
        }

        private void NewClass() {
            CurrentTermProgress = 0;
            NumberOfGeniusesInPool += (Int64)(NumberOfGStudentsInClass * 0.01);
            NumberOfGStudentsInClass = 1000;
        }
    }

    public RectTransform SchoolPrefab;
    List<School> currentSchools = new List<School>();

	// Use this for initialization
	void Start () {
        var dimensional = new School() { Name = "School of dimensional things", TermProgressSpeed = 0.3f };
        var colonization = new School() { Name = "Colonization ministry", TermProgressSpeed = 0.1f };
        currentSchools.Add(dimensional);
        currentSchools.Add(colonization);

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
}
