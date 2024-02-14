
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float runSpeed = 15f;
    [SerializeField] float jumpForce = 5f;

    [Header("Camera Movement")]
    [SerializeField] private float rotationSpeed = 4f;
    private Transform cam;

    bool isGrounded;

    bool isRunning = false;

    Vector2 movementVector;
    public Rigidbody rb;
    Vector3 moveDirection;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = Camera.main.transform;
    }

    private void Update() {
        
    }
    
    private void FixedUpdate() {
        Move();
    }

    void Move(){
        moveDirection = cam.forward * movementVector.y;
        moveDirection = moveDirection + cam.right * movementVector.x;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * walkSpeed;
        Vector3 movementVelocity = moveDirection;
        if(movementVector != Vector2.zero){
            float targeAngle = Mathf.Atan2(movementVector.x, movementVector.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targeAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
        rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);
        
    }



    public void Jump(){
        if(isGrounded){
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

    }

    public void SetIsRunning(){
        isRunning = !isRunning;
    }

    public void SetIsGrounded(bool val) {
        isGrounded = val;
    }

    public void SetMovementVector(Vector3 inputMovementVector){
        movementVector = inputMovementVector;
    }
    
}
