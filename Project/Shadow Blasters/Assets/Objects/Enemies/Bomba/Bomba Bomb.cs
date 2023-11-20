using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaBomb : MonoBehaviour
{

    [SerializeField] private float explodeTime;

    void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explodeTime);
        Destroy(gameObject);
    }
}
