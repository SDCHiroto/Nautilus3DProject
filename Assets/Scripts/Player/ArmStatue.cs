using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmStatue : Controllable
{
    [Header("Interaction Info")]
    [SerializeField] float radiusOfInteraction; // Raggio per la ricerca di oggetti interattivi
    [SerializeField] LayerMask whatIsInteractable; // Maschera per definire cosa è considerato interagibile
    [SerializeField] Interactable interactableObj; // Oggetto interattivo rilevato
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer; // Riferimento al renderer della mesh

    [Header("Materials")]
    [SerializeField] Material off; // Materiale quando il controllo è disabilitato
    [SerializeField] Material on; // Materiale quando il controllo è abilitato

    private bool canInteract = false; // Flag che indica se è possibile interagire con un oggetto

    new private void OnEnable() {
        base.OnEnable(); // Chiamata al metodo OnEnable della classe base (Controllable.cs)
        StartControlling(); // Inizia il controllo
    }

    new private void OnDisable(){
        base.OnDisable(); // Chiamata al metodo OnDisable della classe base (Controllable.cs)
        EndControlling(); // Termina il controllo
    }

    private void Start() {

    }

    new void FixedUpdate() {
       base.FixedUpdate(); // Chiamata al metodo FixedUpdate della classe base (Controllable.cs)
       CheckForInteractions(); // Controlla se ci sono oggetti interattivi nel raggio
    }

    void StartControlling(){
        skinnedMeshRenderer.material = on; // Imposta il materiale della mesh quando il controllo inizia
    }

    void EndControlling(){
        skinnedMeshRenderer.material = off; // Imposta il materiale della mesh quando il controllo termina
    }

    void CheckForInteractions(){
        // Cerca gli oggetti interattivi nel raggio specificato
        Collider[] temp = Physics.OverlapSphere(this.transform.position, radiusOfInteraction, whatIsInteractable);
        if(temp.Length > 0){
            interactableObj = temp[0].GetComponent<Interactable>(); // Se viene trovato un oggetto interattivo, lo memorizza
            canInteract = true; // Imposta il flag di interazione a true
        } else {
            interactableObj = null; // Se non viene trovato alcun oggetto interattivo, azzera l'oggetto interattivo
            canInteract = false; // Imposta il flag di interazione a false
        }
    }

    protected override void OnAction(){
        if(canInteract){ // Se è possibile interagire con un oggetto...
            interactableObj.Use(); // ...utilizza l'oggetto interattivo
        }
    }

    new void OnDrawGizmos(){
        Gizmos.DrawWireSphere(this.transform.position, radiusOfInteraction); // Disegna una sfera di gizmo per rappresentare il raggio di interazione
    }
}
