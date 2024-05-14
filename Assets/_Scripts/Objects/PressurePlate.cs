using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] connectedObject;
    [SerializeField] Animator anim;
    AudioSource audioSource;

    bool isActive = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ArmStatue golem))
        {
            Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ArmStatue golem))
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        isActive = false;
        foreach (GameObject obj in connectedObject)
        {
            if (obj.TryGetComponent(out Laserbeam laser))
            {
                laser.Off();
                anim.SetTrigger("Deactivate");
            }
        }
    }

    public void Activate()
    {
        isActive = true;
        foreach (GameObject obj in connectedObject)
        {
            if (obj.TryGetComponent(out Laserbeam laser))
            {
                laser.On();
                anim.SetTrigger("Activate");
            }
        }
    }

    public void _PlayPPClip(){
        audioSource.Play();
    }



}
