using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class InteractionMember : MonoBehaviour
	{
		void Start()
		{

		}

		void Update()
		{

		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
			{
				if (Player.DamageMember.s_Ivulnerable)
				{
					return;
				}

				Vector2 playerPos = collision.gameObject.transform.position;
				int direction = Math.Sign(playerPos.x - transform.position.x);

				Player.DamageMember.s_Instance.ApplyDamage(new Vector2(5f * direction, 6f), 1);
			}
		}
	}
}