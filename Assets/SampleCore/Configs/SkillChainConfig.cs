using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJson;
using System;

public class SkillChainConfig : ConfigMode
{
    public int groupid;
    public string animationStateName;
    public string functionName;
    public float doPercent;
    public string instruction;

    public SkillChainConfig() {  }

    public SkillChainConfig(SimpleJson.JsonObject o)
    {

    }

    public override void Init(SimpleJson.JsonObject o)
    {
        base.Init(o);
        this.id = Convert.ToInt32(o["id"]);
        this.groupid = Convert.ToInt32(o["groupid"]);
        this.animationStateName = Convert.ToString(o["AnimationStateName"]);
        this.functionName = Convert.ToString(o["FunctionName"]);
        this.doPercent = Convert.ToSingle(o["DoPercent"]);
        this.instruction = Convert.ToString(o["instruction"]);
    }

    /// <summary>
    /// 获取指定技能组信息
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public static List<SkillChainConfig> GetSkillChains(int groupId) {
        return ConfigInfo.Instance._skillChainGroup[groupId];
    }
}
