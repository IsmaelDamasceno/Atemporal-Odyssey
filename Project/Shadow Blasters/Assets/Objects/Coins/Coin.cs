using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        transform.parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), Random.Range(3f, 5f)), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.Score++;
        Debug.Log(GameController.Score);
        Destroy(transform.parent.gameObject);
    }
}
