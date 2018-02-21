using System.Collections;
using UnityEngine;

public class UIWindow
{
    public GameObject gameObject { get; private set; }
    public Transform transform => null != gameObject ? gameObject.transform : null;
    public UIWindow Parent { get; private set; }
    public string PrefabName { get; private set; }
    public bool IsDestroyAfterHide { get; private set; }

    protected virtual void OnLoad()
    {
    }

    protected virtual void OnShowBegin()
    {
    }

    protected virtual void OnShowEnd()
    {
    }

    protected virtual void OnHideBegin()
    {
    }

    protected virtual void OnHideEnd()
    {
    }

    protected virtual IEnumerator OnTween(bool isForward)
    {
        yield break;
    }

    protected virtual void OnFixedUpdate()
    {
    }

    protected virtual void OnDestroy()
    {
    }

    private UIComponent _component;
    public void Load()
    {
        IsDestroyAfterHide = true;
        if (null != gameObject)
        {
            Debug.LogWarningFormat("{0} already loaded!", GetType());
            return;
        }

        PrefabName = GetType().ToString();
        if (null != UIManager.Root)
        {
            var rootTrans = UIManager.Root.transform;
            gameObject = Object.Instantiate(UIManager.Load(PrefabName), rootTrans.position, rootTrans.rotation);
            transform.localScale = rootTrans.localScale;
            transform.SetParent(rootTrans);
        }
        else
        {
            gameObject = Object.Instantiate(UIManager.Load(PrefabName), Vector3.zero, Quaternion.identity);
        }

        gameObject.SetActive(false);
        gameObject.AddComponent<UIComponent>();
        m_isShown = false;
        OnLoad();
    }

    public void Show(UIWindow parent)
    {
        Parent = parent;
        _component.StartCoroutine(ShowImpl(parent));
    }

    private IEnumerator ShowImpl(UIWindow parent)
    {
        if (!IsShown())
        {
            OnShowBegin();
            gameObject.SetActive(true);
            yield return _component.StartCoroutine(OnTween(true));
            m_isShown = true;
            OnShowEnd();
        }
        if (null != parent)
        {
            parent.SetVisiable(false);
        }
        else
        {
            UIManager.HideGroup(PrefabName);
        }
    }

    public void Hide()
    {
        _component.StartCoroutine(HideImpl());
    }

    private IEnumerator HideImpl()
    {
        if (IsShown())
        {
            OnHideBegin();
            m_isShown = false;
            yield return _component.StartCoroutine(OnTween(false));
            gameObject.SetActive(false);
            OnHideEnd();
        }

        Parent?.SetVisiable(true);
        Parent = null;
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
            Debug.LogWarningFormat("{0} already destroyed!", GetType());
            return;
        }

        Object.Destroy(gameObject);
        OnDestroy();
    }

    public bool IsShown()
    {
        return m_isShown;
    }

    private bool m_isShown;

    public bool IsVisiable()
    {
        return IsShown() && null != gameObject && gameObject.activeSelf;
    }

    private void SetVisiable(bool isVisable)
    {
        if (null != gameObject && IsShown())
        {
            gameObject.SetActive(isVisable);
        }
    }
}