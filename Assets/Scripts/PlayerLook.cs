
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensX;
    public float sensY;
    
    public Transform cam;
    public Vector2 lookInput;

    float xRotation;
    float yRotation;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = Camera.main.transform;
    }



    private void Update() {
            // float mouseX = lookInput.x * Time.deltaTime * sensX;
            // float mouseY = lookInput.y * Time.deltaTime * sensY;

            // yRotation += mouseX;
            // xRotation -= mouseY;
            // xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // cam.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            // this.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
}
