using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/FireRateBuff")]
public class FireRateUp : PowerUpEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        AttackMember plAttackMember = target.GetComponent<PropertiesCore>().GetMember("Attack") as Player.AttackMember;
		plAttackMember.Cooldown = amount;
    }
}
