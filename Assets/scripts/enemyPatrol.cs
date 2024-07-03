using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Transform currentPoint;
    public float speed;
    public float spreadRadius = 2.0f; // Radius around the home to spread zombies

    private bool isMoving;
    private bool reachedHome = false; // Flag to check if zombie reached home
    private Vector2 targetPosition; // Target position around home
    private float initialHeight; // Store the initial height

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isMoving = true;
        anim.SetBool("isRunning", true);
        initialHeight = transform.position.y; // Store the initial height
        Debug.Log("Start: isRunning set to true");

        if (currentPoint == null)
        {
            Debug.LogError("currentPoint is not assigned!");
            enabled = false; // Disable the script to prevent further errors
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving || currentPoint == null) return;

        if (!reachedHome)
        {
            Vector2 movePosition = new Vector2(currentPoint.position.x, initialHeight); // Keep the initial height
            Vector2 newPosition = Vector2.MoveTowards(rb.position, movePosition, speed * Time.deltaTime);
            rb.MovePosition(newPosition);

            Debug.Log($"Zombie Position: {transform.position}");
            Debug.Log($"Current Point Position: {currentPoint.position}");
            Debug.Log($"Distance: {Vector2.Distance(transform.position, currentPoint.position)}");
        }
        else
        {
            // Move towards the target position around home
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.deltaTime);
            rb.MovePosition(newPosition);

            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                // Stop moving when close to the target position
                isMoving = false;
                rb.velocity = Vector2.zero;
                Debug.Log("Zombie reached target position around home.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Home" && !reachedHome)
        {
            reachedHome = true;
            anim.SetBool("isAttacking", true); // Set isAttacking to true
            Debug.Log("Trigger Enter: isRunning set to false and isAttacking set to true");
            
            // Set a target position around home
            SetTargetPositionAroundHome();
        }
    }

    private void SetTargetPositionAroundHome()
    {
        // Calculate a random horizontal offset within the spread radius
        float randomOffsetX = Random.Range(-spreadRadius, spreadRadius);
        targetPosition = new Vector2(currentPoint.position.x + randomOffsetX, initialHeight); // Keep the initial height
        Debug.Log($"Zombie target position around home set to: {targetPosition}");
    }
}
