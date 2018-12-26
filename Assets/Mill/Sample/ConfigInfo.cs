using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SimpleJson;
using System.Linq;
using System.Reflection;

public partial class ConfigInfo : MonoSingleton<ConfigInfo> {

    static Dictionary<string, ConfigSummary> summarys = new Dictionary<string, ConfigSummary>();

    protected override void Initialize()
    {
        base.Initialize();
        ReadConfigs();
        Init();
    }

    public static void Init()
    {
        //Type type = typeof(ConfigInfo);

        //foreach (var data in summarys) {
        //    Type T = Type.GetType(data.Key);
        //    Debug.Log(T);

        //    foreach (var field in type.GetMethods()) {
        //        Debug.Log(field);
        //    }

        //    MethodInfo info = type.GetMethod("RegisterConfigHandler").MakeGenericMethod(T);
           
        //    // type.GetMethod(data.Value.containerName).MakeGenericMethod(T)
        //    object[] param = { data.Value.configPath, new List<MusicConfig>()};
            
        //    info.Invoke(ConfigInfo.Instance, null);
        //}

        RegisterConfigHandler<MusicConfig>("Configs/json/musics", musicConfig);
        RegisterConfigHandler<SkillChainConfig>("Configs/json/SkillChain", skillChainConfig);
        ConfigInfo.ReadAllConfigsFuc();
       
    }

    /// <summary>
    /// 读取资源表配置
    /// </summary>
    public void ReadConfigs() {
        JsonArray summary = SimpleJson.SimpleJson.DeserializeObject<JsonArray>(ConfigReader.ReadConfig("Configs/Configs", ""));
        ConfigSummary s = null;
        foreach (var data in summary) {
            s = new ConfigSummary((JsonObject)data);
            summarys.Add(s.className, s);
        }
    }

    /// <summary>
    /// 配置信息
    /// </summary>
    class ConfigSummary {

        public int id;
        public string className;
        public string configPath;
        public string assetName;
        public string containerName;

        public ConfigSummary() { }
        public ConfigSummary(JsonObject o) {
            id = Convert.ToInt32(o["ID"]);
            className = o["ClassName"].ToString();
            configPath = o["ConfigPath"].ToString();
            assetName = o["AssetName"].ToString();
            containerName = o["ContainerName"].ToString();
        }
    }

}


