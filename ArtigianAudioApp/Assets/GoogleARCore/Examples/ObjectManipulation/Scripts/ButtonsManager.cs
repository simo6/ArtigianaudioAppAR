using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour {

    //Panel Main Buttons
    public GameObject PanelButtons;
    public GameObject PanelList;
    public GameObject PanelOptions;

    //Capture attribute
    private bool Capturing;
    private bool ManageCapturing;
    private string ScreenShotPath;

    //Show surfaces attributes
    private bool ShowSurfaces;
    public Toggle ToggleButton;
    public GameObject DetectedPlaneVizualizer;
    public Material MaterialOn;
    public Material MaterialOff;

    //Panel Titles
    public GameObject PanelBasesTitle;
    public GameObject PanelModelsTitle;
    public GameObject PanelMaterialsTitle;

    //Panel Contents
    public GameObject PanelBasesContent;
    public GameObject PanelMaterialsContent;

    //Panel Bases
    public GameObject PanelBaseNormal;
    public GameObject PanelBaseLong;
    public GameObject PanelBaseLarge;

    //Panel Settings
    public GameObject PanelSettingsTitle;
    public GameObject PanelSettingsContent;
    public GameObject PanelLanguagesTitle;
    public GameObject PanelLanguagesContent;

    public static string Base;
    public static int Model;
    public static string Material;

    public static List<string> MaterialsAvailable = new List<string>() {"Laccato Nero","Legno Massello","Vetro"};

    // Use this for initialization
    void Start () {
        Base = "Base Normal";
        Model = 0;
        Material = "Laccato Nero";

        Capturing = false;
        ManageCapturing = false;

        ShowSurfaces = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (Capturing)
        {
            //Frame has to be captured
            StartCoroutine(Screenshot.Instance.TakeAndSave());
            ManageCapturing = true;
            Capturing = false;

        }
        else if (ManageCapturing && !Capturing)
        {
 
            if (Screenshot.Instance.IsSaved())
            {
                //Move the file into a custom folder
                Screenshot.Instance.Move();

                //Frame captured
                PanelButtons.SetActive(true);
                if (ToggleButton.isOn) ShowHideSurfaces();
                ManageCapturing = false;

            }

            //

            

            // This is the path of my folder.
            /*string OriginPath = System.IO.Path.Combine(Application.persistentDataPath, ScreenShotPath);
            string Path = "/Utenti/Simone/Desktop/" + ScreenShotPath;
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                //Store in the PC
                Debug.Log("[Device] PC");
                Path = "C:/Users/Simone/Desktop/" + ScreenShotPath;
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                if (SystemInfo.operatingSystem.Contains("Android"))
                {
                    //Store in an Android device
                    Debug.Log("[Device] Android");
                    Path = "../../../../DCIM/" + ScreenShotPath;
                }
                else
                {
                    //Store in an iOS device
                    Debug.Log("[Device] iOS");
                    //Path = "/DCIM/ArtigianAudio/" + ScreenShotPath;
                }
            }
            
            if (File.Exists(OriginPath))
            {
                Debug.Log("New Path: " + Path);
                Debug.Log("Found - Exists");
                SSTools.ShowMessage("Found", SSTools.Position.bottom, SSTools.Time.twoSecond);
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }
                File.Move(OriginPath, Path);
                
            }
            else
            {
                SSTools.ShowMessage("Not Found", SSTools.Position.bottom, SSTools.Time.twoSecond);
                Debug.Log("Not Found - Not Exist");
            }
            */
        }
            
        
    }
    

    public void OpenClosePanelList()
    {
        Debug.Log(PanelList.activeSelf);
        if (PanelList.activeSelf)
        {
            PanelList.SetActive(false);
        }
        else
        {
            PanelOptions.SetActive(false);
            PanelList.SetActive(true);
        }
    }

    public void OpenClosePanelOptions()
    {
        if (PanelOptions.activeSelf)
        {
            PanelOptions.SetActive(false);
        }
        else
        {
            PanelList.SetActive(false);
            PanelOptions.SetActive(true);
        }
    }

    public void OpenPanelMaterials(int model)
    {
        ClosePanelModels();
        PanelBasesTitle.SetActive(false);
        PanelModelsTitle.SetActive(false);
        PanelMaterialsTitle.SetActive(true);
        PanelBasesContent.SetActive(false);
        PanelMaterialsContent.SetActive(true);

        Model = model;

        return;
    }

    public void OpenPanelLanguages()
    {
        PanelSettingsTitle.SetActive(false);
        PanelSettingsContent.SetActive(false);
        PanelLanguagesTitle.SetActive(true);
        PanelLanguagesContent.SetActive(true);

        return;
    }

    public void BackToPanelSettings()
    {
        PanelSettingsTitle.SetActive(true);
        PanelSettingsContent.SetActive(true);
        PanelLanguagesTitle.SetActive(false);
        PanelLanguagesContent.SetActive(false);
    }

    public void BackToPanelModels()
    {
        OpenPanelModels(Base);
        return;
    }

    public void OpenPanelModels(string baseName)
    {
        PanelBasesTitle.SetActive(false);
        PanelModelsTitle.SetActive(true);
        PanelMaterialsTitle.SetActive(false);
        PanelBasesContent.SetActive(false);
        PanelMaterialsContent.SetActive(false);
        switch (baseName)
        {
            case "Base Normal":        
                PanelBaseNormal.SetActive(true);
                break;
            case "Base Long":
                PanelBaseLong.SetActive(true);
                break;
            case "Base Large":
                PanelBaseLarge.SetActive(true);
                break;
        }
        Base = baseName;
        return;
    }
    public void OpenPanelBases()
    {
        ClosePanelModels();
        PanelBasesTitle.SetActive(true);
        PanelModelsTitle.SetActive(false);
        PanelMaterialsTitle.SetActive(false);
        PanelBasesContent.SetActive(true);
        PanelMaterialsContent.SetActive(false);

        return;
    }

    private void ClosePanelModels()
    {
        PanelBaseNormal.SetActive(false);
        PanelBaseLong.SetActive(false);
        PanelBaseLarge.SetActive(false);
        return;
    }

    public void SelectMaterial(string material)
    {
        ClosePanelModels();
        PanelBasesTitle.SetActive(true);
        PanelModelsTitle.SetActive(false);
        PanelMaterialsTitle.SetActive(false);
        PanelBasesContent.SetActive(true);
        PanelMaterialsContent.SetActive(false);

        OpenClosePanelList();

        Material = material;

        return;
    }

    public void Capture()
    {
        Debug.Log(Application.persistentDataPath);

        PanelButtons.SetActive(false);
        PanelList.SetActive(false);
        PanelOptions.SetActive(false);
        if(ToggleButton.isOn) ShowHideSurfaces();
        Screenshot.Instance.Path = "ArtigianAudio_" + System.DateTime.Now.ToString("yyyy-MM-dd-H-mm-ss") + ".jpg";
        Capturing = true;

        //ScreenShotPath = "Capture_" + System.DateTime.Now.ToString("yyyy-MM-dd-H-mm-ss") + ".jpg";
        //ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + ScreenShotPath);

    }

    public void ShowHideSurfaces()
    {
        if (ShowSurfaces)
        {
            DetectedPlaneVizualizer.GetComponent<Renderer>().material = MaterialOff;
            ShowSurfaces = false;
            foreach (MeshRenderer d in GameObject.Find("Plane Generator").GetComponentsInChildren<MeshRenderer>())
            {
                d.material = MaterialOff;
            }
        }
        else
        {
            DetectedPlaneVizualizer.GetComponent<Renderer>().material = MaterialOn;
            ShowSurfaces = true;
            foreach (MeshRenderer d in GameObject.Find("Plane Generator").GetComponentsInChildren<MeshRenderer>())
            {
                d.material = MaterialOn;
            }
        }
    }

}
