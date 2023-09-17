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

public interface IPropertiesCore
{
	public void ChangeState(EnemyState newState);
}
