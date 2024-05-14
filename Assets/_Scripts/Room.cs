using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int id_room;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] Transform[] respawnPoint; 
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Character"))
        {
            GeneralManager.instance.DisableAllvCams();
            cam.gameObject.SetActive(true);
            other.gameObject.GetComponent<Controllable>().currentRoom = id_room;
        }
    }

    private void Update() {
        cam.Follow = SwitchManager.instance.controlled.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Character"))
        {
            cam.gameObject.SetActive(false);
        }
    }
}
