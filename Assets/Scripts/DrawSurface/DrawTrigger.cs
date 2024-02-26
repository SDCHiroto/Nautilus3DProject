using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrigger : MonoBehaviour
{
    public bool isActive = false;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Brush"){
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Brush"){
            isActive = false;
        }
    }
}
