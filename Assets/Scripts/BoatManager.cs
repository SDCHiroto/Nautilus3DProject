using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public static BoatManager instance;

    [Header("Ref")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] BoatControl boatControl;
    [SerializeField] GameObject player, boat;
    [SerializeField] MeshCollider boatMeshCollider;
    [SerializeField] CapsuleCollider playerCapsuleCollider;

     void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

    }
    
    private void Start() {
        player = GameObject.Find("Player");
        playerCapsuleCollider = GameObject.Find("PlayerMesh").GetComponent<CapsuleCollider>();
        playerMovement = player.GetComponent<PlayerMovement>();
        boat = GameObject.Find("Boat");
        boatControl = boat.GetComponent<BoatControl>();
        //boatMeshCollider = GameObject.Find("BoatMesh").GetComponent<MeshCollider>();
    }

    public void EnterBoat(){
            boatControl.enabled = true;
            playerMovement.enabled = false;
            playerCapsuleCollider.enabled = false;
            player.transform.position = boatControl.socketForPlayer.position;
            player.transform.parent = boatControl.socketForPlayer.gameObject.transform;
            playerMovement.rb.isKinematic = true;

            //boatMeshCollider.convex = true;
    }

    public void ExitBoat(){
            player.transform.parent = null;
            playerMovement.rb.isKinematic = false;
            boatControl.levelOfSpeedForward = 0;
            //boatMeshCollider.convex = false;
            boatControl.enabled = false;
            playerCapsuleCollider.enabled = true;
            playerMovement.enabled = true;
    }
}
