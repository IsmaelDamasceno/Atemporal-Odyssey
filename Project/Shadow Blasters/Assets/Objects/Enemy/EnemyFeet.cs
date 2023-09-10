using Enemy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFeet : MonoBehaviour
{

    public bool OnFloor;
    private EnemyBehaviour _behaviour;

    void Start()
    {
        transform.parent.GetComponent<DamageMember>().Feet = this;
        _behaviour = transform.parent.GetComponent<EnemyBehaviour>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnFloor = true;
        _behaviour.OnFloor = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
		OnFloor = false;
		_behaviour.OnFloor = true;
	}
}
