using UnityEngine.UI;

public class UINormal : UIWindow
{
	protected override void OnLoad()
	{
		transform.Find("Close").GetComponent<Button>().onClick.AddListener(OnClose);
		transform.Find("OpenChild").GetComponent<Button>().onClick.AddListener(OnOpenChild);
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
