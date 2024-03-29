using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    private void Update()
    {
        target = Switch.instance.controlledChara.transform;
        this.transform.position = new Vector3(target.position.x, target.position.y + 2f, -10f);    
    }
}
