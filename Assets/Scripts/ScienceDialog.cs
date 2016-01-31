using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Inventions;
using System.Collections.Generic;

public class ScienceDialog : MonoBehaviour {
    public Transform researchButtonPrefab;
    public Transform detailsText;
    List<IInvention> lastInventions = new List<IInvention>();
    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {
        var currentInventions = Inventions.InventionList;
        if(currentInventions.Count != lastInventions.Count) {
            lastInventions.Clear();
            lastInventions.AddRange(currentInventions);
            var container = transform.GetChild(4);
            foreach(Transform child in container) {
                Destroy(child.gameObject);
            }
            int elementsAddedCounter = 0;
            for(int a = 0; a < currentInventions.Count; ++a) {
                if(Schools.CanSeeInvention(currentInventions[a])) {
                    var newTransform = (Transform)Instantiate(researchButtonPrefab, transform.position, transform.rotation);
                    newTransform.SetParent(container);
                    var rectTransform = newTransform.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(elementsAddedCounter * 15, -10);
                    var newButton = newTransform.GetComponent<ScienceReserachButton>();
                    newButton.detailsText = detailsText;
                    newButton.invention = currentInventions[a];
                    elementsAddedCounter++;
                }

            }
        }
    }

    public void Close() {
        gameObject.SetActive(false);
        Console.ShowConsole = true;
    }

    public void Reset() {

    }
}
