using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float elevatorSpeed;
    [SerializeField] private LayerMask playerMask;

    public int direction = 0;
    private int currentPoint = 0;
    private Rigidbody2D rb;
    private List<float> points = new();
    private BoxCollider2D collisionPoint;
    private bool usingLastFrame = false;
    private Animator animator;
    private Light2D light;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        light = GetComponentInChildren<Light2D>();
        light.enabled = false;
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
                GoNextFloor();
			}
            usingLastFrame = usingElevator;
		}
    }

    public void GoNextFloor()
    {
		currentPoint = (currentPoint + 1) % points.Count;
		direction = Math.Sign(points[currentPoint] - transform.position.y);
		animator.SetBool("Active", true);
		light.enabled = true;
	}
    public void GoToFloor(int floor)
    {
		currentPoint = floor;
		direction = Math.Sign(points[currentPoint] - transform.position.y);
		animator.SetBool("Active", true);
		light.enabled = true;
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
                light.enabled = false;
                transform.position = new Vector2(transform.position.x, points[currentPoint]);
			}
		}
    }
}
