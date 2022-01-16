using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviourEnProceso : StateMachineBehaviour
{
    public string nombreParametro;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(nombreParametro, true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(nombreParametro, false);
    }

}
