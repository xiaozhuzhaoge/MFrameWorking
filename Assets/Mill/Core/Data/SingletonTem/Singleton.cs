using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : System.IDisposable where T : new()
{
	private static T instance;
 
	public static T Instance {
		get {
			if (instance == null) {
				instance = new T ();
			}
			return instance;
		}
	}
 
	public virtual void Dispose ()
	{
		
	}
}

/// <summary>
/// Persistent manager - a singleton component, that will not
/// be destroyed between levels.
/// </summary>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	protected static T _instance;

	protected virtual void Awake()
	{
		// First we check if there are any other instances conflicting
		if (Instance != null && Instance != this)
		{
			// If that is the case, we destroy other instances
			Destroy(gameObject);
			return;
		}

		// Here we save our singleton instance
		Instance = this as T;

		// Furthermore we make sure that we don't destroy between scenes (this is optional)
		//DontDestroyOnLoad(gameObject);

		Initialize();
	}

	/// <summary>
	/// Use this for one time initialization
	/// </summary>
	protected virtual void Initialize() { }

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (T)Object.FindObjectOfType(typeof(T));
			}
			return _instance;
		}
		protected set
		{
			_instance = value;
		}
	}

	public static bool HasInstance
	{
		get { return _instance != null; }
	}

}