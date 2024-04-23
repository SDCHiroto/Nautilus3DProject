using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] IInteractable[] connectedObject;

    public void Interact(){
        foreach(IInteractable obj in connectedObject)
        {
            obj.Interact();
        }
    }
}
