using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Coin : MonoBehaviour
{

    [SerializeField] private AudioClip pickupClip;

    private void Start()
    {
        transform.parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), Random.Range(3f, 5f)), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource source = Player.PropertiesCore.Player.GetComponent<AudioSource>();
		source.volume = GameController.masterVolume * GameController.effectsVolume;
		source.PlayOneShot(pickupClip);
        GameController.GetCoin(1, transform.parent.gameObject);
    }
}
