using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ConfigInfo : MonoSingleton<ConfigInfo>
{
    Dictionary<int, SkillChainConfig> _skillChains = new Dictionary<int, SkillChainConfig>();
    public Dictionary<int, List<SkillChainConfig>> _skillChainGroup = new Dictionary<int, List<SkillChainConfig>>();

    public static void skillChainConfig(List<SkillChainConfig> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            ConfigInfo.Instance._skillChains.Add(list[i].id, list[i]);
            if (!ConfigInfo.Instance._skillChainGroup.ContainsKey(list[i].groupid))
                ConfigInfo.Instance._skillChainGroup.Add(list[i].groupid, new List<SkillChainConfig>());

            ConfigInfo.Instance._skillChainGroup[list[i].groupid].Add(list[i]);
        }
    }

    public Dictionary<int, SkillChainConfig> skillChains
    {
        get
        {
            PreLoadConfig("SkillChain", _skillChains.Count == 0);
            return _skillChains;
        }
    }
}
