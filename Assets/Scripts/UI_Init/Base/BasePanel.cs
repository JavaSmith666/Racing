using UnityEngine;

public class BasePanel {
	public UIType UiType { get; private set; }

	public BasePanel(UIType type)
	{
		UiType = type;
	}

	public virtual void OnEnter() { }
	
	public virtual void OnPasue() 
	{
		UITool.Instance.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public virtual void OnContinue()
	{
		UITool.Instance.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public virtual void OnExit()
	{
		UIManager.Instance.DestroyUI(UiType);
	}
}