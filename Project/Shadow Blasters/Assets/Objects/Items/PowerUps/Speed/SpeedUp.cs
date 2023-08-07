using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedUp : PowerUpEffect
{
    public float amount;

    public override string member { get => "Move"; set => throw new System.NotImplementedException(); }

    public override void Apply(GameObject target)
    {
        target.GetComponent<MoveMember>().MoveSpeed = amount;
    }
}
