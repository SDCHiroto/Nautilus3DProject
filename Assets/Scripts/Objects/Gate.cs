using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IActivable
{   
    [SerializeField] bool isOpen = false;
    [SerializeField] Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void Open(){
        isOpen = true;
        anim.SetTrigger("Open");
    }

    public void Close(){
        isOpen = false;
        anim.SetTrigger("Close");
    }

    public void Activate(){
            if(isOpen) {
                Close();
            } else {
                Open();
            }
    }

}
