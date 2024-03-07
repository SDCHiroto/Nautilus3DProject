using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    public bool grabbed = false; 
    private Rigidbody rb;
    public Transform grabHolder;
    public BoxCollider boxCollider;
    private Vector3 originalPosition;
    public string interactionDescription;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        grabHolder = GameObject.Find("GrabHolder").GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider>();
        originalPosition = this.transform.position;
    }

    private void Update() {

    }

    public void Grab(){
        grabbed = true;
        rb.isKinematic = true;
        boxCollider.enabled = false;
        this.transform.position = grabHolder.position;
        this.transform.parent = grabHolder;
    }

    public void Release(){
        grabbed = false;
        rb.isKinematic = false;
        boxCollider.enabled = true;
        this.transform.parent = null;
    }

}
