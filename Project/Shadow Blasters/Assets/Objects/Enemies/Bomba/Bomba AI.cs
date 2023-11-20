using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BombaAI : MonoBehaviour
{

    [SerializeField] private float activateDistance;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private Vector2 bombForce;
    [SerializeField] private float bombTorque;
    [SerializeField] private GameObject bomb;

    private float curCooldown = 0f;
    private Transform spawnTrs;


    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        spawnTrs = transform.GetChild(0);
    }

    void Update()
    {
        if (curCooldown > 0f)
        {
            curCooldown -= Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, Player.PropertiesCore.Player.transform.position) <= activateDistance && curCooldown <= 0f)
        {
            curCooldown += spawnCooldown;
            Rigidbody2D objRb = Instantiate(bomb, spawnTrs.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            objRb.AddForce(Vector2.up * UnityEngine.Random.Range(3, 5), ForceMode2D.Impulse);
            objRb.AddTorque(UnityEngine.Random.Range(-bombTorque, bombTorque), ForceMode2D.Impulse);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, activateDistance);
    }
#endif
}
