using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Scripts.Inventions;
using System.Collections.Generic;

public class ScienceReserachButton : MonoBehaviour {
    public Transform detailsText;
    public IInvention invention;

    EventTrigger eventTrigger;

    // Use this for initialization
    void Start () {
        eventTrigger = GetComponent<EventTrigger>();
        RegisterOnMouseEnter();
        RegisterOnMouseExit();
        RegisterOnMouseClick();
    }

    public void SetDetailsText() {
        detailsText.GetComponent<Text>().text = invention.Name;
    }

    public void RegisterOnMouseEnter() {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => {
            SetDetailsText();

        });
        eventTrigger.triggers.Add(entry);
    }

    public void RegisterOnMouseExit() {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => {
            detailsText.GetComponent<Text>().text = "";
        });
        eventTrigger.triggers.Add(entry);
    }
    public void RegisterOnMouseClick() {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => {
            Inventions.Research(invention);
        });
        eventTrigger.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update () {

    }
}
