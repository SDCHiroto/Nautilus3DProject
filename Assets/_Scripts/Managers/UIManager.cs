using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject MenuCanvas, SettingsCanvas, LoadingCanvas;
    [SerializeField] TextMeshProUGUI musicVal, sfxVal;
    [SerializeField] TMP_Dropdown resDropdown;
    [SerializeField] Resolution[] resolutions;

    private void Start()
    {
        DefineResolutions();
    }

    private void DefineResolutions()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
    }

    public void LoadScene(string sceneName){
        LoadingCanvas.SetActive(true);
        SoundManager.instance.Crossfade();
        SceneManager.LoadScene(sceneName);
    }

    public void Quit(){
        Debug.Log("Quit from game");
        Application.Quit();
    }

    public void OpenSettings(){
        Debug.Log("open settings");
        MenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    public void CloseSettings(){
        Debug.Log("close settings");
        SettingsCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void SetResolution(int resIndex){
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Debug.Log(isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float volume){
        musicVal.text = ConvertSliderValue(volume).ToString();
        SoundManager.instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume){
        sfxVal.text = ConvertSliderValue(volume).ToString();
        SoundManager.instance.SetSFXVolume(volume);
    }

    float ConvertSliderValue(float sliderValue)
    {
        // Mappa il valore dello slider da -50 a 0 su una scala da 0 a 100
        return Mathf.InverseLerp(-50f, 0f, sliderValue) * 100f;
    }

    public void OpenCredits(){
        Debug.Log("Apri crediti");
    }
    
    
}
