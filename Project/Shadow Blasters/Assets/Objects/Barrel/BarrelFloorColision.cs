using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelFloorColision : MonoBehaviour
{

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb.velocity.y <= -7.5f)
        {
            GetComponent<IDamage>().ApplyDamage(transform, new Vector2(0, 0), 1);
        }
    }
}
