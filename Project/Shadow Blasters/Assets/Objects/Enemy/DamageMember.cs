
using Player;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class DamageMember : MonoBehaviour, IDamage
    {
        private Rigidbody2D _rb;
        private EnemyBehaviour _behaviour;
        [HideInInspector] public WallDetection WallDetection;
		[HideInInspector] public GroundDetection GroundDetection;

        public static float s_IvulnerableTime = 1.25f;
        public static float s_ImpactUncontrolTime = 0.25f;
        public static bool s_Ivulnerable = false;

        void Awake()
        {
			_behaviour = GetComponent<EnemyBehaviour>();
			_rb = GetComponent<Rigidbody2D>();
        }

        public void ApplyForce(Vector2 forceToApply)
        {
			_behaviour.enabled = false;
			WallDetection.enabled = false;
			GroundDetection.enabled = false;

            transform.position += Vector3.up * 0.1f;
            _rb.velocity = Vector2.zero;
            _rb.AddForce(forceToApply, ForceMode2D.Impulse);

            StartCoroutine(ImpactCoroutine());
        }

        public void SetIvulnerable()
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerAttack"), LayerMask.NameToLayer("EnemySolid"), true);
            s_Ivulnerable = true;
            StartCoroutine(VulnerabilityCoroutine());
        }

        private IEnumerator VulnerabilityCoroutine()
        {
            yield return new WaitForSeconds(s_IvulnerableTime);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerAttack"), LayerMask.NameToLayer("EnemySolid"), false);
            s_Ivulnerable = false;
        }

        private IEnumerator ImpactCoroutine()
        {
            yield return new WaitForSeconds(s_ImpactUncontrolTime);
			_behaviour.enabled = true;
			WallDetection.enabled = true;
			GroundDetection.enabled = true;
		}

        public void ApplyDamage(Vector2 impact, int amount)
        {
			ApplyForce(impact);
			SetIvulnerable();
		}
    }
}
