using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    [SerializeField] SpringJoint[] springs;

    [Header("Springs values")]
    [SerializeField] float spring = 100f;
    [SerializeField] float damper = 0.2f;

    private void Update() {
        foreach(SpringJoint joint in springs){
            joint.spring = spring;
            joint.damper = damper;
        }
    }
}
