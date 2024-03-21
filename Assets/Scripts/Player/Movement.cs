
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
    [SerializeField] public Transform exitPoint;

    [Header("Ground Check")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] float offset;
    [SerializeField] bool isJumping;

    [Header("Animation")]
    [SerializeField] Animator anim;
    [SerializeField] float HorizontalVelocity;
   

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
        anim = GetComponent<Animator>();
    }
    
    protected void Update(){
        
    }

    protected void FixedUpdate() {
        Move();
        ManageAnimations();
        if(isJumping && IsGrounded()){
            isJumping = false;
            anim.SetBool("isJumping", isJumping);
        }
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

    void ManageAnimations(){
        HorizontalVelocity = rb.velocity.x;
        anim.SetFloat("HorizontalVelocity", Mathf.Abs(HorizontalVelocity));
        
    }

    protected void OnJump(){
        if(IsGrounded()){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Invoke("SetIsJumping", 0.1f);
        }
            
    }

    void SetIsJumping(){
        isJumping = true;
        anim.SetBool("isJumping", isJumping);
    }

    bool IsGrounded(){
        return Physics.OverlapBox(new Vector3(groundCheck.position.x, groundCheck.position.y - offset, groundCheck.position.z), new Vector3(transform.localScale.x - 1f/2, 0.5f/2, transform.localScale.z - .4f/2),Quaternion.identity, whatIsGround).Length > 0 ? true : false;
    }
    

    protected void OnDrawGizmos() {
       Gizmos.DrawWireCube(new Vector3(groundCheck.position.x, groundCheck.position.y - offset, groundCheck.position.z), new Vector3(transform.localScale.x - 1 , 0.5f, transform.localScale.z- .4f));
    }

    [SerializeField] protected float degreesOfRotation;
    protected void Flip(){
        Debug.Log("flip");
        isFacingRight = !isFacingRight;
        if(isFacingRight) transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, degreesOfRotation , transform.localRotation.z));
        else transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, -degreesOfRotation , transform.localRotation.z));
    }

    void OnSwitchCharacter(){
        Switch.instance.SwitchCharacter();
    }

    protected void OnAction(){
        
    }
}
