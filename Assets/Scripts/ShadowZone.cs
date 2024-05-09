using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out MainCharacter player)){
            player.canHide = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.TryGetComponent(out MainCharacter player)){
            player.canHide = false;
        }
    }
}
