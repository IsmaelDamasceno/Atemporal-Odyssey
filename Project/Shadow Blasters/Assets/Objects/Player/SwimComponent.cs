using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
	enum SwimState
	{
		Sinking,
		Floating
	}
	public class SwimComponent : MonoBehaviour
	{
		[SerializeField] private float waterInitialDistance;

		[Header("Fall")]
		[SerializeField] private AnimationCurve fallCurve;
		[SerializeField] private float fallTimeScale;

		[Header("Float")]
		[SerializeField] private AnimationCurve floatCurve;
		[SerializeField] private float floatTimeScale;
		[SerializeField] private float floatScale;

		private Rigidbody2D rb;
		private float originalGravScale;
		private float time = 0;
		private float yVel;
		private JumpMember jumpMember;
		private BoxCollider2D collider;

		private SwimState currentState = SwimState.Sinking;

		void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			jumpMember = GetComponent<JumpMember>();
			collider = GetComponent<BoxCollider2D>();
		}

		private void OnEnable()
		{
			originalGravScale = rb.gravityScale;
			rb.gravityScale = 0f;
			yVel = rb.velocity.y;

			int waterMask = LayerMask.NameToLayer("Water");
			RaycastHit2D hitInfo =
				Physics2D.BoxCast(transform.position + (Vector3)collider.offset - Vector3.up, collider.size, 0f, Vector2.down, 3f, waterMask);
			transform.position = new Vector2(transform.position.x, hitInfo.point.y + waterInitialDistance);

			Debug.Log(
				$"yPos: {transform.position.y}");
		}
		private void OnDisable()
		{
			rb.gravityScale = originalGravScale;
			currentState = SwimState.Sinking;
			time = 0f;
		}

		private void FixedUpdate()
		{
			if (jumpMember._startedJump)
			{
				enabled = false;
			}

			switch(currentState)
			{
				case SwimState.Sinking:
					{
						time += Time.deltaTime * fallTimeScale;
						yVel = fallCurve.Evaluate(time);
						if (time >= 1f)
						{
							yVel = 0f;
							time = 0f;
							currentState = SwimState.Floating;
						}
					}break;
				case SwimState.Floating:
					{
						time += Time.deltaTime * floatTimeScale;
						yVel = floatCurve.Evaluate(time) * floatScale;
						if (time >= 1f)
						{
							time -= 1f;
						}
					}
					break;
			}
			rb.velocity = new Vector2(rb.velocity.x, yVel);
		}
	}
}
