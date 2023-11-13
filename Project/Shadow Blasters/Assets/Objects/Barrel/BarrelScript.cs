using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour, IDamage
{

    public GameObject destroyParticles;
    public GameObject pieces;
    public GameObject coinObj;

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
