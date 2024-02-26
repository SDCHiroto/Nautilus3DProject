using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Ref")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerInteractions playerInteractions;
    private  PlayerInput playerInput;


    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerInteractions = GameObject.Find("Player").GetComponent<PlayerInteractions>();
        playerInput = GetComponent<PlayerInput>();

    }

    private void Start() {
        playerInput.SwitchCurrentActionMap("Player");
    }

    #region ActionMap Player
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

    void OnStartDraw(){
        playerInput.SwitchCurrentActionMap("Drawing");

    }

    #endregion

    #region ActionMap Draw

    bool isClicked = false;
    void OnClick(){
        isClicked = !isClicked;

        if(isClicked)
            DrawManager.instance.StartDrawing();
        else 
            DrawManager.instance.EndDrawing();
    }

    void OnEndDraw(){
        isClicked = false;
        playerInput.SwitchCurrentActionMap("Player");
        DrawManager.instance.EndDrawing();

    }

    #endregion

}
