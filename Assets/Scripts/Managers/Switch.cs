using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public static Switch instance;
    [Header("Switch Character")]
    [SerializeField] Movement playerRef;
    [SerializeField] public Movement controlledChara;
    [SerializeField] float rangeOfSwitch; 
    [SerializeField] LayerMask whatIsSwitchable;
    [SerializeField] Movement characterSwitchable;
    
    private bool canSwap = false;
    private bool controlling = false;

    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        playerRef = GameObject.Find("Player").GetComponent<Movement>();
        controlledChara = playerRef;
        ActivateCorrectMovementScript();

    }

    private void FixedUpdate() {
        CheckForSwitchCharacter();
    }

    public void SwitchCharacter(){
        if(canSwap && !controlling){
            controlledChara = characterSwitchable;
            controlling = true;
            playerRef.gameObject.SetActive(false);
            playerRef.transform.parent = controlledChara.transform;
            ActivateCorrectMovementScript();
        } else if(controlling){
            controlling = false;
            playerRef.transform.parent = null;
            playerRef.transform.position = controlledChara.exitPoint.position;
            controlledChara = playerRef;    
            playerRef.gameObject.SetActive(true);
            ActivateCorrectMovementScript();
        }
    }

    void CheckForSwitchCharacter(){
        Collider[] temp = Physics.OverlapSphere(controlledChara.transform.position, rangeOfSwitch, whatIsSwitchable);
        if(temp.Length > 0){
            characterSwitchable = temp[0].GetComponent<Movement>();
            canSwap = true;
        } else {
            characterSwitchable = null;
            canSwap = false;
        }
    }

    public void ActivateCorrectMovementScript(){
        Movement[] allControllable = GameObject.FindObjectsByType<Movement>(FindObjectsSortMode.None);
        foreach(Movement controllable in allControllable){
            controllable.enabled = false;
            controllable.gameObject.layer = LayerMask.NameToLayer("Controllable");
        }   

        controlledChara.gameObject.layer = LayerMask.NameToLayer("Controlled");
        controlledChara.enabled = true;
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(controlledChara.transform.position, rangeOfSwitch);
    }
}
