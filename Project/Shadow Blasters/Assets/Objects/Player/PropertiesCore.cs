using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class PropertiesCore : BaseOrgan
	{

		[HideInInspector] public Rigidbody2D Rigidbody;
		[HideInInspector] public BoxCollider2D Collider;
		[HideInInspector] public BoxCollider2D FeetCollider;

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
