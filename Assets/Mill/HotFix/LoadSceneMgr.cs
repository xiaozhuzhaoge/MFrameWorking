using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneMgr : MonoBehaviour {

	public static LoadSceneMgr instance;
	public AsyncOperation ao;
	public float progress;

	void Awake(){
		instance = this;
		DontDestroyOnLoad (this);
		 
	}

	/// <summary>
	/// 加载到指定场景
	/// </summary>
	/// <param name="sceneName">Scene name.</param>
	public void StartLoadScene(string sceneName){
		StartCoroutine (LoadScene (sceneName));
	}

	public IEnumerator LoadScene(string sceneName){
		progress = 0;
		ao = SceneManager.LoadSceneAsync (sceneName);
		ao.allowSceneActivation = false;

		///当前加载的场景进度为 0.9 or 当前加载的假进度为100
		while (ao.progress < 0.9f || progress <= 1f) {
			progress += 0.05f;
			yield return new WaitForEndOfFrame ();
		}
		progress = 1f;
		ao.allowSceneActivation = true;
	}
	 
}
