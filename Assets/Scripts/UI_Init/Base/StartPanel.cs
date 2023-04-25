using UnityEngine.UI;

public class StartPanel : BasePanel
{
	static readonly string path = "Prefabs/UI_Init/Panels/StartPanel";

	private static readonly StartPanel instance;

	public static StartPanel Instance { get { return instance ?? new StartPanel(); } }

	private StartPanel() : base(new UIType(path)) { }

	public override void OnEnter()
	{
		UITool.Instance.GetOrAddComponentInChildren<Button>("Settings").onClick.AddListener(() =>
		{
			PanelManager.Instance.Push(AudioPanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Select map").onClick.AddListener(() =>
		{
			PanelManager.Instance.SetPanelActive(UiType, false);
			PanelManager.Instance.Push(SelectMapPanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Garage").onClick.AddListener(() =>
		{
			PanelManager.Instance.SetPanelActive(UiType, false);
			PanelManager.Instance.Push(GaragePanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Shop").onClick.AddListener(() =>
		{
			PanelManager.Instance.SetPanelActive(UiType, false);
			PanelManager.Instance.Push(ShopPanelGold.Instance);
		});
	}
}