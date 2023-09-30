using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideAttackSetup : MonoBehaviour
{

    [SerializeField] private Transform leftSideTrs;
    [SerializeField] private Transform rightSideTrs;
    [SerializeField] private GameObject controller;

    void Start()
    {
        int side = Random.Range(0, 2);
        if (side == 0)
        {
            Instantiate(controller, leftSideTrs.position, Quaternion.identity);
        }
        else
        {
			GameObject instance = Instantiate(controller, rightSideTrs.position, Quaternion.identity);
            instance.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
