using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour
{
	void Start()
	{
		UIManager.Init(gameObject, Loader);
		UIManager.ShowWindow<UIDefault>();
		StartCoroutine(HideUIDefault());
		UIManager.ShowWindow<UIDestroyAfterHide>();
		StartCoroutine(HideUIDestroyAfterHide());
	}
	private IEnumerator HideUIDefault()
	{
		yield return new WaitForSeconds(1.0f);
		UIManager.HideWindow<UIDefault>();
	}
	private IEnumerator HideUIDestroyAfterHide()
	{
		yield return new WaitForSeconds(1.0f);
		UIManager.HideWindow<UIDestroyAfterHide>();
	}

	void FixedUpdate()
	{
		UIManager.FixedUpdate();
	}

	private GameObject Loader(string folderPath, string prefabName)
	{
		return Resources.Load<GameObject>(string.Format("{0}/{1}", folderPath, prefabName));
	}
}
