using UnityEngine;
using System.Collections;

public class UIDefault : UIWindow
{
	protected override void OnLoad()
	{
		Debug.Log(GetType().ToString() + " " + "OnLoad");
	}

	protected override void OnShow()
	{
		Debug.Log(GetType().ToString() + " " + "OnShow");
	}

	protected override void OnHide()
	{
		Debug.Log(GetType().ToString() + " " + "OnHide");
	}

	protected override void OnFixedUpdate()
	{
		//Debug.Log(GetType().ToString() + " " + "OnFixedUpdate");
	}

	protected override void OnDestroy()
	{
		Debug.Log(GetType().ToString() + " " + "OnDestroy");
	}
}
