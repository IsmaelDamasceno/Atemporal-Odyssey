using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAttack : StateMachineBehaviour
{
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// Esconde o GameObject Attack
		SpriteRenderer renderer = animator.GetComponent<SpriteRenderer>();
		renderer.enabled = false;

		// Diz que o jogador não está mais atacando
		Player.AttackMember attack = animator.GetComponent<Player.AttackMember>();
		attack.SetAttacking(false, typeof(FinishAttack));
		AttackMember attackMember = animator.GetComponent<AttackMember>();
		attackMember.Attacking = false;
		attackMember.StartCoroutine(attackMember.CooldownWait());

		Animator playerAnimator = animator.transform.parent.GetComponent<Animator>();
        playerAnimator.SetBool("Attacking", false);
    }
	
}
