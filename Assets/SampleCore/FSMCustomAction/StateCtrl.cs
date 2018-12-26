using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory(ActionCategory.StateMachine)]
    public class StateCtrl: FsmStateAction
    {
        private FSMMgr smm;
        [RequiredField]
        private FsmOwnerDefault gameObject;

        public FsmString MoveState;

        public override void Reset()
        {
            gameObject = null;
            smm = null;
        }

        public override void OnEnter()
        {
            ///get self
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) {
                Finish();
                return;
            }
           
            smm.MoveState(MoveState.Value);
        }

    }
 
   
}


