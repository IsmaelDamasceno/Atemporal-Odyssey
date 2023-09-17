using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CrystalBot
{
	public class GroundDetection : MonoBehaviour
	{
		private EnemyBehaviour _behaviour;
		private EnemyFeet feet;

		void Start()
		{
			_behaviour = transform.parent.GetComponent<EnemyBehaviour>();
			DamageMember damageMember = transform.parent.GetComponent<DamageMember>();
			feet = transform.parent.GetComponentInChildren<EnemyFeet>();
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (feet.OnFloor)
			{
				_behaviour.Direction *= -1;
				transform.parent.localScale = new Vector2(
					_behaviour.Direction * Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
			}
		}
	}
}
