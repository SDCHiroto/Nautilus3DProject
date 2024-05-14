using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Controllable, IDamageable
{

    // Variabile che indica se il personaggio è attualmente nascosto
    public bool isHidden = false;

    protected override void OnAction(){
    }

    // Imposta il checkpoint del personaggio principale
    public void SetCheckpoint(Vector3 position){
        checkpoint = position;
    }

    // Metodo per gestire il danno inflitto al personaggio principale
    public void GetDamage(){
        // Resetta al personaggio principale dopo il danno
        SwitchManager.instance.ResetToPlayer();
        // Attiva l'animazione di morte
        anim.SetTrigger("Death");
        // Richiama la ricarica della scena dopo un breve ritardo
        Invoke("DelayedReloadScene", .3f);
        // Disabilita l'input del personaggio controllato
        SwitchManager.instance.DisableInputOfControlled();        
    }

    // Metodo per ritardare la ricarica della scena dopo la morte del personaggio principale
    private void DelayedReloadScene() {
        GeneralManager.instance.ReloadScene();
    }

}
