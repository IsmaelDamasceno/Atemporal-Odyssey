using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTargetAttackSetup : MonoBehaviour
{

    [SerializeField] private Transform spawnTrs;
    [SerializeField] private GameObject topAttackControllerPrefab;

    void Start()
    {
        Instantiate(topAttackControllerPrefab, spawnTrs.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
