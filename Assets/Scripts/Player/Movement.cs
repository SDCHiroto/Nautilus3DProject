
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    private bool isFacingRight = true;
    private float horizontal;

    [Header("Refs")]
    [SerializeField] Transform mesh;
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerInput playerInput;

    [Header("Ground Check")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] float offset;





    protected void OnEnable() {
        horizontal = 0;
        playerInput.enabled = true;
    }

    protected void OnDisable() {
        horizontal = 0;
        playerInput.enabled = false;
    }

    protected void Awake() {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    protected void Update() {
        
    }
    
    protected void FixedUpdate() {
        Move();
    }

    void OnMove(InputValue value){
        horizontal = value.Get<Vector2>().x;
    }

    void Move(){
        rb.velocity = new Vector2(horizontal * walkSpeed, rb.velocity.y);
        
        if(!isFacingRight && horizontal > 0f){
            Flip();
        } else if(isFacingRight && horizontal < 0f) {
            Flip();
        }
    }

    protected void OnJump(){
        if(IsGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    bool IsGrounded(){
        return Physics.OverlapBox(new Vector3(groundCheck.position.x, groundCheck.position.y - offset, groundCheck.position.z), new Vector3(transform.localScale.x - 1f/2, 0.5f/2, transform.localScale.z - .4f/2),Quaternion.identity, whatIsGround).Length > 0 ? true : false;
    }
    

    protected void OnDrawGizmos() {
       Gizmos.DrawWireCube(new Vector3(groundCheck.position.x, groundCheck.position.y - offset, groundCheck.position.z), new Vector3(transform.localScale.x - 1 , 0.5f, transform.localScale.z- .4f));
    }

    void Flip(){
        isFacingRight = !isFacingRight;
        Vector3 localScale = mesh.localScale;
        localScale.x *= -1;
        mesh.localScale = localScale;
    }

    void OnSwitchCharacter(){
        Switch.instance.SwitchCharacter();
    }

    protected void OnAction(){
        
    }
}
