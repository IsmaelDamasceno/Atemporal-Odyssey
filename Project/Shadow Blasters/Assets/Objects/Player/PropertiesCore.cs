using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Guarda referência de Components importados do jogador (rigidbody, colliders, etc)
	/// </summary>
	public class PropertiesCore : BaseMember
	{
		[HideInInspector] public Rigidbody2D Rigidbody;
		[HideInInspector] public BoxCollider2D Collider;
		[HideInInspector] public BoxCollider2D FeetCollider;

		[HideInInspector] public bool Attacking = false;

		void Awake()
		{
			Rigidbody = GetComponent<Rigidbody2D>();
			Collider = GetComponent<BoxCollider2D>();
			FeetCollider = transform.GetChild(1).GetComponent<BoxCollider2D>();
		}

		void Update()
		{

		}
	}
}
