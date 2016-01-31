using UnityEngine;
using System.Collections;

public class OpenDialogs : MonoBehaviour {
    public Transform schoolsDetailWindow;
	// Use this for initialization
	void Start () {

	}

    public void OpenSchoolWindow(School school) {
        Console.ShowConsole = false;
        var window = schoolsDetailWindow.GetComponent<SchoolDetails>();
        window.school = school;
        window.gameObject.SetActive(true);
    }
}
