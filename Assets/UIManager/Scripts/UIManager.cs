using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
	public static T ShowWindow<T>() where T : UIWindow, new()
	{
		return ShowWindow<T>(null);
	}
	public static T ShowWindow<T>(UIWindow parent) where T : UIWindow, new()
	{
		var prefabName = typeof(T).ToString();
		UIWindow window;
		if (m_windowDict.TryGetValue(prefabName, out window))
		{
			window.Show(parent);
		}
		else
		{
			window = CreateWindow<T>();
			window.Show(parent);
		}
		return window as T;
	}
	public static void HideWindow<T>() where T : UIWindow
	{
		HideWindow(typeof(T).ToString());
	}
	public static void HideWindow(string prefabName)
	{
		UIWindow window;
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
		UIWindow window;
		return m_windowDict.TryGetValue(prefabName, out window) && window.IsVisiable();
	}
	private static T CreateWindow<T>() where T : UIWindow, new()
	{
		var window = new T();
		window.Load();
		m_windowDict.Add(window.PrefabName, window);
		return window;
	}
	public static void HideGroup(string name)
	{
		var group = UIGroup.GetGroup(name);
		foreach (var pair in m_windowDict)
		{
			if (UIGroup.GetGroup(pair.Key) == group && pair.Key != name)
			{
				HideWindow(pair.Key);
			}
		}
	}
	public static GameObject Root { get; private set; }
	public delegate GameObject LoaderDelegate(string folderPath, string prefabName);
	public static void Init(GameObject root, LoaderDelegate loader)
	{
		Root = root;
		if (null != loader)
		{
			Loader = loader;
		}
	}
	private static readonly List<string> m_destroyList = new List<string>();
	public static void FixedUpdate()
	{
		foreach (var pair in m_windowDict)
		{
			if (pair.Value.IsShown())
			{
				pair.Value.FixedUpdate();
			}
			else
			{
				if (pair.Value.IsDestroyAfterHide)
				{
					m_destroyList.Add(pair.Key);
				}
			}
		}

		if (m_destroyList.Count <= 0)
		{
			return;
		}
		foreach (var name in m_destroyList)
		{
			UIWindow window;
			if (m_windowDict.TryGetValue(name, out window))
			{
				if (window.IsShown())
				{
					continue;
				}

				window.Destroy();
			}
			m_windowDict.Remove(name);
		}
		m_destroyList.Clear();
	}

	private static LoaderDelegate Loader { get; set; }
	private const string DefaultFolderPath = "UI";

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
		return Resources.Load<GameObject>($"{folderPath}/{prefabName}");
	}
	private static readonly Dictionary<string, UIWindow> m_windowDict = new Dictionary<string, UIWindow>();
}
