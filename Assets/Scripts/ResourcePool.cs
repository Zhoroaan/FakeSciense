using UnityEngine;
using System;
using UnityEngine.UI;

public class ResourcePool : MonoBehaviour {
    public ResourceType resourceType;
    Int64 peopleInPool;
    Text countText;
    Button decreasePool;
    Button increasePool;
    Image progressImage;
    double resourcesInPool = 0;
    // Use this for initialization
    void Start () {
        progressImage = GetComponent<Image>();
        countText = transform.GetChild(2).GetComponent<Text>();
        decreasePool = transform.GetChild(3).GetComponent<Button>();
        increasePool = transform.GetChild(4).GetComponent<Button>();
        decreasePool.onClick.AddListener(DecreaseCount);
        increasePool.onClick.AddListener(IncreaseCount);
    }

    void DecreaseCount() {
        if(peopleInPool != 0) {
            var requestedPeople = BuildResource.CountWorkers / 10;
            var moveCount = Math.Min(peopleInPool, requestedPeople);
            peopleInPool -= moveCount;
            BuildResource.FreeWorkers += moveCount;
        }
    }

    void IncreaseCount() {
        var requestedPeople = BuildResource.CountWorkers / 10;
        var moveCount = Math.Min(BuildResource.FreeWorkers, requestedPeople);
        peopleInPool += moveCount;
        BuildResource.FreeWorkers -= moveCount;
    }

    // Update is called once per frame
    void Update () {
        countText.text = peopleInPool.ToString();
        if(peopleInPool != 0) {
            progressImage.fillAmount += Time.deltaTime * 0.3f;
            if(progressImage.fillAmount >= 1) {
                progressImage.fillAmount = 0;
                BuildResource.GetResource(resourceType).Count += (Int64)resourcesInPool;
                resourcesInPool = 0;
            } else {
                resourcesInPool += (double)Time.deltaTime * peopleInPool * 0.01;
            }
        }
    }
}
