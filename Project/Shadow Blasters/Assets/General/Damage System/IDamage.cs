using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
	public void ApplyDamage(Vector2 impact, int amount);
}
