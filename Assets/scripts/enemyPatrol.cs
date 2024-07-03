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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Home")
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isAttacking", true);
            Debug.Log("Trigger Enter: isRunning set to false and isAttacking set to true");
            isMoving = false;
        }
    }
}
