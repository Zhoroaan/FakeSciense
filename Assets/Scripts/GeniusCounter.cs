using UnityEngine;
using UnityEngine.UI;

public class GeniusCounter : MonoBehaviour {
    public School SchoolReference { get; set; }
    private Text textComponent;

    // Use this for initialization
    void Start () {
        textComponent = GetComponent<Text>();

    }

	// Update is called once per frame
	void Update () {
        textComponent.text = SchoolReference.NumberOfGeniusesInPool.ToString();
    }
}
