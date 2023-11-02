using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour, IDamage
{

    public GameObject destroyParticles;
    public GameObject pieces;

    public void ApplyDamage(Transform hitTransform, Vector2 impact, int amount)
    {
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
		Instantiate(pieces, transform.position, Quaternion.identity);
		Destroy(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
