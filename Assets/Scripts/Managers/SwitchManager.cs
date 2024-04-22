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
            currentIndex = -1;
            ActivateCorrectMovementScript();
        
    }

    private void FindNearestControllableCharacter()
    {   
        RaycastHit hit;

        characters = GameObject.FindGameObjectsWithTag("Character");
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
                    visible.Remove(characters[i]);
                }
            }
        }
    }

    int currentIndex = -1;
    public void SwitchCharacter(){
        if(visible.Count > 0){
            controlled.GetComponent<Controllable>().enabled = false;
            currentIndex++;
            if(currentIndex >= visible.Count){
                currentIndex = -1;
                controlled = player.gameObject;
                playerIsInControl = true;
            } else {
                controlled = visible[currentIndex];      
                playerIsInControl = false;          
            }

            ActivateCorrectMovementScript();
            // controlled.GetComponent<Movement>().enabled = true;
        }
    }

    public void ActivateCorrectMovementScript(){
        Controllable[] allControllable = GameObject.FindObjectsByType<Controllable>(FindObjectsSortMode.None);
        foreach(Controllable controllable in allControllable){
            controllable.enabled = false;
        }

        controlled.GetComponent<Controllable>().enabled = true;
    }


}

