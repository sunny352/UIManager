using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour
{
	private GameObject m_root;
	void Start()
	{
		Object.DontDestroyOnLoad(gameObject);
		GroupInfoList infoList = Resources.Load<GroupInfoList>("GroupInfo");
		UIGroup.Init(infoList.Info);
		GameObject root = LoadRes("UI", "UI Root");
		if (null != root)
		{
			m_root = Object.Instantiate<GameObject>(root);
			m_root.transform.SetParent(transform);
			UIManager.Init(m_root, LoadRes);
		}
		else
		{
			UIManager.Init(gameObject, LoadRes);
		}

		UIManager.ShowWindow<UITitle>();
		UIManager.ShowWindow<UINormal>();
		UIManager.ShowWindow<UIWarning>();
	}

	void FixedUpdate()
	{
		UIManager.FixedUpdate();
	}

	private GameObject LoadRes(string folder, string prefabName)
	{
		GameObject obj = null;
#if UNITY_EDITOR
		obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(string.Format("Assets/{0}/{1}.prefab", folder, prefabName));
#endif
		if (null != obj)
		{
			return obj;
		}
		if (null == obj)
		{
			obj = Resources.Load<GameObject>(string.Format("{0}/{1}", folder, prefabName));
		}
		return obj;
	}
}
