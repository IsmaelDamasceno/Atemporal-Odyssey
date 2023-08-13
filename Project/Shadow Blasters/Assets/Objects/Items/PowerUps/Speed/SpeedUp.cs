using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedUp : PowerUpEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<MoveMember>().MoveSpeed = amount;
    }
}
