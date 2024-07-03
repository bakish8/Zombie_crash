using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Transform currentPoint;
    public float speed;
    
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isMoving = true;
        anim.SetBool("isRunning", true);
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

        Vector2 point = currentPoint.position - transform.position;
        rb.velocity = new Vector2(speed * Mathf.Sign(point.x), rb.velocity.y);

        Debug.Log($"Zombie Position: {transform.position}");
        Debug.Log($"Current Point Position: {currentPoint.position}");
        Debug.Log($"Distance: {Vector2.Distance(transform.position, currentPoint.position)}");

        if (Vector2.Distance(transform.position, currentPoint.position) < 1.5f ||Vector2.Distance(transform.position, currentPoint.position) < -1.5f) // Adjusted threshold
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isAttacking", true);
            Debug.Log("Update: isRunning set to false");
            isMoving = false;
        }
        else{
            anim.SetBool("isAttacking", false);
        }
    }
}
