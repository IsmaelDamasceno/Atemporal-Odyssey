using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnd : MonoBehaviour
{

    private ParticleSystem partSystem;

    void Start()
    {
        partSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!partSystem.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
