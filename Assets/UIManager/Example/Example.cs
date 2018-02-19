using UnityEngine;

public class Example : MonoBehaviour
{
	private GameObject m_root;
	void Start()
	{
		DontDestroyOnLoad(gameObject);
		var infoList = Resources.Load<GroupInfoList>("GroupInfo");
		UIGroup.Init(infoList.Info);
		var root = LoadRes("UI", "UI Root");
		if (null != root)
		{
			m_root = Instantiate(root);
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
		GameObject obj;
#if UNITY_EDITOR
		obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/{folder}/{prefabName}.prefab");
#endif
		if (null != obj)
		{
			return obj;
		}

		obj = Resources.Load<GameObject>($"{folder}/{prefabName}");
		return obj;
	}
}
