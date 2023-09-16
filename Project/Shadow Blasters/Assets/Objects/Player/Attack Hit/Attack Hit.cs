using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(-15f, 15f));
    }

    void Update()
    {
        
    }
}
