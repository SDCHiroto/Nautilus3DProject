using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] connectedObject; // Array di oggetti collegati al pulsante
    [SerializeField] Animator anim; // Animator per l'animazione del pulsante
    AudioSource audioSource; // Sorgente audio per il suono del pulsante
    public bool isActive = false; // Indica se il pulsante è attivo o meno

    private void Awake() {
        anim = GetComponent<Animator>(); // Ottiene l'Animator del pulsante
        audioSource = GetComponent<AudioSource>(); // Ottiene la sorgente audio del pulsante
    }

    // Attiva il pulsante
    public void Activate(){
        if(!isActive){ // Controlla se il pulsante non è già attivo
            isActive = true; // Imposta il pulsante come attivo
            _PlayButtonClip(); // Riproduce il suono del pulsante
            foreach(GameObject obj in connectedObject) // Cicla attraverso gli oggetti collegati
            {
                if (obj.TryGetComponent(out Gate gate)){ // Controlla se l'oggetto è una porta e la attiva
                    gate.Activate();
                    anim.SetTrigger("Activate"); // Avvia l'animazione di attivazione del pulsante
                }
            }
        }       
    }

    // Disattiva il pulsante
    public void Deactivate(){
        isActive = false; // Imposta il pulsante come non attivo
        foreach(GameObject obj in connectedObject) // Cicla attraverso gli oggetti collegati
        {
            if (obj.TryGetComponent(out Gate gate)){ // Controlla se l'oggetto è una porta e la disattiva
                gate.Activate();
                anim.SetTrigger("Deactivate"); // Avvia l'animazione di disattivazione del pulsante
            }
        }
    }

    // Riproduce il suono del pulsante
    public void _PlayButtonClip(){
        audioSource.Play();
    }
}
