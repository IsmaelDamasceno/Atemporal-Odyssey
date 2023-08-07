using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{
    public abstract string member { get; set; }
    public abstract void Apply(GameObject target);
}
