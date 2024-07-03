using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of the zombie

    private Vector3 target; // The target position (center of the camera)
    private Camera mainCamera; // Reference to the main camera

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Calculate the target position (center of the screen)
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, mainCamera.nearClipPlane);
        target = mainCamera.ScreenToWorldPoint(screenCenter);

        // Move towards the target position
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        // Calculate the direction to the target
        Vector2 direction = (target - transform.position).normalized;

        // Move the zombie towards the target
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
