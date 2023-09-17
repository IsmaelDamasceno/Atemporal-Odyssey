using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
	public void ApplyDamage(Transform hitTransform, Vector2 impact, int amount);
}
