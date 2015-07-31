using UnityEngine;

public class Example : MonoBehaviour
{
	void Start()
	{
		UIManager.Init(gameObject, Loader);
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
