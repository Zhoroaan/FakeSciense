using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Console : MonoBehaviour {
    GUIStyle textStyle = new GUIStyle();
    List<string> textLines = new List<string>();
    public int LineHeight = 12;

    float scrollAnimationTime = 0f;
    float scrollTime = 0.3f;

	// Use this for initialization
	void Start () {
        var color = new Color(0.074f, 0.047f, 0.03f);
        textStyle.normal.textColor = color;
        textStyle.hover.textColor = color;
        textStyle.richText = true;
        AddText("Start Game");
    }

    // Update is called once per frame
    void Update () {

	}

    void OnGUI() {
        var xPos = Screen.width / 2 + 20;
        int yPos = 20;
        int boxWidth = Screen.width / 2 - 40;
        int boxHeight = Screen.height / 2 - 40;
        GUI.color = Color.white;
        GUI.BeginGroup(new Rect(xPos, yPos, boxWidth, boxHeight + 5));
        DrawConsoleLines(xPos, boxHeight - LineHeight);
        GUI.EndGroup();
    }

    private void DrawConsoleLines(int xPos, int yPos) {
        scrollAnimationTime = Mathf.Max(scrollAnimationTime - Time.deltaTime, 0);

        int elementCount = textLines.Count;
        // Start from the back so we can stop when lines cannot be seen any longer
        for(int a = elementCount - 1; a >= 0; --a) {
            var renderPositionY = yPos + LineHeight * (a - elementCount + 1) + (scrollAnimationTime / scrollTime) * LineHeight;
            if(renderPositionY + LineHeight * 2 < 0) {
                break;
            }
            GUI.Label(new Rect(0, renderPositionY, 100, 30), textLines[a], textStyle);
        }
    }

    public void AddText(string newLine) {
        AddConsoleText(newLine, null);
    }

    void AddConsoleText(string newLine, Color? colorToUse) {
        if(!colorToUse.HasValue) {
            textLines.Add(newLine);
        } else {
            var colorText = GetHexColor(colorToUse);
            textLines.Add("<color=#" +colorText  + ">" + newLine + "</color>");
        }
        scrollAnimationTime += scrollTime;
    }

    private string GetHexColor(Color? colorToUse) {
        float r = colorToUse.Value.r;
        float g = colorToUse.Value.g;
        float b = colorToUse.Value.b;
        float a = colorToUse.Value.a;
        return String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", ComponentAsByte(r), ComponentAsByte(g), ComponentAsByte(b), ComponentAsByte(a));
    }

    private object ComponentAsByte(float colorComponent) {
        return (byte)(colorComponent * 255);
    }
}
