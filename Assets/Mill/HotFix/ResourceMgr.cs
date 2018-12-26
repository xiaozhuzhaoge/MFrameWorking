using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// 数据匹配更新
/// </summary>
public class ResourceMgr : MonoSingleton<ResourceMgr>
{
    /// <summary>
    /// AB下载路径
    /// </summary>
    public string url = "http://127.0.0.1/";
    public string manifestName = "StreamingAssets";
    public string nextLevel = "Login";

    #region 本地资源处理方法

    /// <summary>
    /// 键为资源包名 值为资源包对象
    /// </summary>
    public static Dictionary<string, AssetBundle> caches资源包缓存;

    /// <summary>
    /// 键为资源包名 值为小字典 小字典中键为 资源名 值为资源路径
    /// </summary>
    public static Dictionary<string, Dictionary<string, string>> caches资源包与所有对应的资源数据;

	/// <summary>
	/// 进度条变动委托
	/// </summary>
    public delegate void OnProgressChangeEventHandler(float progress);

    public OnProgressChangeEventHandler OnProgressChange;

	/// <summary>
	/// 进度数据内容变动委托
	/// </summary>
	public delegate void OnProgressContentChangedEventHandler(string cotent);

	public OnProgressContentChangedEventHandler OnProgressContentChange;

    private float progress;
    public float Progress
    {
        get { return progress; }
        set {
            if (progress < value)
            {
				///监听变动
                OnProgressChange(value);
            }
            progress = value;
        }
    }

	private string currentProgress; 

	public string CurrentProgress{
		get { return currentProgress; }
		set {
			if (currentProgress != value)
			{
				///监听变动
				OnProgressContentChange(value);
			}
			currentProgress = value;
		}
	}

