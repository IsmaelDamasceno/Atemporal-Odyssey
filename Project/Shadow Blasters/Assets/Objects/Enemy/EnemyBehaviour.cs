using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [HideInInspector] public bool OnFloor;
    public int Direction;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float yVel = _rb.velocity.y;
        _rb.velocity = new Vector2(Direction * _moveSpeed, yVel);
    }
}
