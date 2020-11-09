using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Screenshot : MonoBehaviour
{

    private static Screenshot sInstance = new Screenshot();

    public static Screenshot Instance
    {
        get
        {
            return sInstance;
        }
    }


    Texture2D tex;
    private WaitForEndOfFrame FrameEnd = new WaitForEndOfFrame();
    public string Path { get; set; }

    public string GetPath()
    {
        // where we want to save the image
        return Application.persistentDataPath + "/" + Path;
    }


    public IEnumerator TakeNow()
    {
        yield return FrameEnd;
        //takes the screenshot, but doesn't save a file. It's stored as a Texture2D instead
        tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();
    }

    public IEnumerator TakeAndSave()
    {
        yield return TakeNow();
        SaveAsFile();
    }

    public Texture2D GetTexture2D()
    {
        //call to access the texture2D from outside this class
        return tex;
    }

    public void SaveAsFile()
    {
        //saves a PNG file to the path specified above
        byte[] bytes = tex.EncodeToJPG();
        File.WriteAllBytes(GetPath(), bytes);
    }

    public void Move()
    {

        if (File.Exists(GetPath()))
        {
            
            NativeGallery.SaveImageToGallery(GetPath(), "Camera", Path, null);
            File.Delete(GetPath());
            SSTools.ShowMessage(LanguageManager.StringCaptureSaved, SSTools.Position.bottom, SSTools.Time.twoSecond);

        }
        else
        {
            SSTools.ShowMessage(LanguageManager.StringCaptureFailed, SSTools.Position.bottom, SSTools.Time.twoSecond);
            Debug.Log("Not Found - Not Exist");
        }
        

        return;
    }

    public bool IsSaved()
    {
        //it's not enough to just check that the file exists, since it doesn't mean it's finished saving
        //we have to check if it can actually be opened
        Texture2D image;
        image = new Texture2D(Screen.width, Screen.height);
        if (File.Exists(GetPath()))
        {
            bool imageLoadSuccess = image.LoadImage(File.ReadAllBytes(GetPath()));
            Destroy(image);
            return imageLoadSuccess;
        }
        else
        {
            return false;
        } 
    }

}
