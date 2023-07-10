using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float _damage;

    private SpriteRenderer _sprRenderer;
    private Animator _selfAnimator;
    private Animator _playerAnimator;

    void Start()
    {
        _sprRenderer = GetComponent<SpriteRenderer>();
        _selfAnimator = GetComponent<Animator>();
        _playerAnimator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Atacar();
        }
    }

    public void Atacar() {
        _sprRenderer.enabled = true;
        _selfAnimator.Play("Base Layer.Attack Anim");
    }
}
