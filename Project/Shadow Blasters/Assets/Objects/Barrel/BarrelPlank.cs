using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPlank : MonoBehaviour
{

    [SerializeField] private Vector2 destroyTime;

    void Start()
    {
        StartCoroutine(DestroyCoroutine(Random.Range(destroyTime.x, destroyTime.y)));
    }

    IEnumerator DestroyCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
