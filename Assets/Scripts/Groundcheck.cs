
using UnityEngine;

public class Groundcheck : MonoBehaviour
{
    [SerializeField] LayerMask whatIsGround;
    PlayerMovement playerMovement;

    private void Awake() {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other) {
        playerMovement.SetIsGrounded(true);
    }

    private void OnTriggerExit(Collider other) {;
        playerMovement.SetIsGrounded(false);
    }
}
