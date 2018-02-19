using System.Collections.Generic;

[System.Serializable]
public class GroupInfo
{
	public string Name;
	public int Group;
}

public class UIGroup
{
	public static void Init(IEnumerable<GroupInfo> groupInfo)
	{
		foreach (var info in groupInfo)
		{
			m_groupDict.Add(info.Name, info.Group);
		}
	}
	public static int GetGroup(string name)
	{
		int group;
		m_groupDict.TryGetValue(name, out group);
		return group;
	}
	private static readonly Dictionary<string, int> m_groupDict = new Dictionary<string, int>();
}
