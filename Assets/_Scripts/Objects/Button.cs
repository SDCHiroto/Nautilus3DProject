using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] connectedObject;
    [SerializeField] Animator anim;
    AudioSource audioSource;
    public bool isActive = false;

    private void Awake() {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Activate(){
        if(!isActive){
            isActive = true;
            _PlayButtonClip();
            foreach(GameObject obj in connectedObject)
            {
                if (obj.TryGetComponent(out Gate gate)){
                    gate.Activate();
                    anim.SetTrigger("Activate");
                }
            }

        }       
    }

    public void Deactivate(){
        isActive = false;
            foreach(GameObject obj in connectedObject)
            {
                if (obj.TryGetComponent(out Gate gate)){
                    gate.Activate();
                    anim.SetTrigger("Deactivate");
                }
            }

    }

    public void _PlayButtonClip(){
        audioSource.Play();
    }
}
