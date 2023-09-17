using Aspid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSpit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PropertiesCore>().ChangeState(EnemyState.Patrol);
    }


}
