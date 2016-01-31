using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Scripts.Inventions;
using System.Collections.Generic;

public class ScienceReserachButton : MonoBehaviour {
    public Transform detailsText;
    public IInvention invention;
    bool toolTipActive = false;
    EventTrigger eventTrigger;

    // Use this for initialization
    void Start() {
        eventTrigger = GetComponent<EventTrigger>();
        RegisterOnMouseEnter();
        RegisterOnMouseExit();
        RegisterOnMouseClick();
    }

    public void RegisterOnMouseEnter() {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => {
            toolTipActive = true;

        });
        eventTrigger.triggers.Add(entry);
    }

    public void RegisterOnMouseExit() {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => {
            detailsText.GetComponent<Text>().text = "";
            toolTipActive = false;
        });
        eventTrigger.triggers.Add(entry);
    }
    public void RegisterOnMouseClick() {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => {
            if(Schools.TryMakeInvention(invention)) {
                Inventions.Research(invention);
                detailsText.GetComponent<Text>().text = "";
            }
        });
        eventTrigger.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update () {
        if(toolTipActive) {
            var details = invention.Name + "\n\n";
            details += "<size=8>";
            foreach(var cost in invention.Costs) {
                var color = Schools.MeetsRequirement(cost) ? "green" : "red";

                details += string.Format("<color={2}>{1} genius from {0}</color>\n", School.GetSchoolName(cost.Type), Util.NiceLongString(cost.Count), color);
            }
            details += "</size>";
            detailsText.GetComponent<Text>().text = details;
        }
    }
}
