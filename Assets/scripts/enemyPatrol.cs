using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
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
        currentPoint = pointB.transform;
        isMoving = true;
        anim.SetBool("isRunning", true);
        Debug.Log("Start: isRunning set to true");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Vector2 point = currentPoint.position - transform.position;
            rb.velocity = new Vector2(speed * Mathf.Sign(point.x), rb.velocity.y);

            Debug.Log($"Zombie Position: {transform.position}");
            Debug.Log($"Current Point Position: {currentPoint.position}");
            Debug.Log($"Distance: {Vector2.Distance(transform.position, currentPoint.position)}");

            if (Vector2.Distance(transform.position, currentPoint.position) < 1.0f) // Adjusted threshold
            {
                rb.velocity = Vector2.zero;
                anim.SetBool("isRunning", false);
                Debug.Log("Update: isRunning set to false");
                isMoving = false;
            }
        }
    }
}
