using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {
    public School SchoolReference { get; set; }
    private Button ButtonControll;

    public void Start() {
        ButtonControll = GetComponent<Button>();
    }

    public void Click() {
        SendMessageUpwards("UpdateSchool", SchoolReference.Name);
    }

    bool IsHidden() {
        return !SchoolReference.CanUpgrade();
    }

    public void Update() {
        if(IsHidden() == ButtonControll.enabled) {
            if(IsHidden()) {
                ButtonControll.enabled = false;
                ButtonControll.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
            } else {
                ButtonControll.enabled = true;
                ButtonControll.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
            }
        }
    }
}
