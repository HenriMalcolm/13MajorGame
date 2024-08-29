using UnityEngine;

public class CamTilt : MonoBehaviour
{
    public float speed = 5.0f;
    public float tiltAmount = 10.0f;  // The amount the camera tilts
    public Camera playerCamera;

    private Vector3 movement;

    void Update()
    {
        // Get input from the player
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Tilt the camera based on movement
        TiltCamera();
    }

    void TiltCamera()
    {
        if (movement != Vector3.zero)
        {
            // Calculate the tilt angle based on movement direction
            float tiltZ = movement.x * -tiltAmount;  // Tilt around Z-axis
            float tiltX = movement.z * tiltAmount;   // Tilt around X-axis

            // Create a new rotation for the camera
            Quaternion targetRotation = Quaternion.Euler(tiltX, playerCamera.transform.eulerAngles.y, tiltZ);

            // Smoothly rotate the camera towards the target rotation
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, targetRotation, Time.deltaTime * speed);
        }
        else
        {
            // Smoothly reset the camera rotation to zero tilt when not moving
            Quaternion targetRotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }
}