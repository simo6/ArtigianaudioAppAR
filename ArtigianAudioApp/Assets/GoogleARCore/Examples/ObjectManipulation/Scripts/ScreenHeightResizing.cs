using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHeightResizing : MonoBehaviour {

    public RectTransform PanelList;
    public RectTransform PanelOptions;
    private float WindowAspect;
    private float marginTop = 100;

    // Use this for initialization
    void Start () {

        // determine the game window's current aspect ratio
        WindowAspect = (float)Screen.width / (float)Screen.height;
        float height;

        if (WindowAspect > 1)
        {
            //Landscape View
            height = 300;            
        }
        else
        {
            //Vertical View
            height = 300;
        }

        PanelList.sizeDelta = new Vector2(PanelList.sizeDelta.x, height);
        PanelList.offsetMax = new Vector2(0, -marginTop);


        PanelOptions.sizeDelta = new Vector2(PanelList.sizeDelta.x, height);
        PanelOptions.offsetMax = new Vector2(0, -marginTop);

    }
	
	// Update is called once per frame
	void Update () {
        // determine the game window's current aspect ratio
        WindowAspect = (float)Screen.width / (float)Screen.height;
        float height;

        if (WindowAspect > 1)
        {
            //Landscape View
            height = 300;
        }
        else
        {
            //Vertical View
            height = 600;
        }

        PanelList.sizeDelta = new Vector2(PanelList.sizeDelta.x, height);
        PanelList.offsetMax = new Vector2(0, -marginTop);

        PanelOptions.sizeDelta = new Vector2(PanelList.sizeDelta.x, height);
        PanelOptions.offsetMax = new Vector2(0, -marginTop);
    }
}
