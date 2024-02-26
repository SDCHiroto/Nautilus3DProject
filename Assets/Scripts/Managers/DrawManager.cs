using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public static DrawManager instance;

    [Header("Refs")]
    public Camera cam;
    public GameObject brushCollider;
    public GameObject brushPrefab;
    private GameObject brushInstance;

    [Header("Triggers")]
    [SerializeField] DrawTrigger[] trigger;

    LineRenderer currentLineRenderer;
    Vector3 lastPos;
    bool createdFirstBrush;
    public bool isClicked = false;
    public float offset;

    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        cam = Camera.main;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update() {
        Draw();
    }

    void AddAPoint(Vector3 pointPos){
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount-1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMouse(){
        Vector3 mousePos = GetMousePosition();
        if(lastPos != mousePos){
            AddAPoint(mousePos);
            lastPos = mousePos;
            GameObject brushCollider = IstantiateBrushCollider();
            brushCollider.transform.parent = brushInstance.transform;
        }
    }

    public void StartDrawing(){
        isClicked = true;
    }

    
    public void EndDrawing(){
        isClicked = false;
        Destroy(brushInstance);
        DeactivateAllTriggers();
    }  

    void Draw(){
        if(isClicked){
            if(!createdFirstBrush){
                CreateBrush();
                createdFirstBrush = true;
            }
            PointToMouse();
        } else {
            currentLineRenderer = null;
            createdFirstBrush = false;
        }

        
    }

    void CreateBrush(){
        brushInstance = Instantiate(brushPrefab);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        Vector3 mousePos = GetMousePosition();
        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
    }

    void DeactivateAllTriggers(){
        foreach(DrawTrigger drawTrigger in trigger){
            drawTrigger.isActive = false;
        }
    }

    GameObject IstantiateBrushCollider(){
        return Instantiate(brushCollider, lastPos, Quaternion.identity);
    }

    Vector3 GetMousePosition(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * offset;
    }
}