using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayMakerFSM))]
public class StateController : MonoBehaviour
{

    [Tooltip("状态ID 最好和当前游戏物体名字挂钩")]
    /// <summary>
    /// ID
    /// </summary>
    public string ID;
    private FSMMgr playfsm;
    public FSMMgr playerFsm { set { playfsm = value; }
        get {
            if (playfsm == null)
                playfsm = new FSMMgr(gameObject);
            return playfsm;
        }
    }
    private PlayMakerFSM pmf;
    public PlayMakerFSM fsm { set { pmf = value; }
        get { if (pmf == null)
                pmf = GetComponent<PlayMakerFSM>();
            return pmf;
        }
    }

    #region Life_Recycle
    private void Awake()
    {
        OnAwake();
    }

    public virtual void OnAwake()
    {
      
       
    }

    // Use this for initialization
    void Start()
    {
        RegisterStates();
        OnStart();
    }

    public virtual void OnStart()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnUpdate();

        if (fsm != null)
        {
            if (playerFsm.GetState(fsm.ActiveStateName) != null)
            {
                CurrentFsmState = fsm.ActiveStateName;
            }

        }

        OnUpdate();
    }


    public virtual void OnUpdate()
    {

    }


    private void FixedUpdate()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnFixedUpdate();

        OnFixedUpdate();
    }

    public virtual void OnFixedUpdate()
    {

    }


    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnLatedUpdate();

        OnLateUpdate();
    }

    public virtual void OnLateUpdate()
    {

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnTriggerEnter(other);
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnTriggerStay(other);
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnTriggerExit(other);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnCollisionEnter(other);
    }

    private void OnCollisionExit(Collision other)
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnCollisionExit(other);
    }

    private void OnCollisionStay(Collision other)
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnCollisionStay(other);
    }


    private void OnEnable()
    {
        if(playerFsm != null)
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnEnable();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnDisable();
    }

    private void OnBecameInvisible()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnBecameInvisible();
    }

    /// <summary>
    /// OnBecameVisible is called when the renderer became visible by any camera.
    /// </summary>
    void OnBecameVisible()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnBecameVisible();
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnDestroy();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnApplicationPause(pauseStatus);
    }

    private void OnTransformChildrenChanged()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnTransformChildrenChanged();
    }
    private void OnTransformParentChanged()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnTransformParentChanged();
    }

    #endregion

    private string currentFsmState;

    public string CurrentFsmState
    {
        get
        {
            return currentFsmState;
        }

        set
        {
            if (currentFsmState != value)
            {
                playerFsm.MoveState(value);
            }

            currentFsmState = value;
        }
    }

    /// <summary>
    /// 连接底层与LUA层的脚本桥梁 
    /// </summary>
    protected void RegisterStates()
    {
 
        ///设置根节点状态信息
       
        playerFsm.SetRoot(new LuaState(playerFsm, ID, fsm.ActiveStateName));
        Debug.Log("注册状态" + fsm.FsmStates.Length + " " + fsm.gameObject.name);
        for (int i = 0; i < fsm.FsmStates.Length; i++)
        {
            ///抛出根节点状态信息
            if (fsm.FsmStates[i].Name.Equals(fsm.ActiveStateName))
            {
                continue;
            }
            
            playerFsm.RegisterState(new LuaState(playerFsm, ID, fsm.FsmStates[i].Name));
        }
       
        //Debug.Log("根节点" + skillChainGroupId + playerFsm.RootState.Name);
    }


}
