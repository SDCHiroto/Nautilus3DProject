using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager instance;

    [Header("Oggetti")]
    public GameObject[] characters; // Array contenente tutti i personaggi controllabili nel gioco
    public List<GameObject> visible; // Lista dei personaggi visibili al giocatore
    public GameObject controlled; // Il personaggio attualmente controllato dal giocatore

    [Header("Riferimenti")]
    [SerializeField] Controllable player; // Riferimento al componente Controllable del giocatore
    [SerializeField] Material outlineMat; // Materiale per l'outline dei personaggi controllabili

    GameObject nearestCharacter; // Il personaggio controllabile più vicino al giocatore
    [SerializeField] private LayerMask whatIsHittable; // Maschera dei layer per il raycast

    public bool playerIsInControl = true; // Indica se il giocatore è al momento in controllo del personaggio

    // Inizializzazione
    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        // Trova il componente Controllable del giocatore
        player = GameObject.Find("Player").GetComponent<Controllable>();
        controlled = player.gameObject; // Imposta il giocatore come personaggio controllato all'avvio
        characters = GameObject.FindGameObjectsWithTag("Character"); // Trova tutti i personaggi controllabili nel gioco
        ActivateCorrectMovementScript(); // Attiva lo script di movimento corretto per il personaggio controllato
    }

    void Update(){
        FindNearestControllableCharacter(); // Trova il personaggio controllabile più vicino al giocatore

        // Se il personaggio controllato non è più visibile al giocatore e non si trova in controllo del giocatore, reimposta il controllo al giocatore
        if(!visible.Contains(controlled) && !playerIsInControl){
            ResetToPlayer();
        }
    }

    // Reimposta il controllo al giocatore
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

        // Trova il personaggio controllabile più vicino al giocatore
        if(playerIsInControl){
            float min_dist = Mathf.Infinity;
            foreach(GameObject golem in visible){
                float temp_dist = Vector3.Distance(player.transform.position, golem.transform.position);
                if( temp_dist < min_dist ){
                    min_dist = temp_dist;
                    nearestCharacter = golem;
                }
            }

            // Rimuove l'outline da tutti i personaggi visibili
            foreach(GameObject golem in visible){
                golem.GetComponent<Controllable>().RemoveOutline();
            }

            // Aggiunge l'outline al personaggio più vicino
            if(visible.Contains(nearestCharacter)) {
                nearestCharacter.GetComponent<Controllable>().AddOutline();
            }
        }  
    }

    // Disabilita l'input per il personaggio controllato
    public void DisableInputOfControlled(){
        controlled.GetComponent<Controllable>().playerInput.enabled = false;
    }

    // Abilita l'input per il personaggio controllato
    public void EnableInputOfControlled(){
        controlled.GetComponent<Controllable>().playerInput.enabled = true;
    }

    // Cambia il personaggio controllato
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

    // Attiva lo script di movimento corretto per il personaggio controllato
    public void ActivateCorrectMovementScript(){
        Controllable[] allControllable = GameObject.FindObjectsByType<Controllable>(FindObjectsSortMode.None);
        foreach(Controllable controllable in allControllable){
            controllable.enabled = false;
        }

        controlled.GetComponent<Controllable>().enabled = true;
    }
}
