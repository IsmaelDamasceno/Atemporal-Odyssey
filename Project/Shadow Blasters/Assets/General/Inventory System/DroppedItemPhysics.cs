using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemPhysics : MonoBehaviour
{
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7);
		Physics2D.IgnoreLayerCollision(6, 6);
	}

    void Start()
    {
		float angle = 90f + Random.Range(-20f, 20f);
        float strenght = Random.Range(6f, 10f);
        Vector2 force = new(strenght * Mathf.Cos(angle * Mathf.Deg2Rad), strenght * Mathf.Sin(angle * Mathf.Deg2Rad));
        _rb.velocity = force;
	}

    void Update()
    {
        if (_rb.velocity.sqrMagnitude <= 0.2236f && !_rb.isKinematic)
        {
            StartCoroutine(StopPhysics());
        }
    }

    private IEnumerator StopPhysics()
    {
        yield return new WaitForSeconds(Random.Range(0.35f, 1.3f));
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
    }
}
