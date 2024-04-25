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
    [SerializeField] Transform player;
    [SerializeField] Material outlineMat;

    bool playerIsInControl = true;
 
    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        player = GameObject.Find("Player").transform;
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

    void ResetToPlayer(){
            controlled.GetComponent<Controllable>().enabled = false;
            controlled = player.gameObject;
            playerIsInControl = true;
            ActivateCorrectMovementScript();
        
    }

    GameObject nearestCharacter;

    private void FindNearestControllableCharacter()
    {   
        RaycastHit hit;
        for(int i = 0; i < characters.Length; i++){
            Vector3 direction = characters[i].transform.position - player.transform.position;
            Physics.Raycast(player.transform.position, direction, out hit, 999);
            if(hit.collider.gameObject == characters[i]){
                Debug.DrawLine(player.transform.position, hit.point, Color.white);
                if(!visible.Contains(characters[i])) {
                    visible.Add(characters[i]);                    
                }
            } else {
                if(visible.Contains(characters[i])) {
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

    public void SwitchCharacter(){
        if(visible.Count > 0 && playerIsInControl){
            player.GetComponent<Controllable>().enabled = false;
            controlled = nearestCharacter;
            playerIsInControl = false;
        } else {
            controlled.GetComponent<Controllable>().enabled = false;
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

