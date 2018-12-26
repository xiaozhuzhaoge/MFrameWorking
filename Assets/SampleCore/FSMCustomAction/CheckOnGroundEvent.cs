using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    public class CheckOnGroundEvent : FsmStateAction
    {

        [RequiredField]
        [CheckForComponent(typeof(Animator))]
        [Tooltip("The target.")]
        public FsmOwnerDefault gameObject;
        [RequiredField]
       
        public FsmString[] ReturnStateNames;
        public FsmFloat heightCompare;
        public FsmFloat NormalizedTime;
        PlayerCtr _cc;

        UnityEngine.Animator _animator;
        string rootAnimationStateName;

        public override void Reset()
        {
            gameObject = null;
            _animator = null;
        }

        float startTime;

        public override void OnEnter()
        {
            base.OnEnter();
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            _animator = go.GetComponent<Animator>();
            _cc = go.GetComponent<PlayerCtr>();
            startTime = Time.time;
        }



        public override void OnUpdate()
        {
            _cc.cc.SimpleMove(Vector3.down);
            
            if (_cc.RayCastGround)
            {
                if(ReturnStateNames.Length == 1)
                {
                    Fsm.SetState(ReturnStateNames[0].Value);
                }
                else if(ReturnStateNames.Length == 2)
                {
                    if (Time.time - startTime > heightCompare.Value)
                    {

                        Fsm.SetState(ReturnStateNames[1].Value);
                    }
                    else
                    {
                        Fsm.SetState(ReturnStateNames[0].Value);
                    }
                
                }
           
            }
        }
    }

}