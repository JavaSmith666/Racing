using UnityEngine;

public class UITool
{
	private static readonly UITool instance = new();

	public static UITool Instance { get { return instance; } }  //singleton

	private GameObject activePanel;

	private UITool() { }

	public void SetActivePanel(GameObject panel)
	{
		activePanel = panel;
	}

	public ref GameObject GetActivePanel()
	{
		return ref activePanel;
	}

	public T GetOrAddComponent<T>()where T : Component
	{
		if (activePanel.GetComponent<T>() == null)
		{
			activePanel.AddComponent<T>();
		}
		return activePanel.GetComponent<T>();
	}

	public GameObject FindChildGameObject(string name)
	{
		Transform[] trans = activePanel.GetComponentsInChildren<Transform>();
		foreach(var item in trans)
		{
			if(item.name == name)
			{
				return item.gameObject;
			}
		}
		Debug.LogWarning($"The object {name} can not be found in panel {activePanel.name}");
		return null;
	}

	public T GetOrAddComponentInChildren<T>(string name) where T : Component
	{
		GameObject child = FindChildGameObject(name);
		if (child == null)
		{
			return null;
		}
		if (child.GetComponent<T>() == null)
		{
			child.AddComponent<T>();
		}
		return child.GetComponent<T>();
	}
}