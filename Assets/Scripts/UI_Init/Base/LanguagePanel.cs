using UnityEngine.UI;

public class LanguagePanel : BasePanel
{
	static readonly string path = "Prefabs/UI_Init/Panels/LanguagePanel";

	private static readonly LanguagePanel instance = new();

	public static LanguagePanel Instance { get { return instance ?? new LanguagePanel(); } }

	private LanguagePanel() : base(new UIType(path)) { }

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

		UITool.Instance.GetOrAddComponentInChildren<Button>("Control").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
			PanelManager.Instance.Push(ControlPanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Cn").onClick.AddListener(() =>
		{
			LanguageManager.Instance.ChangeLanguage(LanguageManager.LanguageList.cn);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("En").onClick.AddListener(() =>
		{
			LanguageManager.Instance.ChangeLanguage(LanguageManager.LanguageList.en);
		});
	}
}