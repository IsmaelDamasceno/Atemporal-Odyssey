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
    private Rigidbody2D rb;

	[SerializeField] private EstalactiteMode currentMode = EstalactiteMode.Asleep;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
			}
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && currentMode == EstalactiteMode.Falling)
		{
			rb.velocity = Vector2.zero;
			rb.isKinematic = true;
			currentMode = EstalactiteMode.Grounded;
		}
        else if (collision.CompareTag("Player") && currentMode == EstalactiteMode.Falling)
        {
			Player.DamageMember.s_Instance.ApplyDamage(transform, new Vector2(5f, 6f), 1);
		}
	}
}
