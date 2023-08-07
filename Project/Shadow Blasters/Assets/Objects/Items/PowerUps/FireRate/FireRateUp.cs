using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/FireRateBuff")]
public class FireRateUp : PowerUpEffect
{
    public float amount;

    public override string member { get => "Attack"; set => throw new System.NotImplementedException(); }
    public override void Apply(GameObject target)
    {
        target.GetComponent<AttackMember>()._cooldown = amount;
    }
}