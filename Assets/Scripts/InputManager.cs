using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Ref")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] BoatControl boatControl;

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
        playerInput = GetComponent<PlayerInput>();

        Cursor.visible = false;
    }

    private void Start() {
//        boatControl = GameObject.Find("Ship").GetComponent<BoatControl>();
        boatControl.enabled = false;
    }

    void OnBoat(){
        if(playerInput.currentActionMap.name == "Player"){
            playerInput.SwitchCurrentActionMap("Boat");
            BoatManager.instance.EnterBoat();
        } else {
            playerInput.SwitchCurrentActionMap("Player");
            BoatManager.instance.ExitBoat();
        }

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

    // ###### BOAT INPUT ######

    void OnSpeedUp(){
        boatControl.ManageLevelOfSpeed(1);
    }

    void OnSlowDown(){
        boatControl.ManageLevelOfSpeed(-1);
    }

    void OnSteerRight(){
        boatControl.ManageSteer(-1);
    }

    void OnSteerLeft(){
        boatControl.ManageSteer(1);
    }

}
