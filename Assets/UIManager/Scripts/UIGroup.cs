using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GroupInfo
{
	public string Name;
	public int Group;
}

public class UIGroup
{
	public static void Init(GroupInfo[] groupInfo)
	{
		for (int index = 0; index < groupInfo.Length; ++index)
		{
			m_groupDict.Add(groupInfo[index].Name, groupInfo[index].Group);
		}
	}
	public static int GetGroup(string name)
	{
		int group = 0;
		m_groupDict.TryGetValue(name, out group);
		return group;
	}
	private static Dictionary<string, int> m_groupDict = new Dictionary<string, int>();
}
