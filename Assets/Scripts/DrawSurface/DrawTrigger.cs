using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrigger : MonoBehaviour
{
    public bool isActive = false;
    private DrawRecognization reco = DrawRecognization.instance;
    public int id;

    private void Update() {
        reco.triggerActivated[id-1] = isActive;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Brush" && !isActive){
            isActive = true;
            SetOrder();
        }


    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Brush"){
            isActive = false;
        }
    }

    private void SetOrder(){
        reco.orderCombination += id.ToString();
    }
}
