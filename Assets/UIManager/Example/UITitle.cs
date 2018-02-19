using UnityEngine.UI;

public class UITitle : UIWindow
{
	protected override void OnLoad()
	{
		transform.Find("OpenNormal").GetComponent<Button>().onClick.AddListener(OnNormal);
		transform.Find("OpenReplace").GetComponent<Button>().onClick.AddListener(OnReplace);
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
