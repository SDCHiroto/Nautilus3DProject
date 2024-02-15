using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Ref")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerInteractions playerInteractions;
    private  PlayerInput playerInput;


    private void OnEnable() {
        Debug.Log("Enablato");
    }

    private void OnDisable() {
        Debug.Log("Disablato");
    }

    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerInteractions = GameObject.Find("Player").GetComponent<PlayerInteractions>();
        playerInput = GetComponent<PlayerInput>();

        Cursor.visible = false;
    }

    private void Start() {
//        boatControl = GameObject.Find("Ship").GetComponent<BoatControl>();

    }


    void OnMove(InputValue value){
        playerMovement.SetMovementVector(value.Get<Vector2>());
    }

    void OnSprint(){
        playerMovement.SetIsRunning();
    }

    void OnJump(){
        playerMovement.Jump();
    }

    void OnInteract(){
        playerInteractions.Interact();
    }
}
