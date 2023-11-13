using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauseDamage : MonoBehaviour
{
    [SerializeField] private Vector2 impact;
    [SerializeField] private int damage;

    public bool active = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!active)
        {
            return;
        }

        IDamage attackedTarget = collision.GetComponent<IDamage>();

        if (attackedTarget != null)
        {
            attackedTarget.ApplyDamage(transform, new Vector2(impact.x, impact.y), damage);
        }
    }
}
