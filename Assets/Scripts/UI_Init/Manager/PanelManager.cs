using System.Collections.Generic;
using UnityEngine;

public class PanelManager
{
	private static readonly PanelManager instance = new();

	public static PanelManager Instance { get { return instance; } }  //singleton

	private Stack<BasePanel> stackPanel;

	private PanelManager()
	{
		stackPanel = new Stack<BasePanel>();
	}

	public void Push(BasePanel panel)
	{
		if (stackPanel.Count > 0)
		{
			stackPanel.Peek().OnPasue();
		}
		stackPanel.Push(panel);
		GameObject panelGo = UIManager.Instance.GetSingleUI(panel.UiType, "Canvas");
		UITool.Instance.SetActivePanel(panelGo);
		panel.OnEnter();
	}

	public void Pop()
	{
		if (stackPanel.Count > 0)
		{
			stackPanel.Peek().OnExit();
			stackPanel.Pop();
		}
		if (stackPanel.Count > 0)
		{
			UITool.Instance.SetActivePanel(UIManager.Instance.GetDicUI()[stackPanel.Peek().UiType]);
			stackPanel.Peek().OnContinue();
		}
	}

	public BasePanel Peek()
	{
		return stackPanel.Peek();
	}

	public void SetPanelActive(UIType type, bool value)
	{
		GameObject panel = UIManager.Instance.GetDicUI()[type];
		panel.SetActive(value);
	}
}