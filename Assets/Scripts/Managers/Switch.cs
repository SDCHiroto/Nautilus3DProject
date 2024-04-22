using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public static Switch instance;
    [Header("Switch Character")]
    [SerializeField] Controllable playerRef;
    [SerializeField] public Controllable controlledChara;
    [SerializeField] float rangeOfSwitch; 
    [SerializeField] LayerMask whatIsSwitchable;
    [SerializeField] Controllable characterSwitchable;
    
    private bool canSwap = false;
    private bool controlling = false;

    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        playerRef = GameObject.Find("Player").GetComponent<Controllable>();
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
            ActivateCorrectMovementScript();
        } else if(controlling){
            controlling = false;
            controlledChara = playerRef;    
            ActivateCorrectMovementScript();
        }
    }

    void CheckForSwitchCharacter(){
        Collider[] temp = Physics.OverlapSphere(controlledChara.transform.position, rangeOfSwitch, whatIsSwitchable);
        if(temp.Length > 0){
            characterSwitchable = temp[0].GetComponent<Controllable>();
            canSwap = true;
        } else {
            characterSwitchable = null;
            canSwap = false;
        }
    }

    public void ActivateCorrectMovementScript(){
        Controllable[] allControllable = GameObject.FindObjectsByType<Controllable>(FindObjectsSortMode.None);
        foreach(Controllable controllable in allControllable){
            controllable.enabled = false;
            controllable.gameObject.layer = LayerMask.NameToLayer("Controllable");
            controllable.gameObject.tag = "Character";
        }   

        controlledChara.gameObject.tag = "Player";
        controlledChara.gameObject.layer = LayerMask.NameToLayer("Controlled");
        controlledChara.enabled = true;
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(controlledChara.transform.position, rangeOfSwitch);
    }
}
