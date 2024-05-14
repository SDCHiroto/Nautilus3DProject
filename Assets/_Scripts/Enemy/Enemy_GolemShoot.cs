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
    [SerializeField] AudioSource asStep, asDeath;

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
            CheckForFloor(); // Controlla se il nemico è sul pavimento
            CheckForPlayer(); // Controlla se il nemico vede il player
            Debug.Log(state);
            switch (state)
            {
                case State.Idle:
                    // Aspetta un certo tempo prima di passare al pattugliamento
                    Invoke("ToPatrol", 1f);
                    break;

                case State.Patrol:
                    // Muoviti nella direzione corrente
                    Move();
                    break;

                case State.Charge:
                    // Prepara il nemico a sparare al player
                    Invoke("ToAttack",1f);
                    break;

                case State.Attack:  
                    // Spara un proiettile se può farlo
                    if(canShoot){
                        ShootBullet();
                    }
                    break;
            }
        }
    }

    // Funzione per sparare un proiettile
    void ShootBullet(){
        canShoot = false;
        Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        Invoke("ResetShoot", fireRate);
    }

    // Resetta la possibilità di sparare
    void ResetShoot(){
        canShoot = true;
    }

    // Controlla se c'è il pavimento sotto il nemico
    void CheckForFloor()
    {
        if(!Physics.Raycast(groundCheck.position, -transform.up, 5, whatIsGround) || Physics.Raycast(groundCheck.position, transform.forward, .5f, whatIsGround))
        {
            Debug.Log("Non c'è il pavimento");
            Flip();
        } 
    }

    // Muoviti nella direzione corrente
    void Move()
    {
        this.transform.position += transform.forward * walkSpeed * Time.deltaTime;
    }

    // Rileva se il player è davanti al nemico
    private void CheckForPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(obstacleCheck.position, transform.forward, out hit, range))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Controlled"))
            {
                if (state != State.Charge && state != State.Attack)
                {
                    ToCharge();
                }
            }
            else
            {
                ToPatrol();
            }
        }
    }

    protected void OnDrawGizmos() {
       Gizmos.DrawRay(obstacleCheck.position, transform.forward);
    }

    // Passa allo stato di pattugliamento
    void ToPatrol()
    {
        anim.SetBool("isWalking", true);
        state = State.Patrol;
    }

    // Passa allo stato di inattività
    void ToIdle()
    {
        anim.SetBool("isWalking", false);
        state = State.Idle;
    }

    // Passa allo stato di attacco
    void ToAttack()
    {
        state = State.Attack;
    }

    // Passa allo stato di carica
    void ToCharge(){
        anim.SetBool("isWalking", false);
        state = State.Charge;
    }

    // Inverte la direzione del nemico
    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        if (isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, 90, transform.localRotation.z));
        }
        else
        {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, degreesOfRotation, transform.localRotation.z));
        }
    }

    // Funzione per gestire la morte del nemico
    public void Die(){
        if(!isDead){
            isDead = true;
            anim.SetTrigger("Death");
        }
    }

    // Funzione per distruggere il nemico
    public void DestroyEnemy(){
        Destroy(this.gameObject);
    }

    // Riproduce l'effetto sonoro dei passi del nemico
    public void _PlayStepEnemyClip(){
        asStep.Play();
    }

    // Riproduce l'effetto sonoro della morte del nemico
    public void _PlayDeathClip(){
        asDeath.Play();
    }
}
