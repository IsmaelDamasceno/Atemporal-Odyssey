using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed;
    public int direction;

    private void Awake()
    {
        StartCoroutine(DestroyCoroutine());
	}

    void Update()
    {
        transform.Translate(new Vector3(speed * direction * Time.deltaTime, 0f, 0f));
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
			Player.DamageMember.s_Instance.ApplyDamage(transform, new Vector2(5f, 6f), 1);
            Destroy(gameObject);
		}
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
			Destroy(gameObject);
		}
    }
}
