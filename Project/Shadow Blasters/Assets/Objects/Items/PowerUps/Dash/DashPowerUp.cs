using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/DashPowerUp")]
public class DashPowerUp : PowerUpEffect
{
    public override void Apply(GameObject target)
    {
		target.GetComponent<Player.DashMember>().enabled = true;
    }
}
