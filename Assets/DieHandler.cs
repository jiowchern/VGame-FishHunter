using UnityEngine;
using System.Collections;

public class DieHandler : StateMachineBehaviour {


    Regulus.Utility.TimeCounter _Counter;
    public delegate void DoneCallback();
    public event DoneCallback DoneEvent;

    public DieHandler()
    {
        _Counter = new Regulus.Utility.TimeCounter();
    }

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _Counter.Reset();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    if(_Counter.Second >= stateInfo.length)
        {
            if (DoneEvent != null)
                DoneEvent();

            DoneEvent = null;
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
	}

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
