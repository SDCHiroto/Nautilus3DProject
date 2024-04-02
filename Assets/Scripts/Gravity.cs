using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody otherRb;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Character")
        {
            if (other.GetComponent<Rigidbody>())
            {
                otherRb = other.GetComponent<Rigidbody>();
                otherRb.velocity = Vector3.zero;
                otherRb.useGravity = false;
            }
        }
    }

    private void Update()
    {
        if(otherRb != null)
        {
            otherRb.AddForce(otherRb.transform.up * otherRb.mass * 2f, ForceMode.Force);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Character")
        {
            if (other.GetComponent<Rigidbody>())
            {
                otherRb.useGravity = true;
                otherRb = null;
            }
        }
    }
}
