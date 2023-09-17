using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float elevatorSpeed;
    [SerializeField] private LayerMask playerMask;

    private int direction = 0;
    private int currentPoint = 0;
    private Rigidbody2D rb;
    private List<float> points = new();
    private BoxCollider2D collisionPoint;
    private bool usingLastFrame = false;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach(Transform pointTrs in transform.GetChild(1))
        {
            points.Add(pointTrs.position.y);
        }

        animator = GetComponent<Animator>();
        collisionPoint = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (direction == 0)
        {
            bool usingElevator = Physics2D.BoxCast(collisionPoint.transform.position, collisionPoint.size, 0f, Vector2.down, 0f, playerMask);
			if (usingElevator && !usingLastFrame)
			{
				currentPoint = (currentPoint + 1) % points.Count;
				direction = Math.Sign(points[currentPoint] - transform.position.y);
				animator.SetBool("Active", true);
			}
            usingLastFrame = usingElevator;
		}
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(0f, direction * elevatorSpeed);
        if (direction != 0)
        {
			if ((direction == 1 && transform.position.y > points[currentPoint]) ||
                (direction == -1 && transform.position.y < points[currentPoint]))
			{
                direction = 0;
                animator.SetBool("Active", false);
                transform.position = new Vector2(transform.position.x, points[currentPoint]);
			}
		}
    }
}
