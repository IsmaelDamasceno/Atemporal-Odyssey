
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
        [HideInInspector] public EnemyFeet Feet;

        public static float s_StunTime = .25f;
        public static bool s_Stunned = false;

        void Awake()
        {
			_behaviour = GetComponent<EnemyBehaviour>();
			_rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
			/*
             if (Feet.OnFloor && !_behaviour.enabled && _rb.velocity.y <= 0.05f)
            {
				_behaviour.enabled = true;
				WallDetection.enabled = true;
				GroundDetection.enabled = true;
			}
             */
		}

		public void ApplyForce(Vector2 forceToApply)
        {
			_behaviour.enabled = false;
			WallDetection.enabled = false;
			GroundDetection.enabled = false;

			/*
             transform.position += Vector3.up * 0.1f;
            _rb.velocity = Vector2.zero;
            _rb.AddForce(forceToApply, ForceMode2D.Impulse);
             */
		}
        private IEnumerator StunCoroutine()
        {
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerAttack"), LayerMask.NameToLayer("EnemySolid"), true);
			s_Stunned = true;

			yield return new WaitForSeconds(s_StunTime);

			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerAttack"), LayerMask.NameToLayer("EnemySolid"), false);
			s_Stunned = false;

			_behaviour.enabled = true;
			WallDetection.enabled = true;
			GroundDetection.enabled = true;
		}

        public void ApplyDamage(Vector2 impact, int amount)
        {
			_behaviour.enabled = false;
			WallDetection.enabled = false;
			GroundDetection.enabled = false;
			_rb.AddForce(impact, ForceMode2D.Impulse);

            StartCoroutine(StunCoroutine());
		}
    }
}
