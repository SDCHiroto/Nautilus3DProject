using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject vcam;
    [SerializeField] Transform[] respawnPoint; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vcam.SetActive(true);
        }
    }

    // void FindNearestRespawnPoint()
    // {
    //     float minDist = 999f;
    //     for(int i = 0; i < respawnPoint.Length; i++)
    //     {
    //         float dist = Vector3.Distance(respawnPoint[i].transform.position, transform.position);

    //         if(dist < minDist)
    //         {
    //             minDist = dist;
    //             SpawnManager.instance.SetCurrentSpawnPoint(respawnPoint[i].position);
    //         }
    //     }
    // }



    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vcam.SetActive(false);
        }
    }
}
