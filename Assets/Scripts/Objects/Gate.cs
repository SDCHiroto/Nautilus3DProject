using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Interactable
{
    override public void Use(){
        Debug.Log(" Debug di Gate ");
        Open();
    }

    void Open(){
        //Apri cancielo
        Debug.Log(" Open ");
        Destroy(gameObject);
    }
}
