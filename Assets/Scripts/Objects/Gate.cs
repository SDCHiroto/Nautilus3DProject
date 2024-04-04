using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Interactable
{

    [SerializeField] bool isClosed = true;

    override public void Use(){
        if(isClosed) Open(); else Close();
    }

    void Open(){
        isClosed = false;
        gameObject.SetActive(false);
    }

    void Close(){
        isClosed = true;
        gameObject.SetActive(true);
    }
}
