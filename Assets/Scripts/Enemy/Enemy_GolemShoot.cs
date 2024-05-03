using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle, Patrol, Attack
};

public class Enemy_GolemShoot : MonoBehaviour
{
    [SerializeField] Transform shootPoint, groundCheck;
    [SerializeField] bool isSeeingPlayer;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] State state = State.Idle;
    [SerializeField] float walkSpeed = 2f;
    public bool isFacingRight;

    public bool needToFlip;


    public float range;
    public float degreesOfRotation;
    public LayerMask whatIsGround;

    private void Start()
    {
        
    }

    private void Update()
    {
        CheckForFloor();
        Debug.Log(state);
        switch (state)
        {
            case State.Idle:
                //Aspetta X secondi

                Invoke("ToPatrol", 1f);
                break;

            case State.Patrol:
                //Muoviti e cerca il giocatore
                CheckForPlayer();
                Move();
                break;

            case State.Attack:
                //Muoviti verso il giocatore se in range e attacca
                break;

        }
    }

    void CheckForFloor()
    {
        if(!Physics.Raycast(groundCheck.position, -transform.up, 5, whatIsGround) || Physics.Raycast(groundCheck.position, transform.forward, .5f, whatIsGround))
        {
            Debug.Log("Non c'è il pavimentoolo");
            Flip();
        } else
        {
            Debug.Log("CAMMINA");
        }
    }

    void Move()
    {
        //rb.velocity = new Vector2(horizontal * walkSpeed, rb.velocity.y); // Applica la velocità di movimento
        this.transform.position += transform.forward * walkSpeed * Time.deltaTime;
    }
    private void CheckForPlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(shootPoint.position, transform.forward, out hit, range, whatIsPlayer))
        {
            isSeeingPlayer = true;
            Debug.Log("trovato player");
        } else
        {
            isSeeingPlayer = false;
            Debug.Log("non vedo il player");
        }
    }

    void ToPatrol()
    {
        state = State.Patrol;
    }

    void ToIdle()
    {
        state = State.Idle;
    }

    void ToAttack()
    {
        state = State.Attack;
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
}
