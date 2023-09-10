using Enemy;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
	private EnemyBehaviour _behaviour;

	void Start()
	{
		_behaviour = transform.parent.GetComponent<EnemyBehaviour>();
		DamageMember damageMember = transform.parent.GetComponent<DamageMember>();
		damageMember.WallDetection = this;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_behaviour.OnFloor)
		{
			_behaviour.Direction *= -1;
			transform.parent.localScale = new Vector2(
				_behaviour.Direction * Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
		}
	}
}
