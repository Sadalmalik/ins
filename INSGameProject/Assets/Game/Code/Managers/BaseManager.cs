using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseManager : SerializedMonoBehaviour
{
	protected static List<BaseManager> _managers = new List<BaseManager>();

	public static void InitAll()
	{
		foreach (var manager in _managers)
		{
			Debug.Log($"[MANAGER] Init: {manager.GetType().Name}");
			manager.Init();
		}
	}

	public virtual void Init()
	{
	}
}

public class BaseManager<M> : BaseManager where M : BaseManager<M>
{
	public static M instance;

	private void Awake()
	{
		instance = (M) this;
		Debug.Log($"[MANAGER] Add: {GetType().Name}");
		_managers.Add((M) this);
	}
}