using UnityEngine.UI;

public class AudioPanel : BasePanel
{
	static readonly string path = "Prefabs/UI_Init/Panels/AudioPanel";

	private static readonly AudioPanel instance;

	public static AudioPanel Instance { get { return instance ?? new AudioPanel(); } }

	private AudioPanel() : base(new UIType(path)) { }

	public override void OnEnter()
	{
		UITool.Instance.GetOrAddComponentInChildren<Button>("Close").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Control").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
			PanelManager.Instance.Push(ControlPanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Language").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
			PanelManager.Instance.Push(LanguagePanel.Instance);
		});
	}
}