    protected override void Initialize()
    {
        base.Initialize();
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 添加资源包数据
    /// </summary>
    /// <param name="abName">资源包名</param>
    /// <param name="ab">资源包</param>
    /// <returns></returns>
    public static bool AddAbToCache(string abName,AssetBundle ab) {
        
        if (caches资源包缓存 == null)
            caches资源包缓存 = new Dictionary<string, AssetBundle>();

        if (caches资源包缓存.ContainsKey(abName))
            return false;
        caches资源包缓存.Add(abName, ab);
        return true;
    }

    /// <summary>
    /// 获取资源包数据
    /// </summary>
    /// <param name="abName">资源包名</param>
    /// <returns></returns>
    public static AssetBundle GetAbFromCache(string abName) {
        
        if (caches资源包缓存 != null)
        {
            if (caches资源包缓存.ContainsKey(abName))
                return caches资源包缓存[abName];
            return null;
        }
        return null;
    }

    /// <summary>
    /// 很绕但是很重要~~~~ 添加数据到资源缓存
    /// </summary>
    /// <param name="AbName"></param>
    /// <param name="AssetName"></param>
    /// <param name="AssetPath"></param>
    /// <returns></returns>
    public static bool AddIntoCacheAssets(string AbName,string AssetName,string AssetPath) {

        if (caches资源包与所有对应的资源数据 == null)
        {
            caches资源包与所有对应的资源数据 = new Dictionary<string, Dictionary<string, string>>();
        }
        if (!caches资源包与所有对应的资源数据.ContainsKey(AbName))
            caches资源包与所有对应的资源数据.Add(AbName, new Dictionary<string, string>());

        if (!caches资源包与所有对应的资源数据[AbName].ContainsKey(AssetName))
        {
            caches资源包与所有对应的资源数据[AbName].Add(AssetName, AssetPath);
            return true;
        }
        return false;
    }

    
    #endregion

    #region 通过网络下载资源

    /// <summary>
    /// 下载资源并缓存
    /// </summary>
    /// <param name="url">服务器地址</param>
    /// <param name="abName">资源包名</param>
    /// <param name="hashCode">资源包哈希值</param>
    /// <returns></returns>
    IEnumerator DownLoadAssets(string url,string abName,Hash128 hashCode) {
        
        ///下载指定的资源包 
        ///1.如果本地不存在这个资源 直接下载 直接保存到本地缓存中
        ///2.如果本地存在这个资源进行 hashcode对比 如果不一样进行下载解析资源包 如果一样解析本地资源包
        WWW download = WWW.LoadFromCacheOrDownload(url + abName,hashCode);
       Debug.Log("下载资源包 " + url + abName + " 哈希值 " + hashCode);
        ///等待HTTP请求返回
        yield return download;

        ///获取下载的资源包
        AssetBundle ab = download.assetBundle;
        AddAbToCache(ab.name, ab);

        foreach (var assetPath in ab.GetAllAssetNames())
        {
            Debug.Log(assetPath);
            string temp = assetPath.Substring(assetPath.LastIndexOf("/") + 1);
            temp = temp.Remove(temp.LastIndexOf("."));
            AddIntoCacheAssets(abName, temp, assetPath);
        }
        ///释放当前联网缓存
        download.Dispose();
    }

    public IEnumerator GetManifest(string url,string abName){

        ///下载资源包 
        WWW donwloadManifest = new WWW(url + abName);
        yield return donwloadManifest;

        AssetBundle ab = donwloadManifest.assetBundle;
        AddAbToCache(ab.name, ab);

        
        assetbundleManifest = ab.LoadAsset<AssetBundleManifest>("assetbundlemanifest");
        //ab.Load<AssetBundleManifest>

        currentProgress = "正在解析资源包数据...";
		Progress = 0.1f;
        
        yield return new WaitForSeconds(1f);

        float totalAssets = assetbundleManifest.GetAllAssetBundles().Length;
 
        foreach (var assetBundleName in assetbundleManifest.GetAllAssetBundles()) {
            //Debug.Log("资源包  " + assetBundleName + "  哈希值  " + assetbundleManifest.GetAssetBundleHash(assetBundleName));
            ///下载每一个资源包 

			Progress += (1.0f / totalAssets) * 0.8f;
			CurrentProgress = string.Format ("正在下载的资源{0}", assetBundleName);
            yield return StartCoroutine(DownLoadAssets(url, assetBundleName, assetbundleManifest.GetAssetBundleHash(assetBundleName)));
        }

		///释放当前联网缓存
		donwloadManifest.Dispose();


		currentProgress = "正在进行热更新补丁修复...";
		//热更新补丁 打补丁
		MakeHotFix.Instance.MakeHotFixFuc();

		yield return new WaitForSeconds (1);

		//开始加载场景 转换场景功能
		LoadSceneMgr.instance.StartLoadScene (nextLevel);


		while (LoadSceneMgr.instance.progress < 1) {
			CurrentProgress = string.Format ("当前加载场景进度{0}%", (Mathf.CeilToInt(LoadSceneMgr.instance.progress * 100)).ToString());
			Progress +=	LoadSceneMgr.instance.progress * 0.1f;
			yield return new WaitForEndOfFrame();
		}
    }

    #endregion

    ///总体资源包配置文件
    static AssetBundleManifest assetbundleManifest;

    public void Start()
    {
        StartCoroutine(GetManifest(url, manifestName));
    }
 
    #region 用户要使用的方法

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public static Object Load(string assetName资源名)
    {
        ///转成小写
        assetName资源名 = assetName资源名.ToLower();
        Debug.Log(assetName资源名);
        Object obj = Resources.Load(assetName资源名);
        if (obj != null)
            return obj;

        foreach (var abName键值对 in caches资源包与所有对应的资源数据)
        {
            ///找到了指定资源对应的资源包名
            if (abName键值对.Value.ContainsKey(assetName资源名))
            {
                ///资源包名 abName键值对.Key
                AssetBundle ab = caches资源包缓存[abName键值对.Key];
                if (ab != null)
                {
                   return ab.LoadAsset(abName键值对.Value[assetName资源名]);
                }
            }
        }
        return null;
    }


    /// <summary>
    /// 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetName资源名"></param>
    /// <returns></returns>
    public static T Load<T>(string assetName资源名) where T : UnityEngine.Object
    {
        //System.Object
        //UnityEngine.Object

        ///转成小写
        assetName资源名 = assetName资源名.ToLower();
        T obj = Resources.Load<T>(assetName资源名);
        if (obj != null)
            return obj;
        if (caches资源包与所有对应的资源数据 == null)
            return null;

        foreach (var abName键值对 in caches资源包与所有对应的资源数据)
        {
            ///找到了指定资源对应的资源包名
            if (abName键值对.Value.ContainsKey(assetName资源名))
            {
                ///资源包名 abName键值对.Key
                AssetBundle ab = caches资源包缓存[abName键值对.Key];
                if (ab != null)
                {
                    return ab.LoadAsset<T>(abName键值对.Value[assetName资源名]);
                }
            }
        }
        return null;
    }

    public static GameObject CreateObj(string name)
    {
        GameObject g = Load<GameObject>(name);
        if (g != null)
            return GameObject.Instantiate<GameObject>(g);
        else
            return null;
    }

    #endregion
}
