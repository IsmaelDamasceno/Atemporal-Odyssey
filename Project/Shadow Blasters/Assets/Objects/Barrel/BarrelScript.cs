using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarrelScript : MonoBehaviour, IDamage
{

    public GameObject destroyParticles;
    public GameObject pieces;
    public GameObject coinObj;

    private Rigidbody2D rb;
    private CauseDamage causeDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        causeDamage = GetComponent<CauseDamage>();
    }

    void FixedUpdate()
    {
        causeDamage.active = rb.velocity.y <= -6.5f;
    }

    public void ApplyDamage(Transform hitTransform, Vector2 impact, int amount)
    {
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
		Instantiate(pieces, transform.position, Quaternion.identity);

        for(int i = 0; i <= Random.Range(0, 1); i++)
        {
            Rigidbody2D gameObjRb = Instantiate(coinObj, transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f), Quaternion.identity).GetComponent<Rigidbody2D>();
        }

		Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (rb == null)
        {
            return;
        }

        Gizmos.color = Color.white;

        Handles.Label(transform.position + Vector3.up, $"y speed: {rb.velocity.y}");
    }
#endif
}
