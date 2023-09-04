
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class DamageMember : MonoBehaviour
    {
        private MoveMember _moveMember;
        private JumpMember _jumpMember;
        private Rigidbody2D _rb;

        public static float s_IvulnerableTime = 1.25f;
        public static float s_ImpactUncontrolTime = 0.25f;
        public static bool s_Ivulnerable = false;

        void Awake()
        {
            _moveMember = GetComponent<MoveMember>();
            _jumpMember = GetComponent<JumpMember>();
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (!_moveMember.enabled)
            {
                if (_jumpMember.OnFloor())
                {
                    _moveMember.enabled = true;
                    _jumpMember.JumpControl = true;
                }
            }
        }

        public void ApplyForce(Vector2 forceToApply)
        {
            _moveMember.enabled = false;
            _jumpMember.JumpControl = false;

            transform.position += Vector3.up * 0.1f;
            _rb.velocity = Vector2.zero;
            _rb.AddForce(forceToApply, ForceMode2D.Impulse);

            StartCoroutine(ImpactCoroutine());
        }

        public void SetIvulnerable()
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            s_Ivulnerable = true;
            StartCoroutine(VulnerabilityCoroutine());
        }

        private IEnumerator VulnerabilityCoroutine()
        {
            yield return new WaitForSeconds(s_IvulnerableTime);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
            s_Ivulnerable = false;
        }

        private IEnumerator ImpactCoroutine()
        {
            yield return new WaitForSeconds(s_ImpactUncontrolTime);
            _moveMember.enabled = true;
            _jumpMember.JumpControl = true;
        }
    }
}
