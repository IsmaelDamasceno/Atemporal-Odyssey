using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerUpEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
			Destroy(gameObject);
			powerUpEffect.Apply(Player.PropertiesCore.Player);
			BUffUIManager.CreateBuff(powerUpEffect.UIBuffItem);
		}
    }
}
