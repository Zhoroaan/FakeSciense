using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceCount : MonoBehaviour {
    public ResourceType resourceType;
    public bool showTotalCount = false;
    Text textElement;
    CanvasRenderer headerRenderer;
    CanvasRenderer countRenderer;
    // Use this for initialization
    void Start () {
        textElement = GetComponent<Text>();
        headerRenderer = transform.parent.GetChild(0).GetComponent<CanvasRenderer>();
        countRenderer = GetComponent<CanvasRenderer>();
    }

	// Update is called once per frame
	void Update () {
        var count = showTotalCount ? BuildResource.FreeWorkers : BuildResource.GetResource(resourceType).Count;
        textElement.text = count.ToString();
        // Hide resource text if hidden
        var alpha = count != 0 ? 1 : 0;
        countRenderer.SetAlpha(alpha);
        headerRenderer.SetAlpha(alpha);
    }
}
