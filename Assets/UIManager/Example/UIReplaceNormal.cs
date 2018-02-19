using UnityEngine.UI;

public class UIReplaceNormal : UIWindow
{
	protected override void OnLoad()
	{
		transform.Find("Close").GetComponent<Button>().onClick.AddListener(OnClose);
	}
	private void OnClose()
	{
		Hide();
	}
}
