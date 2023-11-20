using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] private float explosionTime;
    [SerializeField] private float explosionIncrease;


    void Start()
    {
        StartCoroutine(Explode());
    }

    void Update()
    {
        float increase = explosionIncrease * Time.deltaTime;
        transform.localScale += new Vector3(increase, increase, increase);
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionTime);

        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
    }
#endif
}
