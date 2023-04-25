using UnityEngine.UI;

public class ControlPanel : BasePanel
{
	static readonly string path = "Prefabs/UI_Init/Panels/ControlPanel";

	private static readonly ControlPanel instance;

	public static ControlPanel Instance { get { return instance ?? new ControlPanel(); } }

	private ControlPanel() : base(new UIType(path)) { }

	public override void OnEnter()
	{
		UITool.Instance.GetOrAddComponentInChildren<Button>("Close").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Audio").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
			PanelManager.Instance.Push(AudioPanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Language").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
			PanelManager.Instance.Push(LanguagePanel.Instance);
		});
	}
}