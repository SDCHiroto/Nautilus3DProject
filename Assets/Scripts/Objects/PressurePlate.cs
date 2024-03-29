using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Interactable connectedObject;

    public bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            isActive = true;
            connectedObject.Use();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
        {
            isActive = false;
            connectedObject.Use();
        }
    }



}
