using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Controllable, IDamageable
{

    protected override void OnAction(){
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