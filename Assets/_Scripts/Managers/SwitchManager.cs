using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager instance;

    [Header("Objects")]
    public GameObject[] characters;
    public List<GameObject> visible;
    public GameObject controlled;

    [Header("Refs")]
    [SerializeField] Controllable player;
    [SerializeField] Material outlineMat;

    GameObject nearestCharacter;
    [SerializeField] private LayerMask whatIsHittable;

    public bool playerIsInControl = true;
 
    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        player = GameObject.Find("Player").GetComponent<Controllable>();
        controlled = player.gameObject;
        characters = GameObject.FindGameObjectsWithTag("Character");
        ActivateCorrectMovementScript();
    }

    void Update(){
        FindNearestControllableCharacter(); 

        if(!visible.Contains(controlled) && !playerIsInControl){
            ResetToPlayer();
        }
    }

    public void ResetToPlayer(){
            controlled.GetComponent<Controllable>().enabled = false;
            controlled.GetComponent<Controllable>().SetisControllingAnimation(false);
            controlled.layer = LayerMask.NameToLayer("Controllable");
            controlled = player.gameObject;
            player.SetisControllingAnimation(false);
            playerIsInControl = true;
            ActivateCorrectMovementScript();
        
    }

    private void FindNearestControllableCharacter()
    {   
        RaycastHit hit;
        for(int i = 0; i < characters.Length; i++){
            Vector3 direction = characters[i].transform.position - player.transform.position;
            Physics.Raycast(player.transform.position, direction, out hit, 999, whatIsHittable);
            if(hit.collider.gameObject == characters[i] && (player.currentRoom == characters[i].GetComponent<Controllable>().currentRoom)){
                Debug.DrawLine(player.transform.position, hit.point, Color.white);
                if(!visible.Contains(characters[i])) {
                    visible.Add(characters[i]);                    
                }
            } else {
                if(visible.Contains(characters[i]) ) {
                    characters[i].GetComponent<Controllable>().RemoveOutline();
                    visible.Remove(characters[i]);
                }
            }
        }

        if(playerIsInControl){
            float min_dist = Mathf.Infinity;
            foreach(GameObject golem in visible){
                float temp_dist = Vector3.Distance(player.transform.position, golem.transform.position);
                if( temp_dist < min_dist ){
                    min_dist = temp_dist;
                    nearestCharacter = golem;
                }
            }

            foreach(GameObject golem in visible){
                golem.GetComponent<Controllable>().RemoveOutline();
            }

            if(visible.Contains(nearestCharacter)) {
                nearestCharacter.GetComponent<Controllable>().AddOutline();
            }
        }

           
    }

    public void DisableInputOfControlled(){
        controlled.GetComponent<Controllable>().playerInput.enabled = false;
    }

    public void EnableInputOfControlled(){
        controlled.GetComponent<Controllable>().playerInput.enabled = true;
    }

    public void SwitchCharacter(){
        if(visible.Count > 0 && playerIsInControl){
            player.SetisControllingAnimation(true);
            player.enabled = false;
            controlled = nearestCharacter;
            controlled.layer = LayerMask.NameToLayer("Controlled");
            controlled.GetComponent<Controllable>().SetisControllingAnimation(true);
            playerIsInControl = false;
        } else {
            player.SetisControllingAnimation(false);
            controlled.GetComponent<Controllable>().SetisControllingAnimation(false);
            controlled.GetComponent<Controllable>().enabled = false;
            controlled.layer = LayerMask.NameToLayer("Controllable");
            controlled = player.gameObject;
            playerIsInControl = true;
        }

        ActivateCorrectMovementScript();

    }

    public void ActivateCorrectMovementScript(){
        Controllable[] allControllable = GameObject.FindObjectsByType<Controllable>(FindObjectsSortMode.None);
        foreach(Controllable controllable in allControllable){
            controllable.enabled = false;
        }

        controlled.GetComponent<Controllable>().enabled = true;
    }


}

