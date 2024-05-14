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

    // Riferimento al canvas di pausa
    public GameObject pauseCanvas;

    // Flag che indica se il gioco è in pausa
    public bool isPaused = false;

    // Scala del tempo per il controllo della velocità di gioco
    public float timeScale = 1;

    // Array di telecamere virtuali per le diverse stanze
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

    private void Update()
    {
        Time.timeScale = timeScale;
    }


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Disabilita tutte le telecamere virtuali
    public void DisableAllvCams()
    {
        foreach(GameObject vCam in vCams)
        {
            vCam.SetActive(false);
        }
    }

    // Zoom di una specifica telecamera
    public void ZoomCamera(float val)
    {
        vCams[SwitchManager.instance.controlled.GetComponent<Controllable>().currentRoom]
            .GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = val;
    }
    
    // Reimposta lo zoom di una specifica telecamera
    public void ResetZoomCamera()
    {
        vCams[SwitchManager.instance.controlled.GetComponent<Controllable>().currentRoom]
            .GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 10;
    }

    // Reimposta la zoom della telecamera quando il personaggio è respawnato
    public void RespawnReset()
    {
        ResetZoomCamera();
        SwitchManager.instance.EnableInputOfControlled();
    }

    // Ricarica la scena corrente
    public void ReloadScene()
    {
        ResetZoomCamera();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // Esegue l'animazione di dissolvenza in entrata
    public void FadeInAnimation()
    {
        ZoomCamera(5);
        anim.SetTrigger("FadeIn");
    }

    // Gestisce l'evento di pausa
    public void PausePressed()
    {
        if(!isPaused)
            Pause();
        else
            RemovePause();
    }

    // Metodo chiamato quando è attivato l'input di pausa
    void OnPause()
    {
        PausePressed(); 
    }

    // Mette in pausa il gioco
    public void Pause()
    {
        SwitchManager.instance.DisableInputOfControlled();
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        pI.enabled = true;
    }

    // Rimuove la pausa dal gioco
    public void RemovePause()
    {
        pI.enabled = false;
        pauseCanvas.SetActive(false);
        SwitchManager.instance.EnableInputOfControlled();
        Time.timeScale = 1;     
        isPaused = false;
    }
}
