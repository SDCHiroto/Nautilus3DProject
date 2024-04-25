using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IActivable
{   
    [SerializeField] bool isOpen = false;

    public void Open(){
        isOpen = true;
        this.gameObject.SetActive(false);
    }

    public void Close(){
        isOpen = false;
        this.gameObject.SetActive(false);
    }

    public void Activate(){
            if(isOpen) {
                Close();
            } else {
                Open();
            }
    }

}
