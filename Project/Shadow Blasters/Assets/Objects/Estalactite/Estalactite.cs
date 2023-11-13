using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EstalactiteMode
{
    Asleep,
    Falling,
    Grounded
}

public class Estalactite : MonoBehaviour
{

    [SerializeField] private LayerMask contactLayers;
    [SerializeField] private float interactionDistance;
    [SerializeField] private GameObject particles;
    private Rigidbody2D rb;
    private CauseDamage causeDamage;

	[SerializeField] private EstalactiteMode currentMode = EstalactiteMode.Asleep;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        causeDamage = GetComponent<CauseDamage>();
        causeDamage.active = false;
        rb.isKinematic = true;
	}

    void Update()
    {
		if (currentMode == EstalactiteMode.Asleep)
        {
			if (Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.down, interactionDistance, contactLayers))
			{
				rb.isKinematic = false;
                currentMode = EstalactiteMode.Falling;
                causeDamage.active = true;
                GameObject obj = Instantiate(particles);
                obj.transform.position = transform.position + Vector3.up;
			}
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && currentMode == EstalactiteMode.Falling)
		{
			rb.velocity = Vector2.zero;
			rb.isKinematic = true;
            causeDamage.active = false;
			currentMode = EstalactiteMode.Grounded;
        }
	}
}
