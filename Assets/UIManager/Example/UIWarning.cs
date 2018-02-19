using UnityEngine.UI;

public class UIWarning : UIWindow
{
	protected override void OnLoad()
	{
		transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(OnClose);
	}
	private void OnClose()
	{
		Hide();
	}
}
