using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Vector3 currentSpawnPoint;


    public static SpawnManager instance;
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        //Bisogna resettare pure tutte le statue/trappole

        Switch.instance.controlledChara.transform.position = currentSpawnPoint;
    }

    public void SetCurrentSpawnPoint(Vector3 position)
    {
        currentSpawnPoint = position;
    }




}
