using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class BoatControl : MonoBehaviour
{
    [SerializeField] public int levelOfSpeedForward = 0;
    [SerializeField] float boatSpeed, steerSpeed;
    [SerializeField] float steer, distanceFromWater;
    [SerializeField] GameObject waterPlane;
    public Transform PositionWhereApplyForce, socketForPlayer;
    public Rigidbody rb;

    public bool touchingWater, touchingGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        waterPlane = GameObject.Find("WaterPlane");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();   
        Equilibre();
        Debug.Log(rb.velocity.magnitude);
    }

    void Move(){
        if(touchingWater && !touchingGround) {
            rb.AddForce(transform.forward * levelOfSpeedForward * boatSpeed, ForceMode.Impulse);
            if(levelOfSpeedForward > 0) {
                rb.AddForceAtPosition(transform.right * steer * steerSpeed , PositionWhereApplyForce.position);
            }
            rb.constraints = RigidbodyConstraints.None;
            if(rb.velocity.magnitude > 90) rb.velocity = Vector3.ClampMagnitude(rb.velocity, 90);

        } else if(touchingGround){
            rb.AddForce(transform.forward * levelOfSpeedForward * boatSpeed, ForceMode.Impulse);
            if(levelOfSpeedForward > 0) {
                rb.AddForceAtPosition(transform.right * steer * steerSpeed , PositionWhereApplyForce.position);
            }      
            rb.constraints = RigidbodyConstraints.None;
            if(rb.velocity.magnitude > 60) rb.velocity = Vector3.ClampMagnitude(rb.velocity, 60);

        } else if (!touchingGround && !touchingWater){
            rb.AddForce(transform.forward * levelOfSpeedForward * boatSpeed, ForceMode.Impulse);
            rb.AddForce(Vector3.down * 50f, ForceMode.Impulse);
                Debug.Log("3");
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            if(rb.velocity.magnitude > 70) rb.velocity = Vector3.ClampMagnitude(rb.velocity, 70);
      
        }
    }

    public Transform equilibreDx, equilibreSx;
    public float powerOfEquilibre;

    void Equilibre(){
        if(transform.eulerAngles.z > 10) {
            rb.AddForceAtPosition(transform.up * powerOfEquilibre, equilibreDx.position);
        } else if(transform.eulerAngles.z < -10) {
             rb.AddForceAtPosition(transform.up * powerOfEquilibre, equilibreSx.position);
        }
    }

    public void ManageLevelOfSpeed(int value){
        levelOfSpeedForward += value;
        if(levelOfSpeedForward < 0) levelOfSpeedForward = 0;
        if(levelOfSpeedForward > 3) levelOfSpeedForward = 3;
    }

    public void ManageSteer(int value){
        steer += value;
        if(steer < -1) steer = -1;
        if(steer > 1) steer = 1;
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water")){
            touchingWater = true;
        } else {
            touchingGround = true;
        }
        
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water")){
            touchingWater = false;
        } else { 
            touchingGround = false;
        }
    }
}
