using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Interactable
{

    [SerializeField] bool isClosed = true;

    override public void Use(){
        Debug.Log(" Debug di Gate ");
        if(isClosed) Open(); else Close();
    }

    void Open(){
        Debug.Log(" Open ");
        isClosed = false;
        gameObject.SetActive(false);
    }

    void Close(){
        isClosed = true;
        gameObject.SetActive(true);
    }
}
