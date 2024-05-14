using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;

    public Animator anim;
    public PlayerInput pI;
    
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
        pI = GetComponent<PlayerInput>();

    }   

    private void Update() {
        Time.timeScale = timeScale;

        Debug.Log(this.GetComponent<PlayerInput>().currentControlScheme);
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

    public void PausePressed(){
        if(!isPaused){
            Pause();
        } else {
            RemovePause();
        }
        
    }

    void OnPause(){
        PausePressed(); 
    }

    public void Pause(){
        SwitchManager.instance.DisableInputOfControlled();
        Time.timeScale = 0;
        isPaused = true;
        pI.enabled = true;
    }

    public void RemovePause(){
        pI.enabled = false;
        SwitchManager.instance.EnableInputOfControlled();
        Time.timeScale = 1;     
        isPaused = false;

    }
}
