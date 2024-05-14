using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] GameObject[] connectedObject; // Oggetti collegati alla leva
    [SerializeField] Animator anim; // Animator della leva per l'animazione
    AudioSource audioSource; // Sorgente audio per il suono della leva

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Ottiene il riferimento all'Animator della leva
        audioSource = GetComponent<AudioSource>(); // Ottiene il riferimento all'AudioSource della leva
    }

    // Funzione chiamata quando il giocatore interagisce con la leva
    public void Interact()
    {
        foreach (GameObject obj in connectedObject)
        {
            // Se l'oggetto collegato implementa l'interfaccia IActivable, lo attiva
            if (obj.TryGetComponent(out IActivable objHitted))
            {
                objHitted.Activate(); // Attiva l'oggetto collegato
                anim.SetTrigger("Activate"); // Avvia l'animazione della leva
            }
        }
    }

    // Funzione per riprodurre il suono della leva
    public void _PlayLeverClip()
    {
        audioSource.Play(); // Riproduce il suono della leva
    }
}
