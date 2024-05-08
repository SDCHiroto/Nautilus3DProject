using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;

    public Animator anim;
    
    public bool isPaused = false;
    public float timeScale = 1;


    // Array di telecamere delle varie stanze. L'indice dell' array definisce la stanza di appartenenza di quella specifica telecamera. 
    //Es: vCams[2] contiene la telecamera della stanza 2.
    public GameObject[] vCams;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        anim = GetComponent<Animator>();
    }   

    private void Update() {
        Time.timeScale = timeScale;
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

    public void ZoomCamera(float val)
    {
        vCams[SwitchManager.instance.controlled.GetComponent<Controllable>().currentRoom].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = val;
    }
    
    public void ResetZoomCamera()
    {
        vCams[SwitchManager.instance.controlled.GetComponent<Controllable>().currentRoom].GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 10;
    }

    public void RespawnReset(){
        ResetZoomCamera();
        SwitchManager.instance.EnableInputOfControlled();
    }

    public void ReloadScene(){
        ResetZoomCamera();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FadeInAnimation(){
        ZoomCamera(5);
        anim.SetTrigger("FadeIn");
    }

    void OnPause(){
        if(!isPaused){
            Pause();
        } else {
            RemovePause();
        }
    }

    public void Pause(){
        SwitchManager.instance.DisableInputOfControlled();
        Time.timeScale = 0;
        isPaused = true;
    }

    public void RemovePause(){
        SwitchManager.instance.EnableInputOfControlled();
        Time.timeScale = 1;     
        isPaused = false;
    }
}
