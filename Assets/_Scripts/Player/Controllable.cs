using System.Collections.Generic;
using Unity.VisualScripting; // Assumo che questo namespace sia utilizzato per Unity Visual Scripting, puoi rimuoverlo se non è necessario
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Controllable : MonoBehaviour, IPowerable
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 10f; // Velocità di movimento del personaggio
    [SerializeField] float jumpForce = 5f; // Forza del salto
    protected bool isFacingRight = true; // Flag che indica se il personaggio sta guardando verso destra
    private float horizontal; // Input orizzontale
    [SerializeField] float mass;

    [Header("Health System")]
    [SerializeField] float currentHealth, maxHealth;

    [Header("Refs")]
    [SerializeField] Transform mesh; // Riferimento alla mesh del personaggio
    [SerializeField] Rigidbody rb; // Riferimento al Rigidbody del personaggio
    [SerializeField] public PlayerInput playerInput; // Riferimento all'input del giocatore
    [SerializeField] public SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] Material outLineMaterial; 
    [SerializeField] AudioSource audioSource;

    [Header("Ground Check")]
    [SerializeField] LayerMask whatIsJumpSurface; // Maschera per definire cosa è considerato terreno per permettere al giocatore di saltare
    [SerializeField] Transform groundCheck; // Punto di controllo per verificare se il personaggio è a terra
    [SerializeField] float offset; // Offset per il controllo del terreno
    [SerializeField] bool isJumping; // Flag che indica se il personaggio sta saltando

    [Header("Checkpoint info")]
    [SerializeField] protected Vector3 checkpoint;

    [Header("Animation")]
    [SerializeField] protected Animator anim; // Riferimento all'Animator del personaggio
    [SerializeField] float HorizontalVelocity; // Velocità orizzontale del personaggio

    public int currentRoom;
    public bool isDead = false;

    protected void OnEnable() {
        horizontal = 0;
        rb.mass = mass;
        playerInput.enabled = true;
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Collider>().enabled = true;
    }

    protected void OnDisable() {
        horizontal = 0;
        rb.mass = 999;
        ManageWalkingAnimations();
        playerInput.enabled = false;
    }

    protected void Awake() {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        checkpoint = this.transform.position;
    }


    protected void FixedUpdate() {
        Move(); // Gestisce il movimento del personaggio
        ManageWalkingAnimations(); // Gestisce le animazioni di camminata del personaggio
        if(isJumping && CanJump()){
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
        if(CanJump()){ // Controlla se il personaggio è a terra prima di saltare
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Applica la forza del salto al personaggio
            Invoke("SetIsJumping", 0.1f); // Invoca il metodo per impostare il flag di salto dopo un breve ritardo
        }     
    }

    void SetIsJumping(){
        isJumping = true; // Imposta il flag di salto a true
        anim.SetBool("isJumping", isJumping); // Imposta l'animazione di salto a true
    }

    public void AddOutline(){
        Material[] materials = skinnedMeshRenderer.sharedMaterials;

        if (!ContainsMaterial(materials, outLineMaterial))
        {
            List<Material> mats = new List<Material>(materials);
            mats.Add(outLineMaterial);
            materials = mats.ToArray();
            skinnedMeshRenderer.sharedMaterials = materials;
        }
    }

    public void RemoveOutline(){
        Material[] materials = skinnedMeshRenderer.sharedMaterials;

        if (ContainsMaterial(materials, outLineMaterial))
        {
            List<Material> mats = new List<Material>(materials);
            mats.Remove(outLineMaterial);
            materials = mats.ToArray();
            skinnedMeshRenderer.sharedMaterials = materials;
        }
    }

    bool ContainsMaterial(Material[] materials, Material material)
    {
        foreach (Material m in materials)
        {
            if (m == material)
            {
                return true;
            }
        }
        return false;
    }

    bool CanJump(){
        // Controlla se il personaggio è a terra usando una sfera di collisione
        return Physics.OverlapBox(new Vector3(groundCheck.position.x, groundCheck.position.y - offset, groundCheck.position.z), new Vector3(groundCheck.localScale.x - 1f/2, 0.5f/2, groundCheck.localScale.z - .4f/2),
        Quaternion.identity, whatIsJumpSurface).Length > 0 ? true : false;
    }
    
    
    void DisableInput(){
        if(playerInput.enabled) {
            playerInput.enabled = false;
        }
    }

    void RestoreInput(){
        if(!playerInput.enabled) {
            playerInput.enabled = true;
        }
    }

    protected void OnDrawGizmos() {
       Gizmos.DrawWireCube(new Vector3(groundCheck.position.x, groundCheck.position.y - offset, groundCheck.position.z), new Vector3(transform.localScale.x - 1 , 0.5f, transform.localScale.z- .4f));
    }

    [SerializeField] protected float degreesOfRotation; // Gradi di rotazione per il flip, serve perché alcune mesh devono ruotare ma senza far ruoratare il gameobject in se, e viceversa
    protected void Flip(){
        isFacingRight = !isFacingRight; // Inverte la direzione in cui il personaggio sta guardando
        if (isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, 90, transform.localRotation.z));
        }
        else
        {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, degreesOfRotation, transform.localRotation.z));
        }

    }

    protected void OnSwitchCharacter(){
        SwitchManager.instance.SwitchCharacter(); // Chiamata al metodo per cambiare personaggio

    }

    public void SetisControllingAnimation(bool temp_bool){
        anim.SetBool("isControlling", temp_bool);
    }

    protected abstract void OnAction();

    protected void Respawn(){
        this.transform.position = checkpoint;
    }

    void FadeInEffect()
    {
        GeneralManager.instance.FadeInAnimation();
    }

    public void Power(){
        ActivatedByBullet();
    }

    protected void ActivatedByBullet()
    {
        OnAction();
    }

    public void StopInteractAnimation(){
        anim.SetBool("Interact", false);
    }

    public void _PlayStepClip(){
        if(CanJump()){
            audioSource.Play();            
        }
    }

    void OnPause(){
        GeneralManager.instance.PausePressed();    
    }

    void OnRespawn(){
        GeneralManager.instance.ReloadScene();
    }
}
