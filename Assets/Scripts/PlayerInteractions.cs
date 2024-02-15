using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private Transform cam;

    [SerializeField] private float maxDistance;
    public GameObject grabbedObject;
    bool holdingObject = false;

    private void Start() {
        cam = Camera.main.transform;    
    }

    public void Interact(){
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance) && !holdingObject)   { //Se premo E e non ho niente in mano
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);
            if(hit.collider.gameObject.GetComponent<Grabbable>()){ // Se quello che ho davanti è un "Grabbable" lo raccolgo
                holdingObject = true;
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<Grabbable>().Grab();
            }
        } 
        else if(holdingObject && grabbedObject) { // Se premo E ed ho qualcosa in mano lo rilascio;
            holdingObject = false;
            grabbedObject.GetComponent<Grabbable>().Release();
            grabbedObject = null;
        }
    }
}
