using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    // Pannelli UI
    [SerializeField] GameObject MenuCanvas, SettingsCanvas, LoadingCanvas, controlsCanvas;

    // Testo per i valori dei volumi
    [SerializeField] TextMeshProUGUI musicVal, sfxVal;

    // Dropdown per la risoluzione
    [SerializeField] TMP_Dropdown resDropdown;

    // Array di risoluzioni disponibili
    [SerializeField] Resolution[] resolutions;

    // Pulsanti per il focus iniziale nei vari menu
    [SerializeField] GameObject menuFirstButton, settingsFirstButton, controlsFirstButton;

    private void Start()
    {
        DefineResolutions(); // Definisce le opzioni di risoluzione disponibili
        EventSystem.current.SetSelectedGameObject(menuFirstButton); // Imposta il focus iniziale sul primo pulsante del menu principale
    }

    // Definisce le opzioni di risoluzione disponibili nel dropdown
    private void DefineResolutions()
    {
        resolutions = Screen.resolutions; // Ottiene tutte le risoluzioni disponibili
        resDropdown.ClearOptions(); // Cancella le opzioni attuali nel dropdown
        List<string> options = new List<string>(); // Lista delle opzioni
        int currentResolutionIndex = 0; // Indice della risoluzione attuale

        // Ciclo per creare le opzioni del dropdown
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height; // Formatta la risoluzione come "larghezza x altezza"
            options.Add(option); // Aggiunge l'opzione alla lista

            // Controlla se la risoluzione è quella attualmente in uso e aggiorna l'indice
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Aggiunge le opzioni al dropdown, imposta l'opzione attuale e aggiorna il dropdown
        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
    }

    // Carica una nuova scena
    public void LoadScene(string sceneName){
        LoadingCanvas.SetActive(true); // Attiva il pannello di caricamento
        SoundManager.instance.Crossfade(); // Avvia la transizione audio
        SceneManager.LoadScene(sceneName); // Carica la scena
    }

    // Esce dal gioco
    public void Quit(){
        Debug.Log("Quit from game");
        Application.Quit(); // Chiude l'applicazione
    }

    // Apre il pannello delle impostazioni
    public void OpenSettings(){
        EventSystem.current.SetSelectedGameObject(null); // Deseleziona l'elemento attualmente selezionato
        EventSystem.current.SetSelectedGameObject(settingsFirstButton); // Imposta il focus sul primo pulsante del pannello delle impostazioni
        MenuCanvas.SetActive(false); // Disattiva il pannello del menu principale
        SettingsCanvas.SetActive(true); // Attiva il pannello delle impostazioni
    }

    // Chiude il pannello delle impostazioni
    public void CloseSettings(){
        EventSystem.current.SetSelectedGameObject(null); // Deseleziona l'elemento attualmente selezionato
        EventSystem.current.SetSelectedGameObject(menuFirstButton); // Imposta il focus sul primo pulsante del menu principale
        SettingsCanvas.SetActive(false); // Disattiva il pannello delle impostazioni
        MenuCanvas.SetActive(true); // Attiva il pannello del menu principale
    }

    // Imposta la risoluzione dello schermo
    public void SetResolution(int resIndex){
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Imposta la modalità schermo intero
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; // Imposta la modalità schermo intero
    }

    // Imposta il volume della musica
    public void SetVolume(float volume){
        musicVal.text = ConvertSliderValue(volume).ToString(); // Aggiorna il testo del volume della musica
        SoundManager.instance.SetMusicVolume(volume); // Imposta il volume della musica tramite il SoundManager
    }

    // Imposta il volume degli effetti sonori
    public void SetSFXVolume(float volume){
        sfxVal.text = ConvertSliderValue(volume).ToString(); // Aggiorna il testo del volume degli effetti sonori
        SoundManager.instance.SetSFXVolume(volume); // Imposta il volume degli effetti sonori tramite il SoundManager
    }

    // Converte il valore dello slider da -50 a 0 su una scala da 0 a 100
    float ConvertSliderValue(float sliderValue)
    {
        return Mathf.InverseLerp(-50f, 0f, sliderValue) * 100f;
    }

    // Apre il pannello dei controlli
    public void OpenControls(){
        EventSystem.current.SetSelectedGameObject(null); // Deseleziona l'elemento attualmente selezionato
        EventSystem.current.SetSelectedGameObject(controlsFirstButton); // Imposta il focus sul primo pulsante del pannello dei controlli
        MenuCanvas.SetActive(false); // Disattiva il pannello del menu principale
        controlsCanvas.SetActive(true); // Attiva il pannello dei controlli
    }

    // Chiude il pannello dei controlli
    public void CloseControls(){
        EventSystem.current.SetSelectedGameObject(null); // Deseleziona l'elemento attualmente selezionato
        EventSystem.current.SetSelectedGameObject(menuFirstButton); // Imposta il focus sul primo pulsante del menu principale
        controlsCanvas.SetActive(false); // Disattiva il pannello dei controlli
        MenuCanvas.SetActive(true); // Attiva il pannello del menu principale
    }    
}
