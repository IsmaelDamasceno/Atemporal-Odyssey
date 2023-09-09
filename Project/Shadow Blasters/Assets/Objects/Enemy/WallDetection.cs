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
		Debug.Log("Entrando Parede");
		_behaviour.Direction *= -1;
		transform.parent.localScale = new Vector2(_behaviour.Direction * transform.parent.localScale.x, transform.parent.localScale.y);
	}
}
