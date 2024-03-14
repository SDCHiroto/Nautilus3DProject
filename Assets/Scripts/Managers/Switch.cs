using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public static Switch instance;
    [Header("Switch Character")]
    [SerializeField] Movement playerRef;
    [SerializeField] Movement controlledChara;
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

    public void SwitchCharacter(){
        if(canSwap){
            controlledChara = characterSwitchable;
            ActivateCorrectMovementScript();
        }
    }

    private void FixedUpdate() {
        CheckForSwitchCharacter();
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

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(controlledChara.transform.position, rangeOfSwitch);
    }
}
