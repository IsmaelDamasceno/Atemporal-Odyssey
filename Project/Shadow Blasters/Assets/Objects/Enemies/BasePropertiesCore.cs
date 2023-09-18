using CrystalBot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EnemyState
{
	Patrol,
	Damage,
	Attack
}

public abstract class BasePropertiesCore: MonoBehaviour
{
	public int health;
	public abstract void ChangeState(EnemyState newState);
}
