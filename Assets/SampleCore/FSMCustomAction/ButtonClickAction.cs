using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    public class ButtonClickAction : FsmStateAction
    {
        [RequiredField]
        public FsmEvent[] events;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmFloat[] NormalizedTime;

        [RequiredField]
        public FsmOwnerDefault gameObject;
        
        float startTime;
        public FsmInt eventIndex;


        UnityEngine.Animator _animator;

        bool StartUpdate;

        public override void Reset()
        {
            gameObject = null;
            events = new FsmEvent[1];
            _animator = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            _animator = go.GetComponent<Animator>();
            eventIndex.Value = -1;
            for (int i = 0; i < events.Length; i++)
            {
                AttackMenu.Instance.flagCallback[i] -= OnButtonClick;
                AttackMenu.Instance.flagCallback[i] += OnButtonClick;
            }
        }

        public void OnButtonClick(int index,bool flag) {

            if (NormalizedTime == null || NormalizedTime.Length != events.Length)
            {
                Fsm.Event(events[index]);
                Finish();
                RemoveAllClickEvents();
                return;
            }

            if (NormalizedTime[index].Value < 0.001f)
            {
                Fsm.Event(events[index]);
                RemoveAllClickEvents();
                Finish();
            }
            else
            {
                eventIndex = index;
                StartUpdate = true;
                RemoveAllClickEvents();
            }
        
            return;
        }

        public void RemoveAllClickEvents()
        {
            for (int i = 0; i < events.Length; i++)
            {
                AttackMenu.Instance.flagCallback[i] -= OnButtonClick;
            }
        }

        public override void OnUpdate()
        {
            FixChangeAnimationBug();

            if (StartUpdate == false || eventIndex.Value == -1)
                return;

         
            if (NormalizedTime != null && NormalizedTime.Length == events.Length)
            {
                if (NormalizedTime[eventIndex.Value].Value != 0)
                {
                    for(int i = 0; i < Fsm.ActiveState.Actions.Length; i++)
                    {
                        if(Fsm.ActiveState.Actions[i] as AnimatorCrossFade != null)
                        {
                            AnimatorCrossFade data = Fsm.ActiveState.Actions[i] as AnimatorCrossFade;
                            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= NormalizedTime[eventIndex.Value].Value && 
                                _animator.GetCurrentAnimatorStateInfo(0).IsName(data.stateName.Value))
                            {
                                Fsm.Event(events[eventIndex.Value]);
                                Finish();
                            }
                            return;
                        }
                    }
                    
                }
            }
        }

        /// <summary>
        /// 由于状态跳转动画播放和状态机跳转之间会产生异步导致动画播放卡主，所以进行动画状态判断，如果当前状态动画名和播放不一样，重新播放一边
        /// </summary>
        public void FixChangeAnimationBug() {
            for (int i = 0; i < Fsm.ActiveState.Actions.Length; i++)
            {
                if (Fsm.ActiveState.Actions[i] as AnimatorCrossFade != null)
                {
                    AnimatorCrossFade data = Fsm.ActiveState.Actions[i] as AnimatorCrossFade;
                    ///如果当前播放的动画状态与状态内设置不一 强制跳转动画
                    if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(data.stateName.Value))
                    {
                        _animator.CrossFade(data.stateName.Value,0);
                    }
                }

            }
        }
    }
}
