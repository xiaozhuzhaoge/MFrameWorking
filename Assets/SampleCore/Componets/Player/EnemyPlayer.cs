using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPlayer : CharacterCtrlBase
{
    /// <summary>
    /// 寻路组件
    /// </summary>
    public NavMeshAgent agent;
    /// <summary>
    /// 寻路遮挡物
    /// </summary>
    public NavMeshObstacle nmo;

    public override void OnAwake()
    {
        base.OnAwake();
        agent = GetComponent<NavMeshAgent>();
        nmo = GetComponent<NavMeshObstacle>();
      
    }

  
}
