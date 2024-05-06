using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;
    
    public bool isPaused = false;

    // Array di telecamere delle varie stanze. L'indice dell' array definisce la stanza di appartenenza di quella specifica telecamera. 
    //Es: vCams[2] contiene la telecamera della stanza 2.
    public GameObject[] vCams;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableAllvCams(){
        foreach(GameObject vCam in vCams){
            vCam.SetActive(false);
        }
    }


    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            if(!isPaused){
                Pause();
            } else {
                RemovePause();
            }

        }    
    }


    void Pause(){
        SwitchManager.instance.DisableInputOfControlled();
        Time.timeScale = 0;
        isPaused = true;
    }

    void RemovePause(){
        SwitchManager.instance.EnableInputOfControlled();
        Time.timeScale = 1;     
        isPaused = false;
    }
}
