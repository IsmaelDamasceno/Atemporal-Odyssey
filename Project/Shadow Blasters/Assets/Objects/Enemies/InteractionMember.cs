using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrystalBot
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
				Player.DamageMember.s_Instance.ApplyDamage(transform, new Vector2(5f, 6f), 1);
			}
		}
	}
}