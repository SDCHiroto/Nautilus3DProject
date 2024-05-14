using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] GameObject[] connectedObject;
    [SerializeField] Animator anim;
    AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    public void Interact()
    {
        foreach (GameObject obj in connectedObject)
        {
            if (obj.TryGetComponent(out IActivable objHitted))
            {
                objHitted.Activate();
                anim.SetTrigger("Activate");
            }

        }
    }

    public void _PlayLeverClip()
    {
        audioSource.Play();
    }
}
