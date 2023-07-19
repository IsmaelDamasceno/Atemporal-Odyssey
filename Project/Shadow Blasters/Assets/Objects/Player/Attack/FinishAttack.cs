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
		Player.AttackOrgan attack = animator.GetComponent<Player.AttackOrgan>();
		attack.SetAttacking(false, typeof(FinishAttack));
	}
	
}
