using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip boitataDeathClip;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayDeath()
    {
		source.volume = GameController.masterVolume * GameController.effectsVolume;
		source.PlayOneShot(boitataDeathClip);
    }
}
