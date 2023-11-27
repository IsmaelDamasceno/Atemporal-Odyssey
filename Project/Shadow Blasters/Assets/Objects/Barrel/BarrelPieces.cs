using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPieces : MonoBehaviour
{

    [SerializeField] private float pushForce;
    [SerializeField] private float torqueForce;

    public AudioClip breakSound;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
		source.volume = GameController.masterVolume * GameController.effectsVolume;
		source.PlayOneShot(breakSound);

        float maxDistance = 0f;
        foreach(Transform childTrs in transform)
        {
            if (Mathf.Abs(childTrs.localPosition.x) > maxDistance)
            {
                maxDistance = Mathf.Abs(childTrs.localPosition.x);
            }
        }
        maxDistance += 0.05f;

        foreach(Transform childTrs in transform)
        {
            float forcePercent = Math.Sign(childTrs.localPosition.x) * (1f - Mathf.Abs(childTrs.localPosition.x) / maxDistance);
            childTrs.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(forcePercent * pushForce, forcePercent * pushForce), ForceMode2D.Impulse);
            childTrs.GetComponent<Rigidbody2D>().AddTorque(-forcePercent * torqueForce, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        
    }
}
