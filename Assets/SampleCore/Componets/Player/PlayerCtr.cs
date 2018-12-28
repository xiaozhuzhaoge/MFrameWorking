using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DoEvetHandler(string stateName);

public class PlayerCtr : MonoBehaviour {

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
    public Transform L;
    public Transform R;
    public Transform WeaponFollow;
    public SkinnedMeshRenderer weaponPoint;
    public float CurrentNormalizedTime { get { return fsm.FsmVariables.GetFsmFloat("CurrentNormalizedTime").Value; } }

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        fsm = GetComponent<PlayMakerFSM>();

        WeaponFollow = Utility.FindChild(transform, "WeaponPoint");
    }

    // Use this for initialization
    void Start () {

        WeaponFollow.gameObject.layer = LayerMask.NameToLayer("PlayerWeapon");
        playerFsm = new FSMMgr(gameObject);
        RegisterStates();
       
	}

    // Update is called once per frame
    void Update () {

        playerFsm.Update();

        if (fsm != null)
            if (playerFsm.GetState(fsm.ActiveStateName) != null)
                CurrentFsmState = fsm.ActiveStateName;

        CheckOnGround();

    }

    private string currentFsmState;

    public string CurrentFsmState
    {
        get
        {
            return currentFsmState;
        }

        set
        {
            if(currentFsmState != value)
            {
                Debug.Log("移动状态机" + value);
                playerFsm.MoveState(value);
            }

            currentFsmState = value;
        }
    }

    //public void OnGUI()
    //{
    //    GUILayout.Label(playerFsm.CurrentState.Name);
        
    //}

    void RegisterStates()
    {

        ///注册动画帧事件
        List<SkillChainConfig> SkillChains = SkillChainConfig.GetSkillChains(skillChainGroupId);

        for (int i = 0;i < SkillChains.Count; i++)
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


    List<Vector3> temp = new List<Vector3>();
  
    public void OpenBox() { WeaponFollow.gameObject.SetActive(true); }
    public void CloseBox() { WeaponFollow.gameObject.SetActive(false); }

    public void CastEffect(string value) {

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

    public bool HitEnemey(RaycastHit hit, Action callback = null) {

        IHitAnalysis beHit = hit.collider.GetComponent<IHitAnalysis>();
        if (!BeHits.Contains(beHit))
        {
            BeHits.Enqueue(beHit);
            beHit.BeHit(hit.point);
            FreezeFrame(10);
            if(callback != null)
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
    public bool RadiusCastHit(float angle,float lookAccurate,float range) {

        ///清除缓存
        BeHits.Clear();
        RaycastHit hit;
        float subAngle = (angle / 2) / lookAccurate;
        bool isHit = false;

        for (int i = 0; i < lookAccurate; i++) {
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
                for(int j =0; j < hits.Count; j++)
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
    public bool CastRadiusRay(Quaternion eulerAngler,float range, out RaycastHit hit, Color debugColor, int layMask = 11, float yoffset = 1,float duration = 2)
    {
        Debug.DrawRay(transform.position, eulerAngler * transform.forward * range, debugColor);
        Debug.DrawRay(transform.position + new Vector3(0,yoffset,0), eulerAngler * transform.forward * range, debugColor);
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
    public RaycastHit[] CastSphereRayDirectionAll(Vector3 localPos, float range, Vector3 localDir, float Distance, int layMask = 11) {

        Debug.Log("球形射线");
        Debug.DrawRay(transform.TransformPoint(localPos), transform.TransformDirection(localDir) * Distance, Color.blue, 1);
        BeHits.Clear();
        RaycastHit[] hits = Physics.SphereCastAll(transform.TransformPoint(localPos), range, transform.TransformDirection(localDir), Distance, 1 << layMask);
        for(int i = 0; i < hits.Length; i++)
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
            if(HitEnemey(hit))
                return true;
        }
 
        return false;
    }

    #endregion

    #region UseCoilider

    
    public void ThrowBox(Vector3 pos,Vector3 dir,float speed,string effect,float time, float angle) {
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
        go.layer = WeaponFollow.gameObject.layer;
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
        go.layer = WeaponFollow.gameObject.layer;
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
    public void CreateEffect(Vector3 pos, Vector3 dir,float time, string effect, float angle) {
        var go = ResourceMgr.CreateObj(effect);
         
        if (go == null) return;
        go.transform.position = transform.TransformPoint(pos);
        go.transform.rotation = Quaternion.LookRotation(transform.TransformDirection(dir));
        if(go.transform.Find("EffectOffset") != null)
            go.transform.Find("EffectOffset").Rotate(Vector3.right, angle);
        go.AddComponent<DestorySelf>().delay = time;
    }

    #endregion
    #endregion

    /// <summary>
    /// 是否在往上跳
    /// </summary>
    public bool isJumpingUp;

    /// <summary>
    /// 检测是否落地
    /// </summary>
    public void CheckOnGround() {

        if(isJumpingUp == false)
        {
            if(RayCastGround == false)
            {
                fsm.SetState("Fall");
            }
        }
  
    }
    public float checkLength;

    public bool RayCastGround
    {
        get
        {
            Debug.DrawRay(transform.position, Vector3.down * checkLength, Color.cyan);
            return Physics.CheckSphere(transform.position, checkLength, 1 << LayerMask.NameToLayer("Ground"));
        }
        
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
