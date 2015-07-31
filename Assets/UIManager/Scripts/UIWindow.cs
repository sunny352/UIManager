using UnityEngine;

public class UIWindow
{
	public GameObject Window { get; private set; }
	public string PrefabName { get; private set; }
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
		if (null != Window)
		{
			Debug.LogWarningFormat("{0} already loaded!", GetType().ToString());
			return;
		}
		PrefabName = GetType().ToString();
		Transform rootTrans = UIManager.Root.transform;
		Window = Object.Instantiate(UIManager.Load(PrefabName), rootTrans.position, rootTrans.rotation) as GameObject;
		Window.transform.parent = rootTrans;
		Window.SetActive(false);
		m_isShown = false;
		OnLoad();
	}
	public void Show()
	{
		if (IsShown())
		{
			Debug.LogWarningFormat("{0} already shown!", GetType().ToString());
			return;
		}
		Window.SetActive(true);
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
		Window.SetActive(false);
		m_isShown = false;
		OnHide();
	}
	public void FixedUpdate()
	{
		OnFixedUpdate();
	}
	public void Destroy()
	{
		if (null == Window)
		{
			Debug.LogWarningFormat("{0} already destroyed!", GetType().ToString());
			return;
		}
		GameObject.Destroy(Window);
		OnDestroy();
	}
	public bool IsShown()
	{
		return m_isShown;
	}
	private bool m_isShown = false;
	public bool IsVisiable()
	{
		return null != Window ? Window.activeSelf : false;
	}
	public void SetVisiable(bool isVisable)
	{
		if (null != Window)
		{
			Window.SetActive(isVisable);
		}
	}
}
