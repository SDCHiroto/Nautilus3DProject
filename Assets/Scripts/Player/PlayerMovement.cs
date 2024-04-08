using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    [SerializeField] float cooldownDuration = 1.0f; // Durata del cooldown in secondi
    private bool canAttack = true; // Flag per indicare se è possibile attaccare
    
    protected override void OnAction(){
        if(canAttack){
            anim.SetTrigger("Slash1");
            canAttack = false;
            Invoke("ResetAttack", cooldownDuration);
        }

    }

    private void ResetAttack()
    {
        // Riattiva la possibilità di attaccare
        canAttack = true;
    }
}
