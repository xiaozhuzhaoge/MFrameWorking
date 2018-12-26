using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    public class AnimationPlayedEventAction : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Animator))]
        [Tooltip("The target.")]
        public FsmOwnerDefault gameObject;
        [RequiredField]
        public FsmString ReturnStateName;
        public FsmFloat NormalizedTime;
        

        UnityEngine.Animator _animator;
        string rootAnimationStateName;

        public override void Reset()
        {
            gameObject = null;
            _animator = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            _animator = go.GetComponent<Animator>();

            for (int i = 0; i < Fsm.ActiveState.Actions.Length; i++)
            {
                if (Fsm.ActiveState.Actions[i] as AnimatorCrossFade != null)
                {
                    AnimatorCrossFade data = Fsm.ActiveState.Actions[i] as AnimatorCrossFade;
                    rootAnimationStateName = data.stateName.Value;
                }
            }

            
        }


        public override void OnUpdate()
        {
            base.OnUpdate();
            AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
            
            if (info.normalizedTime >= NormalizedTime.Value 
                && info.IsName(rootAnimationStateName))
            {
                Fsm.SetState(ReturnStateName.Value);
                Finish();

            }
        }
    }

}