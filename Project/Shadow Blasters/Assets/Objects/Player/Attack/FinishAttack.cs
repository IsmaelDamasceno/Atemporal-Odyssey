using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAttack : StateMachineBehaviour
{
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// Makes the Attack Game Object invisible
		SpriteRenderer renderer = animator.GetComponent<SpriteRenderer>();
		renderer.enabled = false;

		// Set Attacking to false
		Player.AttackMember attack = animator.GetComponent<Player.AttackMember>();
		attack.SetAttacking(false, typeof(FinishAttack));
	}
	
}
