using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject vcam;

    void OnTriggerEnter(Collider other) {
        Debug.Log("Aaaa!");
        if(other.CompareTag("Player")){
            vcam.SetActive(true);
        }
    }

        void OnTriggerExit(Collider other) {
                    Debug.Log("Aaaa!");
        if(other.CompareTag("Player")){
            vcam.SetActive(false);
        }
    }
}
