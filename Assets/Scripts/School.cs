using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class School {
    public class UpgradeRequirement {
        public UpgradeRequirement(ResourceType resourceType, Int64 count) {
            ResourceType = resourceType;
            Count = count;
        }
        public ResourceType ResourceType { get; set; }
        public Int64 Count { get; set; }
    }

    public enum Type {
        dimensional,
        colonization,
        space,
        vehicles,
        ai,
        cyborgs,
        machines,
        water,
        rocks,
        buildings,
        earth,
        roundThings,
        computers,
        electricity,
        arts,
        fire,
        philosphy,
        wind,
        nonRoundThings,
        _Count
    }

    static String[] schoolNames = new string[(int)Type._Count];

    static public void SetSchoolName(Type type, string name) {
        schoolNames[(int)type] = name;
    }

    static public String GetSchoolName(Type type) {
        return schoolNames[(int)type];
    }

    public class SchoolTypeInformation {
        public Type SchoolType { get; set; }
        public string Name { get; set; }
    }

    public School(Type type, string name) {
        SchoolType = new SchoolTypeInformation() { SchoolType = type, Name = name };
        SetSchoolName(type, name);
        Name = name;
        Qualifications = new List<SchoolTypeInformation>();
        LeedsTo = new List<School>();
        Requirements = new List<UpgradeRequirement>();
        NumberOfGStudentsInClass = GetNumberOfStudentsToAdd();
        Qualifications.Add(SchoolType);
    }

    public string Name { get; set; }
    public List<School> LeedsTo {
        get; set;
    }

    public List<SchoolTypeInformation> Qualifications {
        get;
        set;
    }

    public List<UpgradeRequirement> Requirements {
        get; set;
    }

    public SchoolTypeInformation SchoolType {
        get; set;
    }

    public float CurrentTermProgress { get; set; }
    public GameObject SchoolReference { get; set; }
    public Int64 NumberOfGeniusesInPool { get; set; }
    public Int64 NumberOfGeniusesCurrentlySelected { get; set; }
    public Int64 NumberOfGStudentsInClass { get; set; }
    public float TermProgressSpeed { get; set; }
    public Quaternion Rotation { get; set; }
    public Quaternion TargetRotation { get; set; }

    Image progressImageControll;

    RectTransform schoolButtons;

    public void Spawn(Transform prefab, Transform transform) {
        var pos = prefab.transform.position;
        var rot = prefab.transform.rotation;
        var gameObject = ((Transform)Transform.Instantiate(prefab, pos, rot)).gameObject;
        gameObject.transform.SetParent(transform, false);
        schoolButtons = gameObject.GetComponent<RectTransform>();
        gameObject.GetComponentInChildren<Text>().text = Name;
        SchoolReference = gameObject;
        progressImageControll = gameObject.GetComponent<Image>();
        gameObject.GetComponentInChildren<UpgradeButton>().SchoolReference = this;
        gameObject.GetComponentInChildren<GeniusCounter>().SchoolReference = this;
        gameObject.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(SchoolClicked);
        TargetRotation = Rotation;
        SetPositionFromRotation();
    }

    public bool CanUpgrade() {
        if(LeedsTo.Count == 0) {
            return false;
        }

        foreach(var requirement in Requirements) {
            var currentResource = BuildResource.GetResource(requirement.ResourceType);
            if(requirement.Count > currentResource.Count) {
                return false;
            }
        }

        return true;
    }

    public void SchoolClicked() {
        SchoolReference.transform.root.BroadcastMessage("OpenSchoolWindow", this);
    }

    public void UpdatePosition(Quaternion newTarget) {
        TargetRotation = newTarget;
    }

    public void Update() {
        CurrentTermProgress += TermProgressSpeed * Time.deltaTime;
        if(CurrentTermProgress > 1) {
            NewClass();
        }

        progressImageControll.fillAmount = CurrentTermProgress;
        if(Rotation != TargetRotation) {
            Rotation = Quaternion.RotateTowards(Rotation, TargetRotation, Time.deltaTime * 180.0f);
            SetPositionFromRotation();
        }
    }

    void SetPositionFromRotation() {
        schoolButtons.anchoredPosition = (Rotation * Vector2.up * 85.0f);
        schoolButtons.anchoredPosition += Vector2.down * 20.0f;
    }

    private void NewClass() {
        CurrentTermProgress = 0;
        var numberOfGeniuses = (Int64)(NumberOfGStudentsInClass * 0.01);
        SchoolReference.transform.root.BroadcastMessage("AddText", Util.NiceLongString(numberOfGeniuses) + " geniuses graduated from " + Name + ".");
        var numberOfWorkers = NumberOfGStudentsInClass - numberOfGeniuses;
        SchoolReference.transform.root.BroadcastMessage("AddText", Util.NiceLongString(numberOfWorkers) + " moved to resource gathering.");
        BuildResource.FreeWorkers += numberOfWorkers;
        NumberOfGeniusesInPool += numberOfGeniuses;
        NumberOfGStudentsInClass = GetNumberOfStudentsToAdd();
    }

    Int64 GetNumberOfStudentsToAdd() {
        return (Int64)(BuildResource.CountWorkers * 0.01);
    }
}