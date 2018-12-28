using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DoEvetHandler(string stateName);

public class CharacterCtrlBase : MonoBehaviour
{

    /// <summary>
    /// 技能组ID
    /// </summary>
    public int skillChainGroupId;
    public CharacterController cc;
    public float speed;
    public Animator ani;
    public FSMMgr playerFsm;
    public float rotationSpeed;
    public PlayMakerFSM fsm;
    public string SelfWeaponLayer;
    public float CurrentNormalizedTime { get { return fsm.FsmVariables.GetFsmFloat("CurrentNormalizedTime").Value; } }

    #region Life_Recycle
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        fsm = GetComponent<PlayMakerFSM>();
        playerFsm = new FSMMgr(gameObject);
        OnAwake();
    }

    public virtual void OnAwake(){

    }

    // Use this for initialization
    void Start()
    {
        RegisterStates();
        OnStart();
    }

    public virtual void OnStart(){

    }

    // Update is called once per frame
    void Update()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnUpdate();

        if (fsm != null && playerFsm != null)
            if (playerFsm.GetState(fsm.ActiveStateName) != null)
                CurrentFsmState = fsm.ActiveStateName;

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

    /// <summary>
    /// Callback for processing animation movements for modifying root motion.
    /// </summary>
    void OnAnimatorMove()
    {
        if (playerFsm.CurrentState != null)
            playerFsm.CurrentState.OnAnimatorMove();
    }

    private void OnEnable()
    {
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
                Debug.Log("移动状态机" + value);
                playerFsm.MoveState(value);
            }

            currentFsmState = value;
        }
    }

    /// <summary>
    /// 连接底层与LUA层的脚本桥梁 并且根据数据表动态匹配伤害逻辑
    /// </summary>
    void RegisterStates()
    {

        ///注册动画帧事件
        List<SkillChainConfig> SkillChains = SkillChainConfig.GetSkillChains(skillChainGroupId);

        for (int i = 0; i < SkillChains.Count; i++)
        {
            Utility.RigisterAnimationEvent(ani, SkillChains[i].animationStateName, SkillChains[i].functionName, SkillChains[i].doPercent, SkillChains[i].instruction);
        }

        ///设置根节点状态信息
        playerFsm.SetRoot(new SkillState(playerFsm, fsm.ActiveStateName));

        for (int i = 0; i < fsm.FsmStates.Length; i++)
        {
            ///抛出根节点状态信息
            if (fsm.FsmStates[i].Name.Equals(fsm.ActiveStateName))
            {
                continue;
            }
            playerFsm.RegisterState(new SkillState(playerFsm, fsm.FsmStates[i].Name));
        }

        Debug.Log("根节点" + playerFsm.RootState.Name);
    }

    #region 技能脚本

    public virtual void CastEffect(string value)
    {

        string[] values = value.Split('|');
        string type = values[0];
        switch (type)
        {
            case "ShowEffect":
                CreateEffect(Utility.GetPosV3(values[1]), Utility.GetPosV3(values[2]), Convert.ToSingle(values[3]), values[4], Convert.ToSingle(values[5]));
                break;
            case "Radius":
                RadiusCastHit(Convert.ToSingle(values[1]), Convert.ToSingle(values[2]), Convert.ToSingle(values[3]));
                break;
            case "RadiusPenetrated":
                RadiusCastHitAll(Convert.ToSingle(values[1]), Convert.ToSingle(values[2]), Convert.ToSingle(values[3]));
                break;
            case "SpherePenetrated":
                CastSphereRayDirectionAll(Utility.GetPosV3(values[1]), Convert.ToSingle(values[2]), Utility.GetPosV3(values[3]), Convert.ToSingle(values[4]));
                break;
            case "Sphere":
                CastSphereRayDirection(Utility.GetPosV3(values[1]), Convert.ToSingle(values[2]), Utility.GetPosV3(values[3]), Convert.ToSingle(values[4]));
                break;
            case "Box":
                break;
            case "Line":
                break;
            case "ThrowBox":
                ThrowBox(Utility.GetPosV3(values[1]), Utility.GetPosV3(values[2]), Convert.ToSingle(values[3]), values[4], Convert.ToSingle(values[5]), Convert.ToSingle(values[6]));
                break;
            case "ThrowBoxPenetrated":
                ThrowBoxAll(Utility.GetPosV3(values[1]), Utility.GetPosV3(values[2]), Convert.ToSingle(values[3]), values[4], Convert.ToSingle(values[5]), Convert.ToSingle(values[6]));
                break;
            case "CreateEffect":
                CreateEffect(Utility.GetPosV3(values[1]), Utility.GetPosV3(values[2]), Convert.ToSingle(values[3]), values[4], Convert.ToSingle(values[5]));
                break;
        }


    }

    /// <summary>
    /// 本次受击对象
    /// </summary>
    public Queue<IHitAnalysis> BeHits = new Queue<IHitAnalysis>();

    public bool HitEnemey(RaycastHit hit, Action callback = null)
    {

        IHitAnalysis beHit = hit.collider.GetComponent<IHitAnalysis>();
        if (!BeHits.Contains(beHit))
        {
            BeHits.Enqueue(beHit);
            beHit.BeHit(hit.point);
            FreezeFrame(10);
            if (callback != null)
            {
                callback();
            }
            return true;
        }
        return false;
    }


    #region UseRay
    /// <summary>
    /// 扇形检测范围
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="lookAccurate"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public bool RadiusCastHit(float angle, float lookAccurate, float range)
    {
        ///清除缓存
        BeHits.Clear();
        RaycastHit hit;
        float subAngle = (angle / 2) / lookAccurate;
        bool isHit = false;

        for (int i = 0; i < lookAccurate; i++)
        {
            if (CastRadiusRay(Quaternion.Euler(0, -1 * subAngle * (i + 1), 0), range, out hit, Color.red))
            {
                HitEnemey(hit, () => { isHit = true; });
            }
            if (CastRadiusRay(Quaternion.Euler(0, 1 * subAngle * (i + 1), 0), range, out hit, Color.red))
            {
                HitEnemey(hit, () => { isHit = true; });
            }
        }
        return isHit;
    }
    /// <summary>
    /// 扇形检测范围穿透
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="lookAccurate"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public bool RadiusCastHitAll(float angle, float lookAccurate, float range)
    {
        ///清除缓存
        BeHits.Clear();
        RaycastHit hit;
        float subAngle = (angle / 2) / lookAccurate;
        bool isHit = false;
        List<RaycastHit> hits;
        for (int i = 0; i < lookAccurate; i++)
        {
            if (CastRadiusRayAll(Quaternion.Euler(0, -1 * subAngle * (i + 1), 0), range, out hits, Color.red))
            {
                for (int j = 0; j < hits.Count; j++)
                {
                    HitEnemey(hits[j], () => { isHit = true; });
                }

            }
            if (CastRadiusRayAll(Quaternion.Euler(0, 1 * subAngle * (i + 1), 0), range, out hits, Color.red))
            {
                for (int j = 0; j < hits.Count; j++)
                {
                    HitEnemey(hits[j], () => { isHit = true; });
                }

            }
        }
        return isHit;
    }

    /// <summary>
    /// 发送扇形体射线检测 
    /// </summary>
    /// <param name="eulerAngler">偏移角</param>
    /// <param name="range">发射距离</param>
    /// <param name="hit">碰撞信息</param>
    /// <param name="debugColor">debug颜色</param>
    /// <param name="yoffset">厚度</param>
    /// <param name="duration">debug持续时间</param>
    /// <returns></returns>
    public bool CastRadiusRay(Quaternion eulerAngler, float range, out RaycastHit hit, Color debugColor, int layMask = 11, float yoffset = 1, float duration = 2)
    {
        Debug.DrawRay(transform.position, eulerAngler * transform.forward * range, debugColor);
        Debug.DrawRay(transform.position + new Vector3(0, yoffset, 0), eulerAngler * transform.forward * range, debugColor);
        return Physics.Raycast(transform.position, eulerAngler * transform.forward, out hit, range, 1 << layMask)
            || Physics.Raycast(transform.position + new Vector3(0, yoffset, 0), eulerAngler * transform.forward, out hit, range, 1 << layMask);
    }

    /// <summary>
    /// 发送扇形体射线检测穿透 
    /// </summary>
    /// <param name="eulerAngler">偏移角</param>
    /// <param name="range">发射距离</param>
    /// <param name="hit">碰撞信息</param>
    /// <param name="debugColor">debug颜色</param>
    /// <param name="yoffset">厚度</param>
    /// <param name="duration">debug持续时间</param>
    /// <returns></returns>
    public bool CastRadiusRayAll(Quaternion eulerAngler, float range, out List<RaycastHit> hits, Color debugColor, int layMask = 11, float yoffset = 1, float duration = 2)
    {
        hits = new List<RaycastHit>();
        Debug.DrawRay(transform.position, eulerAngler * transform.forward * range, debugColor);
        Debug.DrawRay(transform.position + new Vector3(0, yoffset, 0), eulerAngler * transform.forward * range, debugColor);
        RaycastHit[] hitdown = Physics.RaycastAll(transform.position, eulerAngler * transform.forward, range, 1 << layMask);
        RaycastHit[] hitup = Physics.RaycastAll(transform.position, eulerAngler * transform.forward, range, 1 << layMask);
        for (int i = 0; i < hitdown.Length; i++)
            hits.Add(hitdown[i]);
        for (int i = 0; i < hitup.Length; i++)
            hits.Add(hitdown[i]);

        return hits.Count != 0;
    }

    /// <summary>
    /// 发送球形射线指定方向 穿透所有
    /// </summary>
    /// <param name="localPos">相对坐标</param>
    /// <param name="range">范围</param>
    /// <param name="localDir">相对方向</param>
    /// <param name="Distance">距离</param>
    /// <param name="layMask">层级</param>
    /// <returns></returns>
    public RaycastHit[] CastSphereRayDirectionAll(Vector3 localPos, float range, Vector3 localDir, float Distance, int layMask = 11)
    {

        Debug.Log("球形射线");
        Debug.DrawRay(transform.TransformPoint(localPos), transform.TransformDirection(localDir) * Distance, Color.blue, 1);
        BeHits.Clear();
        RaycastHit[] hits = Physics.SphereCastAll(transform.TransformPoint(localPos), range, transform.TransformDirection(localDir), Distance, 1 << layMask);
        for (int i = 0; i < hits.Length; i++)
        {
            HitEnemey(hits[i]);
        }
        return hits;
    }

    /// <summary>
    /// 发送球形射线指定方向 碰撞第一个消失
    /// </summary>
    /// <param name="localPos">相对坐标</param>
    /// <param name="range">范围</param>
    /// <param name="localDir">相对方向</param>
    /// <param name="Distance">距离</param>
    /// <param name="layMask">层级</param>
    /// <returns></returns>
    public bool CastSphereRayDirection(Vector3 localPos, float range, Vector3 localDir, float Distance, int layMask = 11)
    {
        Debug.Log("球形射线");
        Debug.DrawRay(transform.TransformPoint(localPos), transform.TransformDirection(localDir) * Distance, Color.blue, 1);
        BeHits.Clear();
        RaycastHit hit;
        if (Physics.SphereCast(transform.TransformPoint(localPos), range, transform.TransformDirection(localDir), out hit, Distance, 1 << layMask))
        {
            if (HitEnemey(hit))
                return true;
        }

        return false;
    }

    #endregion

    #region UseCoilider
    
    /// <summary>
    /// 是否在往上跳
    /// </summary>
    public bool isJumpingUp;
    public float checkLength;


    public bool RayCastGround
    {
        get
        {
            Debug.DrawRay(transform.position, Vector3.down * checkLength, Color.cyan);
            return Physics.CheckSphere(transform.position, checkLength, 1 << LayerMask.NameToLayer("Ground"));
            //return cc.isGrounded;
        }

    }

    public void ThrowBox(Vector3 pos, Vector3 dir, float speed, string effect, float time, float angle)
    {
        var go = ResourceMgr.CreateObj(effect);
        if (go == null) return;
        go.transform.position = transform.TransformPoint(pos);
        Rigidbody rig = go.AddComponent<Rigidbody>();
        rig.useGravity = false;
        rig.velocity = transform.TransformDirection(dir) * speed;
        go.transform.rotation = Quaternion.LookRotation(transform.TransformDirection(dir));
        go.transform.Find("EffectOffset").Rotate(Vector3.right, angle);
        go.AddComponent<DestorySelf>().delay = time;
        go.AddComponent<SlashHitTarget>().OneHit = true;
        go.layer = LayerMask.NameToLayer(SelfWeaponLayer);
    }


    public void ThrowBoxAll(Vector3 pos, Vector3 dir, float speed, string effect, float time, float angle)
    {
        var go = ResourceMgr.CreateObj(effect);
        if (go == null) return;
        go.transform.position = transform.TransformPoint(pos);
        Rigidbody rig = go.AddComponent<Rigidbody>();
        rig.useGravity = false;
        rig.velocity = transform.TransformDirection(dir) * speed;
        go.transform.rotation = Quaternion.LookRotation(transform.TransformDirection(dir));
        go.transform.Find("EffectOffset").Rotate(Vector3.right, angle);
        go.AddComponent<DestorySelf>().delay = time;
        go.AddComponent<SlashHitTarget>();
        go.layer = LayerMask.NameToLayer(SelfWeaponLayer);
    }

    #endregion

    #region Effect

    /// <summary>
    /// 生成特效
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="pos"></param>
    /// <param name="dir"></param>
    /// <param name="time"></param>
    /// <param name="angle"></param>
    public void CreateEffect(Vector3 pos, Vector3 dir, float time, string effect, float angle)
    {
        var go = ResourceMgr.CreateObj(effect);

        if (go == null) return;
        go.transform.position = transform.TransformPoint(pos);
        go.transform.rotation = Quaternion.LookRotation(transform.TransformDirection(dir));
        if (go.transform.Find("EffectOffset") != null)
            go.transform.Find("EffectOffset").Rotate(Vector3.right, angle);
        go.AddComponent<DestorySelf>().delay = time;
    }

    #endregion
    #endregion

    /// <summary>
    /// 检测是否落地
    /// </summary>
    public virtual void CheckOnGround()
    {
    }

    /// <summary>
    /// 帧冻结 
    /// </summary>
    public void FreezeFrame(int frame)
    {
        ani.speed = 0;
        Utility.instance.WaitForFrame(frame, () => { ani.speed = 1; });
    }

}
