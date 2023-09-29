using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMember : MonoBehaviour
{

	private Rigidbody2D rb;
	private float originalGravScale;

	private Vector3 ladderPos;
	private InputMember inputMember;
	private bool lerping = true;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		inputMember = GetComponent<InputMember>();
	}

	public void StartLadder(Vector2 contactPos)
	{
		ladderPos = new Vector3(
			contactPos.x,
			Mathf.Round(contactPos.y) - 0.5f,
			0f
		);

		originalGravScale = rb.gravityScale;
		rb.velocity = Vector2.zero;

		rb.gravityScale = 0f;
	}

	private void OnDisable()
	{
		rb.gravityScale = originalGravScale;
		lerping = true;
	}

	private void FixedUpdate()
	{
		if (lerping)
		{
			if (Vector3.Distance(transform.position, ladderPos) >= 0.05f)
			{
				transform.position = new Vector2(Mathf.Lerp(transform.position.x, ladderPos.x, 0.15f), transform.position.y);
			}
			else
			{
				transform.position = new Vector2(ladderPos.x, transform.position.y);
				lerping = false;
			}
		}
		rb.velocity = new Vector2(0f, inputMember.LadderInput * 5f);
	}
}
