using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Controllable, IDamageable
{
    public bool canHide = false;
    public bool isHidden = false;

    protected override void OnAction(){
        if(canHide && !!isHidden){
            isHidden = true;
            
        } else if (canHide && isHidden){
            isHidden = false;

        }
    }

    public void SetCheckpoint(Vector3 position){
        checkpoint = position;
    }

    public void GetDamage(){
        SwitchManager.instance.ResetToPlayer();
        anim.SetTrigger("Death");
        Invoke("DelayedReloadScene", .3f);
        SwitchManager.instance.DisableInputOfControlled();        
    }

    private void DelayedReloadScene() {
        GeneralManager.instance.ReloadScene();
    }

}