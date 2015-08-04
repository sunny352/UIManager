using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UINormal : UIWindow
{
	protected override void OnLoad()
	{
		transform.FindChild("Close").GetComponent<Button>().onClick.AddListener(OnClose);
		transform.FindChild("OpenChild").GetComponent<Button>().onClick.AddListener(OnOpenChild);
	}
	private void OnClose()
	{
		Hide();
	}
	private void OnOpenChild()
	{
		UIManager.ShowWindow<UINormalChild>(this);
	}
}
