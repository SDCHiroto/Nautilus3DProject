using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [Header("Canvas")]
    [SerializeField] GameObject drawCanvas;
    [SerializeField] public TextMeshProUGUI InteractionText;

    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        drawCanvas = GameObject.FindGameObjectWithTag("DrawCanvas");
        InteractionText = GameObject.Find("InteractionText").GetComponent<TextMeshProUGUI>();
        InteractionText.text = "";
        CloseDrawCanvas();
    }


    public void CloseDrawCanvas(){
        drawCanvas.SetActive(false);
        LockMouse();
    }

    public void OpenDrawCanvas(){
        drawCanvas.SetActive(true);
        FreeMouse();
    }

    public void LockMouse(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FreeMouse(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }



}
