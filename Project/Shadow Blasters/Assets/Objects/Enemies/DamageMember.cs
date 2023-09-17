using System.Collections;
using UnityEngine;

namespace CrystalBot
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

		private IPropertiesCore propertiesCore;

        void Awake()
        {
			_behaviour = GetComponent<EnemyBehaviour>();
			_rb = GetComponent<Rigidbody2D>();
			propertiesCore = GetComponent<IPropertiesCore>();
        }

        private IEnumerator StunCoroutine()
        {
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerAttack"), LayerMask.NameToLayer("EnemySolid"), true);
			s_Stunned = true;

			yield return new WaitForSeconds(s_StunTime);

			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerAttack"), LayerMask.NameToLayer("EnemySolid"), false);
			s_Stunned = false;

			propertiesCore.ChangeState(EnemyState.Patrol);
		}

		public void ApplyDamage(Transform hitTransform, Vector2 impact, int amount)
		{
			if (!s_Stunned)
            {
				propertiesCore.ChangeState(EnemyState.Damage);
				_rb.AddForce(impact, ForceMode2D.Impulse);

				StartCoroutine(StunCoroutine());

				gameObject.AddComponent<FlashWhite>().Init(s_StunTime, s_StunTime, GetComponent<SpriteRenderer>());

				int l = Random.Range(1, 4);
				for (int i = 0; i < l; i++)
				{
					Instantiate(Resources.Load<GameObject>("Attack Hit"), transform.position, Quaternion.identity);
				}
			}
		}
    }
}
