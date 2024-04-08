using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 10f; // Velocità di movimento del personaggio
    [SerializeField] float jumpForce = 5f; // Forza del salto
    private bool isFacingRight = true; // Flag che indica se il personaggio sta guardando verso destra
    private float horizontal; // Input orizzontale

    [Header("Refs")]
    [SerializeField] Transform mesh; // Riferimento alla mesh del personaggio
    [SerializeField] protected Rigidbody rb; // Riferimento al Rigidbody del personaggio
    [SerializeField] PlayerInput playerInput; // Riferimento all'input del giocatore
    [SerializeField] public Transform exitPoint; // Punto di uscita del personaggio

    [Header("Ground Check")]
    [SerializeField] LayerMask whatIsGround; // Maschera per definire cosa è considerato terreno
    [SerializeField] Transform groundCheck; // Punto di controllo per verificare se il personaggio è a terra
    [SerializeField] float offset; // Offset per il controllo del terreno
    [SerializeField] bool isJumping; // Flag che indica se il personaggio sta saltando

    [Header("Animation")]
    [SerializeField] protected Animator anim; // Riferimento all'Animator del personaggio
    [SerializeField] float HorizontalVelocity; // Velocità orizzontale del personaggio

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
        // Questo metodo viene chiamato ad ogni frame, ma al momento non viene utilizzato per questo script
    }

    protected void FixedUpdate() {
        Move(); // Gestisce il movimento del personaggio
        ManageWalkingAnimations(); // Gestisce le animazioni di camminata del personaggio
        if(isJumping && IsGrounded()){
            isJumping = false;
            anim.SetBool("isJumping", isJumping); // Imposta l'animazione di salto a false se il personaggio è a terra
        }
    }
    
    void OnMove(InputValue value){
        horizontal = value.Get<Vector2>().x; // Ottiene l'input orizzontale dal player
    }

    void Move(){
        rb.velocity = new Vector2(horizontal * walkSpeed, rb.velocity.y); // Applica la velocità di movimento

        // Controlla la direzione in cui il personaggio sta guardando
        if(!isFacingRight && horizontal > 0f){
            Flip(); // Se il personaggio sta guardando verso sinistra e si muove verso destra, lo gira
        } else if(isFacingRight && horizontal < 0f) {
            Flip(); // Se il personaggio sta guardando verso destra e si muove verso sinistra, lo gira
        }
    }

    void ManageWalkingAnimations(){
        HorizontalVelocity = rb.velocity.x; // Ottiene la velocità orizzontale del personaggio
        anim.SetFloat("HorizontalVelocity", Mathf.Abs(HorizontalVelocity)); // Imposta il parametro dell'animazione di velocità orizzontale
    }

    protected void OnJump(){
        if(IsGrounded()){ // Controlla se il personaggio è a terra prima di saltare
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Applica la forza del salto al personaggio
            Invoke("SetIsJumping", 0.1f); // Invoca il metodo per impostare il flag di salto dopo un breve ritardo
        }
            
    }

    void SetIsJumping(){
        isJumping = true; // Imposta il flag di salto a true
        anim.SetBool("isJumping", isJumping); // Imposta l'animazione di salto a true
    }

    bool IsGrounded(){
        // Controlla se il personaggio è a terra usando una sfera di collisione
        return Physics.OverlapBox(new Vector3(groundCheck.position.x, groundCheck.position.y - offset, groundCheck.position.z), new Vector3(transform.localScale.x - 1f/2, 0.5f/2, transform.localScale.z - .4f/2),Quaternion.identity, whatIsGround).Length > 0 ? true : false;
    }
    

    protected void OnDrawGizmos() {
       Gizmos.DrawWireCube(new Vector3(groundCheck.position.x, groundCheck.position.y - offset, groundCheck.position.z), new Vector3(transform.localScale.x - 1 , 0.5f, transform.localScale.z- .4f));
    }

    [SerializeField] protected float degreesOfRotation; // Gradi di rotazione per il flip
    protected void Flip(){
        isFacingRight = !isFacingRight; // Inverte la direzione in cui il personaggio sta guardando
                // Ruota il personaggio di 180 gradi se sta guardando verso sinistra, altrimenti lo ruota nella posizione originale
        if(isFacingRight) transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, degreesOfRotation , transform.localRotation.z));
        else transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, -degreesOfRotation , transform.localRotation.z));
    }

    void OnSwitchCharacter(){
        Switch.instance.SwitchCharacter(); // Chiamata al metodo per cambiare personaggio
    }

    protected abstract void OnAction();
}
