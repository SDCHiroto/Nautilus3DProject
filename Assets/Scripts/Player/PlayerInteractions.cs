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

    private void Update() {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance) && !holdingObject){
            if(hit.collider.gameObject.GetComponent<Grabbable>()){ // Se quello che ho davanti è un "Grabbable" lo raccolgo
                MenuManager.instance.InteractionText.text = hit.collider.gameObject.GetComponent<Grabbable>().interactionDescription;
            } 
            else if(hit.collider.gameObject.GetComponent<Interactable>()){ // Se quello che ho davanti è un "Interactable" lo uso
                MenuManager.instance.InteractionText.text = hit.collider.gameObject.GetComponent<Interactable>().interactionDescription;
            } else {
                MenuManager.instance.InteractionText.text = "";
            }
        } 
        else {
            MenuManager.instance.InteractionText.text = "";
        }
    }

    public void Interact(){
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance) && !holdingObject)   { //Se premo E e non ho niente in mano
            if(hit.collider.gameObject.GetComponent<Grabbable>()){ // Se quello che ho davanti è un "Grabbable" lo raccolgo
                holdingObject = true;
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<Grabbable>().Grab();
            } 
            else if(hit.collider.gameObject.GetComponent<Interactable>()){ // Se quello che ho davanti è un "Interactable" lo uso
                GameObject interactableObject = hit.collider.gameObject;
                interactableObject.GetComponent<Interactable>().Use();
            }   
        } 
        else if(holdingObject && grabbedObject) { // Se premo E ed HO qualcosa in mano lo rilascio;
            holdingObject = false;
            grabbedObject.GetComponent<Grabbable>().Release();
            grabbedObject = null;
        }
    }
}
