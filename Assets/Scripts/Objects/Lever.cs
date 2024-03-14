using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [Header("References")]
    [SerializeField] Interactable connectedObject;

    override public void Use(){
        Debug.Log("Debug di Lever ");
        connectedObject.Use();
    }
}
