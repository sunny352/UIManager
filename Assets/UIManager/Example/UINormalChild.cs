using UnityEngine.UI;

public class UINormalChild : UIWindow
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
