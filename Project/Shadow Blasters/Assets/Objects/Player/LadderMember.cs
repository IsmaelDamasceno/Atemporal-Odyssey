using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LadderMember : MonoBehaviour
{

	[SerializeField] private float ladderSpeed;
	[SerializeField] private LayerMask groundMask;

	private Rigidbody2D rb;
	private float originalGravScale;
	private BoxCollider2D collider;

	private Vector3 ladderPos;
	private InputMember inputMember;
	private bool lerping = true;
	private Animator animator;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		inputMember = GetComponent<InputMember>();
		collider = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
	}

	public void StartLadder(Vector2 contactPos)
	{
        animator.SetBool("Ladder", true);

        ladderPos = new Vector3(
			contactPos.x,
			Mathf.Round(contactPos.y) - 0.5f,
			0f
		);

		originalGravScale = rb.gravityScale;
		rb.velocity = Vector2.zero;

		rb.gravityScale = 0f;
	}

	public void ExitLadder()
	{
        animator.SetBool("Ladder", false);
    }

	private void OnDisable()
	{
		rb.gravityScale = originalGravScale;
		lerping = true;
	}


	private void Update()
	{
		if (JumpMember.grounded && inputMember.MoveInput != 0f)
		{
			PropertiesCore.ExitLadder();
		}
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

		float ySpeed = inputMember.LadderInput * ladderSpeed;
		animator.SetFloat("Ladder Speed", Math.Abs(inputMember.LadderInput));
		if (ySpeed != 0f)
		{
			RaycastHit2D hitInfo = Physics2D.BoxCast(transform.position + (Vector3)collider.offset, new Vector2(collider.size.x * 0.5f, collider.size.y), 0f, Vector2.up * Math.Sign(ySpeed), ySpeed * Time.fixedDeltaTime * 2f, groundMask);

			if (hitInfo.transform == null)
			{
				rb.velocity = new Vector2(0f, ySpeed);
			}
			else
			{
				rb.velocity = new Vector2(0f, 0f);
			}
		}
		else
		{
			rb.velocity = new Vector2(0f, 0f);
		}
	}

	/*
	 void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;

		Vector3 pos = transform.position + (Vector3)collider.offset;

		float ySpeed = inputMember.LadderInput * ladderSpeed * Time.fixedDeltaTime * 2f;

		Gizmos.DrawSphere(new Vector3(pos.x-collider.size.x / 2f, pos.y + collider.size.y / 2f + ySpeed, 0f), 0.1f);
		Gizmos.DrawSphere(new Vector3(pos.x + collider.size.x / 2f, pos.y + collider.size.y / 2f + ySpeed, 0f), 0.1f);
		Gizmos.DrawSphere(new Vector3(pos.x - collider.size.x / 2f, pos.y - collider.size.y / 2f + ySpeed, 0f), 0.1f);
		Gizmos.DrawSphere(new Vector3(pos.x + collider.size.x / 2f, pos.y - collider.size.y / 2f + ySpeed, 0f), 0.1f);
	}
	 */
}
