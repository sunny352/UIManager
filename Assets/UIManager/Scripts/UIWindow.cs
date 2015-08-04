using UnityEngine;

public class UIWindow
{
	public GameObject gameObject { get; private set; }
	public Transform transform { get { return null != gameObject ? gameObject.transform : null; } }
	public UIWindow Parent { get; private set; }
	public string PrefabName { get; private set; }
	public bool IsDestroyAfterHide { get; set; }
	protected virtual void OnLoad()
	{
	}
	protected virtual void OnShow()
	{
	}
	protected virtual void OnHide()
	{
	}
	protected virtual void OnFixedUpdate()
	{
	}
	protected virtual void OnDestroy()
	{
	}
	public void Load()
	{
		IsDestroyAfterHide = true;
		if (null != gameObject)
		{
			Debug.LogWarningFormat("{0} already loaded!", GetType().ToString());
			return;
		}
		PrefabName = GetType().ToString();
		if (null != UIManager.Root)
		{
			Transform rootTrans = UIManager.Root.transform;
			gameObject = Object.Instantiate(UIManager.Load(PrefabName), rootTrans.position, rootTrans.rotation) as GameObject;
			transform.localScale = rootTrans.localScale;
			transform.parent = rootTrans;
		}
		else
		{
			gameObject = Object.Instantiate(UIManager.Load(PrefabName), Vector3.zero, Quaternion.identity) as GameObject;
		}
		gameObject.SetActive(false);
		m_isShown = false;
		OnLoad();
	}
	public void Show(UIWindow parent)
	{
		Parent = parent;
		Show();
		if (null != parent)
		{
			parent.SetVisiable(false);
		}
		else
		{
			UIManager.HideGroup(PrefabName);
		}
	}
	private void Show()
	{
		if (IsShown())
		{
			Debug.LogWarningFormat("{0} already shown!", GetType().ToString());
			return;
		}
		gameObject.SetActive(true);
		m_isShown = true;
		OnShow();
	}
	public void Hide()
	{
		if (!IsShown())
		{
			Debug.LogWarningFormat("{0} already hidden!", GetType().ToString());
			return;
		}
		gameObject.SetActive(false);
		m_isShown = false;
		OnHide();
		if (null != Parent)
		{
			Parent.SetVisiable(true);
			Parent = null;
		}
	}
	public void FixedUpdate()
	{
		OnFixedUpdate();
	}
	public void Destroy()
	{
		if (null != Parent)
		{
			Parent.SetVisiable(true);
			Parent = null;
		}
		if (null == gameObject)
		{
			Debug.LogWarningFormat("{0} already destroyed!", GetType().ToString());
			return;
		}
		GameObject.Destroy(gameObject);
		OnDestroy();
	}
	public bool IsShown()
	{
		return m_isShown;
	}
	private bool m_isShown = false;
	public bool IsVisiable()
	{
		return null != gameObject ? gameObject.activeSelf : false;
	}
	public void SetVisiable(bool isVisable)
	{
		if (null != gameObject && IsShown())
		{
			gameObject.SetActive(isVisable);
		}
	}
}
