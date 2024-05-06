using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Controllable
{

    protected override void OnAction(){
    }

    public void SetCheckpoint(Vector3 position){
        checkpoint = position;
    }

}
