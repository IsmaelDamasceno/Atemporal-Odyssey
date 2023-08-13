using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{
	public GameObject UIBuffItem;

	public abstract void Apply(GameObject target);
}
