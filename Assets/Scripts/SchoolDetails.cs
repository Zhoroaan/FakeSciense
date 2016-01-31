using UnityEngine;
using UnityEngine.UI;

public class SchoolDetails : MonoBehaviour {
    public School school { get; set; }
    Text headerText;
    Text detailsText;
	// Use this for initialization
	void Start () {
        headerText = transform.GetChild(1).GetComponent<Text>();
        detailsText = transform.GetChild(3).GetComponent<Text>();
        detailsText.supportRichText = true;
    }

	// Update is called once per frame
	void Update () {
        if(school != null) {
            headerText.text = school.Name;
            var details = "Number of students in class: " + school.NumberOfGStudentsInClass + "\n"
             + "Number of geniuses in pool: " + school.NumberOfGeniusesInPool + "\n";

            if(school.LeedsTo.Count != 0) {
                details += "\n";
                foreach(var requirement in school.Requirements) {
                    var currentResource = BuildResource.GetResource(requirement.ResourceType);
                    details += currentResource.Name + ": ";
                    var color = currentResource.Count >= requirement.Count ? "green" : "red";

                    details += string.Format("<color={0}>{1}</color>\n", color, requirement.Count);
                }
            }

            detailsText.text = details;
        }
	}

    void Close() {
        gameObject.SetActive(false);
        Console.ShowConsole = true;
    }
}
