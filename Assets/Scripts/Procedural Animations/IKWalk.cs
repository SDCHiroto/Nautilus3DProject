using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IKWalk : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] float footSpacing;
    [SerializeField] LayerMask whatIsGround;

    void Update()
    {
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit info, 10, whatIsGround)){
            transform.position = info.point;
        }
    }
}
