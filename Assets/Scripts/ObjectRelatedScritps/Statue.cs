using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : Interactable
{   
    float currentDegrees = 0;
    
    override public void Use(){
        currentDegrees += 45;
        transform.rotation = Quaternion.Euler(0, currentDegrees, 0);
    }
}
