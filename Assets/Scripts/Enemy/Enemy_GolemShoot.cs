using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle, Patrol, Charge, Attack
};

public class Enemy_GolemShoot : MonoBehaviour
{
    [SerializeField] Transform shootPoint, groundCheck, obstacleCheck;
    [SerializeField] bool isSeeingPlayer;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] State state = State.Idle;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] Animator anim;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = 1f;
    bool canShoot = true;
    public bool isFacingRight;

    public bool needToFlip;


    public float range;
    public float degreesOfRotation;
    public LayerMask whatIsGround;
    private bool isDead;

    private void Awake() {
        anim = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(!GeneralManager.instance.isPaused && !isDead){
            CheckForFloor();
            CheckForPlayer();
            Debug.Log(state);
            switch (state)
            {
                case State.Idle:
                    //Aspetta X secondi
                    Invoke("ToPatrol", 1f);
                    break;

                case State.Patrol:
                    //Muoviti 
                    Move();
                    break;

                case State.Charge:
                    //Spara un proiettile nella direzione dove sta guardando (e quindi dove ha visto il player)
                    Invoke("ToAttack",1f);
                    break;

                case State.Attack:  
                    if(canShoot){
                        ShootBullet();
                    }
                    break;

            }
        }

    }

    void ShootBullet(){
        canShoot = false;
        Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        Invoke("ResetShoot", fireRate);
    }

    void ResetShoot(){
        canShoot = true;
    }
    void CheckForFloor()
    {
        if(!Physics.Raycast(groundCheck.position, -transform.up, 5, whatIsGround) || Physics.Raycast(groundCheck.position, transform.forward, .5f, whatIsGround))
        {
            Debug.Log("Non c'è il pavimentoolo");
            Flip();

        } 
    }

    void Move()
    {
        //rb.velocity = new Vector2(horizontal * walkSpeed, rb.velocity.y); // Applica la velocit� di movimento
        this.transform.position += transform.forward * walkSpeed * Time.deltaTime;
    }

    private void CheckForPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(obstacleCheck.position, transform.forward, out hit, range))
        {
            // Check if the hit object is the player
            if (hit.collider.CompareTag("Player") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Controlled"))
            {
                if(state != State.Charge && state != State.Attack)
                    ToCharge();                    
                Debug.Log("Player found");
            } else ToPatrol();
        }
        else
        {
    
            Debug.Log("Player not detected");
        }
    }

    protected void OnDrawGizmos() {
       Gizmos.DrawRay(obstacleCheck.position, transform.forward);
    }

    void ToPatrol()
    {
        anim.SetBool("isWalking", true);
        state = State.Patrol;
    }

    void ToIdle()
    {
        anim.SetBool("isWalking", false);
        state = State.Idle;
    }

    void ToAttack()
    {
        state = State.Attack;
    }

    void ToCharge(){
        anim.SetBool("isWalking", false);
        state = State.Charge;
    }

    protected void Flip()
    {
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

    public void Die(){
        if(!isDead){
            isDead = true;
            anim.SetTrigger("Death");
        }
    }

    public void DestroyEnemy(){
        Destroy(this.gameObject);
    }
}
