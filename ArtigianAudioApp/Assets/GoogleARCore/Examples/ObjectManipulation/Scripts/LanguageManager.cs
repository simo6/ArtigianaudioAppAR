using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour {

    public TextMeshProUGUI StringSettingsTitle;
    public TextMeshProUGUI StringChangeLanguage;
    public TextMeshProUGUI StringSelectLanguage;
    public TextMeshProUGUI StringShowDetectedSurfaces;
    public TextMeshProUGUI StringItalian;
    public TextMeshProUGUI StringEnglish;
    public TextMeshProUGUI StringSpanish;

    public TextMeshProUGUI StringMenu;
    public TextMeshProUGUI StringUndo;
    public TextMeshProUGUI StringCapture;
    public TextMeshProUGUI StringRedo;
    public TextMeshProUGUI StringSettings;

    public TextMeshProUGUI StringSelectBase;
    public TextMeshProUGUI StringSelectModel;
    public TextMeshProUGUI StringSelectMaterial;

    public TextMeshProUGUI StringLaquared;
    public TextMeshProUGUI StringSolidWood;
    public TextMeshProUGUI StringGlass;

    public Image LanguageImage;

    public static string StringCaptureSaved;
    public static string StringCaptureFailed;
    public static string StringBaseSelected;
    public static string StringBaseNotFound;

    // Use this for initialization
    void Start () {
        StringCaptureSaved = "Capture saved";
        StringCaptureFailed = "Capture failed";
        StringBaseSelected = "Selected";
        StringBaseNotFound = "Base not found!";
}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeLanguage(string lan)
    {
        Sprite s = null;
        switch (lan)
        {
            case "IT":
                StringSettingsTitle.text = "Impostazioni";
                StringChangeLanguage.text = "Cambia lingua";
                StringSelectLanguage.text = "Seleziona una lingua";
                StringShowDetectedSurfaces.text = "Mostra le superfici rilevate";
                StringItalian.text = "Italiano";
                StringEnglish.text = "Inglese";
                StringSpanish.text = "Spagnolo";

                StringMenu.text = "Menu";
                StringUndo.text = "Annulla";
                StringCapture.text = "Foto";
                StringRedo.text = "Ricrea";
                StringSettings.text = "Impostazioni";

                StringSelectBase.text = "Seleziona una Base";
                StringSelectModel.text = "Seleziona un Modello";
                StringSelectMaterial.text = "Seleziona un Materiale";

                StringLaquared.text = "Laccato";
                StringSolidWood.text = "Legno massello";
                StringGlass.text = "Vetro";

                StringCaptureSaved = "Foto salvata";
                StringCaptureFailed = "Errore di salvataggio";
                StringBaseSelected = "Selezionato";
                StringBaseNotFound = "Base non disponibile!";

                s = Resources.Load<Sprite>("Images/IT");

                break;

            case "EN":
                StringSettingsTitle.text = "Settings";
                StringChangeLanguage.text = "Change language";
                StringSelectLanguage.text = "Select a language";
                StringShowDetectedSurfaces.text = "Show detected surfaces";
                StringItalian.text = "Italian";
                StringEnglish.text = "English";
                StringSpanish.text = "Spanish";

                StringMenu.text = "Menu";
                StringUndo.text = "Undo";
                StringCapture.text = "Capture";
                StringRedo.text = "Redo";
                StringSettings.text = "Settings";

                StringSelectBase.text = "Select a Base";
                StringSelectModel.text = "Select a Model";
                StringSelectMaterial.text = "Selet a Material";

                StringLaquared.text = "Lacquered";
                StringSolidWood.text = "Solid wood";
                StringGlass.text = "Glass";

                StringCaptureSaved = "Capture saved";
                StringCaptureFailed = "Capture failed";
                StringBaseSelected = "Selected";
                StringBaseNotFound = "Base not found!";

                s = Resources.Load<Sprite>("Images/EN");

                break;
            case "ES":

                StringSettingsTitle.text = "Ajustes";
                StringChangeLanguage.text = "Cambiar idioma";
                StringSelectLanguage.text = "Seleccionar un idioma";
                StringShowDetectedSurfaces.text = "Muestra superficies detectadas";
                StringItalian.text = "Italiano";
                StringEnglish.text = "Ingles";
                StringSpanish.text = "Espanol";

                StringMenu.text = "Menu";
                StringUndo.text = "Cancelar";
                StringCapture.text = "Captura";
                StringRedo.text = "Rehacer";
                StringSettings.text = "Ajustes";

                StringSelectBase.text = "Seleccionar una Base";
                StringSelectModel.text = "Seleccionar un Modelo";
                StringSelectMaterial.text = "Seleccionar un Material";

                StringLaquared.text = "Lacado";
                StringSolidWood.text = "Madera maciza";
                StringGlass.text = "Vaso";

                StringCaptureSaved = "Captura guardada";
                StringCaptureFailed = "Captura fallida";
                StringBaseSelected = "Seleccionado";
                StringBaseNotFound = "Base no disponible!";

                s = Resources.Load<Sprite>("Images/ES");

                break;
            default:

                break;

        }
        LanguageImage.sprite = s;
        return;
    }
}
