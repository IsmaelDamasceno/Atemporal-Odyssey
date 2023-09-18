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

        public float stunTime = .25f;
        public bool stunned = false;

		private BasePropertiesCore propertiesCore;

        void Awake()
        {
			_behaviour = GetComponent<EnemyBehaviour>();
			_rb = GetComponent<Rigidbody2D>();
			propertiesCore = GetComponent<BasePropertiesCore>();
        }

        private IEnumerator StunCoroutine()
        {
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerAttack"), LayerMask.NameToLayer("EnemySolid"), true);
			stunned = true;

			yield return new WaitForSeconds(stunTime);

			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerAttack"), LayerMask.NameToLayer("EnemySolid"), false);
			stunned = false;

			propertiesCore.ChangeState(EnemyState.Patrol);
		}

		public void ApplyDamage(Transform hitTransform, Vector2 impact, int amount)
		{
			if (!stunned)
            {
				propertiesCore.health -= amount;
				if (propertiesCore.health <= 0)
				{
					Destroy(gameObject);
				}

				propertiesCore.ChangeState(EnemyState.Damage);
				_rb.AddForce(impact, ForceMode2D.Impulse);

				StartCoroutine(StunCoroutine());

				gameObject.AddComponent<FlashWhite>().Init(stunTime, stunTime, GetComponent<SpriteRenderer>());

				int l = Random.Range(1, 4);
				for (int i = 0; i < l; i++)
				{
					Instantiate(Resources.Load<GameObject>("Attack Hit"), transform.position, Quaternion.identity);
				}
			}
		}
    }
}
