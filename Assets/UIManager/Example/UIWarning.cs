using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIWarning : UIWindow
{
	protected override void OnLoad()
	{
		transform.FindChild("Confirm").GetComponent<Button>().onClick.AddListener(OnClose);
	}
	private void OnClose()
	{
		Hide();
	}
}
