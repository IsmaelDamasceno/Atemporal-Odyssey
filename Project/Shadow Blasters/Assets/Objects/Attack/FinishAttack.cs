using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAttack : StateMachineBehaviour
{
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		SpriteRenderer renderer = animator.GetComponent<SpriteRenderer>();
		renderer.enabled = false;
	}
}
