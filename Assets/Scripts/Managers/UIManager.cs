using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject MenuCanvas, SettingsCanvas;
    [SerializeField] TMP_Dropdown resDropdown;
    [SerializeField] Resolution[] resolutions;

    private void Start() {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
    }

    public void LoadScene(string sceneName){
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
        Debug.Log(volume);
    }

    public void OpenCredits(){
        Debug.Log("Apri crediti");
    }
    
    
}
