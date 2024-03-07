using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [Header("Wind Power")]
    [SerializeField] GameObject windParticleEffect;
    [SerializeField] Transform startPoint;
    [SerializeField] float maxDistance;

    private void Awake() {
        startPoint = GameObject.Find("StartPoint").transform;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            ActivateWind();
        }
    }

    void ActivateWind(){
        Instantiate(windParticleEffect, startPoint.position, startPoint.rotation);
        RaycastHit hit;
        if(Physics.Raycast(startPoint.transform.position, startPoint.transform.forward, out hit, maxDistance)){
            if(hit.collider.gameObject.GetComponent<Grabbable>()){ // Se il vento colpisce un "Grabbable" 
                GameObject objHitted = hit.collider.gameObject;
                Vector3 pointHitted = hit.point;
                //applicare una forza all'oggetto colpito, partendo dal punto colpito, la cui potenza è proporzionale alla vicinanza con l'origine della raffica di vento
            } 
        }
    }
}
