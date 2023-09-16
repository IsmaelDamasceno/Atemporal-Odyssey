using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CrystalBot
{
	public class GroundDetection : MonoBehaviour
	{
		private EnemyBehaviour _behaviour;

		void Start()
		{
			_behaviour = transform.parent.GetComponent<EnemyBehaviour>();
			DamageMember damageMember = transform.parent.GetComponent<DamageMember>();
			damageMember.GroundDetection = this;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (_behaviour.OnFloor)
			{
				_behaviour.Direction *= -1;
				transform.parent.localScale = new Vector2(
					_behaviour.Direction * Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
			}
		}
	}
}
