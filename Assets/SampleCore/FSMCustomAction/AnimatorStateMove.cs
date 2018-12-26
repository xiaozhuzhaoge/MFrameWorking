using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory(ActionCategory.StateMachine)]
    public class AnimatorStateMove: GetAnimatorCurrentStateInfo
    {
        private FSMMgr smm;
   
        public FsmString MoveState;
        public FsmFloat CompareValue;

        [Tooltip("The name of the state that will be played.")]
        public FsmString AnimationState;

        [Tooltip("The duration of the transition. Value is in source state normalized time.")]
        public FsmFloat transitionDuration;

        [Tooltip("Layer index containing the destination state. Leave to none to ignore")]
        public FsmInt layer;

        private Animator _animator;


        public override void Reset()
        {
            gameObject = null;
            smm = null;
        }

        public override void OnEnter()
        {
            // get the animator component
            var go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (go == null)
            {
                Finish();
                return;
            }

            _animator = go.GetComponent<Animator>();


            if (_animator != null)
            {
                int _layer = layer.IsNone ? -1 : layer.Value;

                float _normalizedTime = normalizedTime.IsNone ? Mathf.NegativeInfinity : normalizedTime.Value;

                _animator.CrossFade(AnimationState.Value, transitionDuration.Value, _layer, _normalizedTime);
            }


            if (_animator == null)
            {
                Finish();
                return;
            }

            GetLayerInfo();

            if (!everyFrame)
            {
                Finish();
            }
        
        }

        public override void OnActionUpdate()
        {
            GetLayerInfo();
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= CompareValue.Value)
            {
                Fsm.SetState(MoveState.Value);
                Debug.Log("????" + MoveState.Value);
                Finish();
            }
        }

        

    }
 
   
}


