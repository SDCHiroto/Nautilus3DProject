using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmStatue : Movement
{
    [Header("Interaction Info")]
    [SerializeField] float radiusOfInteraction;
    [SerializeField] LayerMask whatIsInteractable;
    [SerializeField] Interactable interactableObj;
    [SerializeField] MeshRenderer meshRenderer;

    [Header("Materials")]
    [SerializeField] Material off;
    [SerializeField] Material on;

    private bool canInteract = false;

    new private void OnEnable() {
        base.OnEnable();
        StartControlling();
    }

    new private void OnDisable(){
        base.OnDisable();
        EndControlling();
    }

    private void Start() {
        meshRenderer.material = off;
    }

    new void FixedUpdate() {
       base.FixedUpdate(); 
       CheckForInteractions();
    }

    void StartControlling(){
        meshRenderer.material = on;
    }

    void EndControlling(){
        meshRenderer.material = off;
    }

    void CheckForInteractions(){
        Collider[] temp = Physics.OverlapSphere(this.transform.position, radiusOfInteraction, whatIsInteractable);
        if(temp.Length > 0){
            interactableObj = temp[0].GetComponent<Interactable>();
            canInteract = true;
        } else {
            interactableObj = null;
            canInteract = false;
        }
    }

    new void OnAction(){
        if(canInteract){
            Debug.Log(" Debug di ArmStatue");
            interactableObj.Use();
        }
    }

    new void OnDrawGizmos(){
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(this.transform.position, radiusOfInteraction);
    }
}
