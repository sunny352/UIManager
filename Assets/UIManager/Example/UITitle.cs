using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITitle : UIWindow
{
	protected override void OnLoad()
	{
		transform.FindChild("OpenNormal").GetComponent<Button>().onClick.AddListener(OnNormal);
		transform.FindChild("OpenReplace").GetComponent<Button>().onClick.AddListener(OnReplace);
	}
	private void OnNormal()
	{
		UIManager.ShowWindow<UINormal>();
	}
	private void OnReplace()
	{
		UIManager.ShowWindow<UIReplaceNormal>();
	}
}
