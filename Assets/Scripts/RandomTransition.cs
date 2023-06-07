using UnityEngine;

public class RandomTransition : StateMachineBehaviour {
    [SerializeField] private int _numberOfStates;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetInteger("Random", Random.Range(0, _numberOfStates));
    }
}
