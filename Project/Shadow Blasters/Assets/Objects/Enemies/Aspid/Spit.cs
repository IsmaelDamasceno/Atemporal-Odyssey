using CrystalBot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit: MonoBehaviour
{

    [SerializeField] private GameObject projectile;

    public void Attack()
    {
        GameObject gameObject = Instantiate(projectile, transform.GetComponentInChildren<ProjectileOffset>().transform.position, Quaternion.identity);
        gameObject.GetComponent<Projectile>().direction = GetComponent<EnemyBehaviour>().Direction;
    }
}
