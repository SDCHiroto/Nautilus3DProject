using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] GameObject[] connectedObject;

    public void Interact(){
        foreach(GameObject obj in connectedObject)
        {
            if (obj.TryGetComponent(out IActivable objHitted)){
                objHitted.Activate();
            }
            
        }
    }
}
