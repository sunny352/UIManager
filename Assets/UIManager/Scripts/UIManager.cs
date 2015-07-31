using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
	public static T ShowWindow<T>() where T : UIWindow, new()
	{
		string prefabName = typeof(T).ToString();
		UIWindow window = null;
		if (m_windowDict.TryGetValue(prefabName, out window))
		{
			return window as T;
		}
		else
		{
			return CreateWindow<T>();
		}
	}
	public static void HideWindow<T>() where T : UIWindow
	{
		HideWindow(typeof(T).ToString());
	}
	public static void HideWindow(string prefabName)
	{
		UIWindow window = null;
		if (m_windowDict.TryGetValue(prefabName, out window))
		{
			window.Hide();
		}
	}
	public static bool IsVisiable<T>() where T : UIWindow
	{
		return IsVisiable(typeof(T).ToString());
	}
	public static bool IsVisiable(string prefabName)
	{
		UIWindow window = null;
		if (m_windowDict.TryGetValue(prefabName, out window))
		{
			return window.IsVisiable();
		}
		else
		{
			return false;
		}
	}
	private static T CreateWindow<T>() where T : UIWindow, new()
	{
		T window = new T();
		window.Load();
		m_windowDict.Add(window.PrefabName, window);
		return window;
	}
	public static GameObject Root { get; private set; }
	public delegate GameObject LoaderDelegate(string folderPath, string prefabName);
	public static void Init(GameObject root, LoaderDelegate loader)
	{
		Root = root;
		Loader = loader;
	}
	private static List<string> m_destroyList = new List<string>();
	public static void FixedUpdate()
	{
		foreach (var pair in m_windowDict)
		{
			pair.Value.FixedUpdate();
			if (!pair.Value.IsVisiable())
			{
				m_destroyList.Add(pair.Key);
			}
		}
		for (int index = 0; index < m_destroyList.Count; ++index)
		{
			m_windowDict.Remove(m_destroyList[index]);
		}
	}

	private static LoaderDelegate Loader { get; set; }
	private static readonly string DefaultFolderPath = "UI";
	public static GameObject Load(string prefabName)
	{
		return Loader(DefaultFolderPath, prefabName);
	}
	static UIManager()
	{
		Loader = DefaultLoader;
	}
	private static GameObject DefaultLoader(string folderPath, string prefabName)
	{
		return Resources.Load<GameObject>(string.Format("{0}/{1}", folderPath, prefabName));
	}
	private static Dictionary<string, UIWindow> m_windowDict = new Dictionary<string, UIWindow>();
}
