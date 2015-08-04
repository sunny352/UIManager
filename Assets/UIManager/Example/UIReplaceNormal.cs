﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIReplaceNormal : UIWindow
{
	protected override void OnLoad()
	{
		transform.FindChild("Close").GetComponent<Button>().onClick.AddListener(OnClose);
	}
	private void OnClose()
	{
		Hide();
	}
}