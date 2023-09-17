using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CrystalBot
{
	public class EnemyFeet : MonoBehaviour
	{

		public bool OnFloor;
		private EnemyBehaviour _behaviour;

		void Start()
		{
			transform.parent.GetComponent<DamageMember>().Feet = this;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			OnFloor = true;

		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			OnFloor = false;
		}
	}
}
