
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour
{
    public Transform[] floaters;
    [SerializeField] float underWaterDrag = 3;
    [SerializeField] float underWaterAngularDrag = 1;
    [SerializeField] float airDrag = 0;
    [SerializeField] float airAngularDrag = 0.05f;
    [SerializeField] float floatingPower = 15;

    [SerializeField] float waterHeight = 0f;

    private Rigidbody rb;
    int numOfFloatersUnderwater;
    bool isUnderwater;

    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    void FixedUpdate()
    {
        numOfFloatersUnderwater = 0;
        foreach(Transform floater in floaters){
            float difference = floater.position.y - waterHeight;
            if(difference < 0 ) {
                rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floater.position, ForceMode.Force);
                numOfFloatersUnderwater++;
                if(!isUnderwater){
                    isUnderwater = true;
                    SwitchState(true);
                }
            }
        }
        if(isUnderwater && numOfFloatersUnderwater == 0) {
                isUnderwater = false;
                SwitchState(false);
            }
        }

    void SwitchState(bool underwater){
        if(underwater) {
            rb.drag = underWaterDrag;
            rb.angularDrag = underWaterAngularDrag;
        } else {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }
}
