using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [Header("References")]
    [SerializeField] Laserbeam[] connectedObject;

    override public void Use(){
        foreach(Laserbeam laser in connectedObject)
        {
            laser.RotateLaserbeam();
        }
    }
}
