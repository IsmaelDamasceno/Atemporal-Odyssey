using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
	enum SwimState
	{
		Sinking,
		Splashing,
		Floating
	}
	public class SwimComponent : MonoBehaviour
	{
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

		private SwimState currentState = SwimState.Sinking;

		void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		private void OnEnable()
		{
			originalGravScale = rb.gravityScale;
			rb.gravityScale = 0f;
			yVel = rb.velocity.y;
		}
		private void OnDisable()
		{
			rb.gravityScale = originalGravScale;
		}

		private void FixedUpdate()
		{
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
