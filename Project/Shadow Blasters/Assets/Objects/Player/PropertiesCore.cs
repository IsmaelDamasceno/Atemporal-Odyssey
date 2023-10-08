using System.Collections;
using UnityEngine;

public enum PlayerState
{
    Free,
	Dash,
    Damage,
    Attack,
	Swim,
    SwimFree,
    Ladder,
	Interacting,
}

namespace Player
{
	/// <summary>
	/// Guarda referência de Components importados do jogador (rigidbody, colliders, etc)
	/// </summary>
	public class PropertiesCore : BaseMember
	{
		[HideInInspector] public Rigidbody2D Rigidbody;
		[HideInInspector] public BoxCollider2D Collider;
		[HideInInspector] public BoxCollider2D FeetCollider;

        public PlayerState currentState;
		private MoveMember moveMember;
		private DamageMember damageMember;
		private LadderMember ladderMember;
		private JumpMember jumpMember;
		private DashMember dashMember;
		private AttackMember attackMember;
		private InteractionMember interactionMember;

        [HideInInspector] public static bool swimming = false;
		[HideInInspector] public static bool swimJump = false;
		//[HideInInspector] public static bool ladder = false;
        [HideInInspector] public bool Attacking = false;

        public static GameObject Player;
		public static PropertiesCore s_Instance;
		public static AudioPlayer audioPlayer;

		private static Animator animator;
		private static InputMember inputMember;

		public static bool canInteract = true;

		void Awake()
		{
			if (Player == null)
			{
				Player = gameObject;
				s_Instance = this;
				canInteract = true;

				Rigidbody = GetComponent<Rigidbody2D>();
				Collider = GetComponent<BoxCollider2D>();
				FeetCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
				animator = GetComponent<Animator>();
				inputMember = GetComponent<InputMember>();
				audioPlayer = GetComponent<AudioPlayer>();
				moveMember = GetComponent<MoveMember>();
				damageMember = GetComponent<DamageMember>();
				ladderMember = GetComponent<LadderMember>();
				jumpMember = GetComponent<JumpMember>();
				dashMember = GetComponent<DashMember>();
				attackMember = GetComponent<AttackMember>();
				interactionMember = GetComponent<InteractionMember>();

				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemySolid"), true);

				DontDestroyOnLoad(gameObject);

				swimming = false;
				swimJump = false;
				Attacking = false;

				GameController.SavePlayerInitialPos(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private IEnumerator SwimJumpCoroutine()
		{
			swimJump = false;
			yield return new WaitForSeconds(.25f);
			if (swimming)
			{
				swimJump = true;
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
			{
                s_Instance.ChangeState(PlayerState.Swim);
                animator.SetBool("Swimming", true);
                animator.Play("Base Layer.Swim");

                StartCoroutine(CoroutineUtils.DelaySeconds(
                    () => {
						if (currentState == PlayerState.Swim)
						{
                            s_Instance.ChangeState(PlayerState.SwimFree);
                        }
                    },
                .25f));
            }
		}
		private void OnTriggerStay2D(Collider2D collision)
		{
			if (currentState != PlayerState.Ladder && collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
			{
				if (inputMember.LadderInput > 0f || (inputMember.LadderInput < 0f && JumpMember.grounded))
				{
					EnterLadder(collision);
				}
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
			{
				animator.SetBool("Swimming", false);
				swimming = false;
				swimJump = false;
			}
			else if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
			{
				ExitLadder();
			}
		}

        public void ChangeState(PlayerState newState)
        {
            currentState = newState;

            switch (currentState)
            {
                case PlayerState.Free:
                    {
                        moveMember.enabled = true;
						jumpMember.JumpControl = true;
						ladderMember.enabled = false;
                    }
                    break;
                case PlayerState.Damage:
                    {
                        moveMember.enabled = false;
						jumpMember.JumpControl = false;
                    }
                    break;
				case PlayerState.Dash:
					{
						moveMember.enabled = false;
					}
					break;
				case PlayerState.Attack:
					{

					}
					break;
				case PlayerState.Swim:
					{
						jumpMember.enabled = false;
					}
					break;
				case PlayerState.SwimFree:
					{
                        jumpMember.enabled = true;
                    }
                    break;
				case PlayerState.Interacting:
					{

					}
					break;
				case PlayerState.Ladder:
					{
						moveMember.enabled = false;
						ladderMember.enabled = true;
					}
					break;
            }
        }

		public static bool CanJump()
		{
			return s_Instance.currentState == PlayerState.SwimFree || 
				s_Instance.currentState == PlayerState.Ladder;
		}

        public static void EnterLadder(Collider2D collision)
		{
			s_Instance.ChangeState(PlayerState.Ladder);
            s_Instance.Collider.excludeLayers = (int)Mathf.Pow(2, LayerMask.NameToLayer("Ground")) +
                (int)Mathf.Pow(2, LayerMask.NameToLayer("WoodFloor"));
            s_Instance.ladderMember.StartLadder(new Vector2(collision.transform.position.x, collision.ClosestPoint(s_Instance.transform.position).y));
        }
        public static void ExitLadder()
		{
			s_Instance.ladderMember.ExitLadder();

            s_Instance.ChangeState(PlayerState.Free);
            s_Instance.Collider.excludeLayers = 0;
		}
	}
}
