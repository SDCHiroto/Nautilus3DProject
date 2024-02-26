using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_DrawManager : MonoBehaviour
{
    public LR_DrawManager instance;

    [Header("Refs")]
    public Camera cam;
    public GameObject brushPrefab;
    GameObject brushInstance;

    [Header("Refs")]
    [SerializeField] DrawTrigger[] trigger;


    public float timerDefault;
    public float timer;


    Vector3 lastPos;
    public bool isClicked = false;
    public float offset;

    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        cam = Camera.main;
    }

    private void FixedUpdate() {
        timer -= Time.fixedDeltaTime;

        if(timer <= 0){
            Draw();
            timer = timerDefault;
        }

    }

    public void StartDrawing(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        isClicked = true;
    }

    public void EndDrawing(){
        isClicked = false;
       DestroyPaintedBrush();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }   

    void DestroyPaintedBrush(){
        GameObject[] brushesIstantiated = GameObject.FindGameObjectsWithTag("Brush");
        foreach(GameObject brush in brushesIstantiated){
            Destroy(brush);
        }
    }

    void Draw(){
        if(isClicked){
            CreateBrush();
        }
    }

    void CreateBrush(){
        Vector3 mousePos = GetMousePosition();
        if(mousePos != lastPos) {
            lastPos = mousePos;
            brushInstance = Instantiate(brushPrefab, mousePos, Quaternion.identity);     
            brushInstance.transform.parent = this.transform;       
        }


    }

    Vector3 GetMousePosition(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * offset;
    }
}
