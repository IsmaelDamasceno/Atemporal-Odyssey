using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageParticles : MonoBehaviour
{

    private ParticleSystem partSystem;

    void Awake()
    {
        partSystem = GetComponent<ParticleSystem>();
        partSystem.Play();
    }

    void Update()
    {
        if (!partSystem.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
