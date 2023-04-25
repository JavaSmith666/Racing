using UnityEngine.UI;

public class ShopPanelGold : BasePanel
{
	static readonly string path = "Prefabs/UI_Init/Panels/ShopPanelGold";

	private static readonly ShopPanelGold instance;

	public static ShopPanelGold Instance { get { return instance ?? new ShopPanelGold(); } }

	private ShopPanelGold() : base(new UIType(path)) { }

	public override void OnEnter()
	{
		UITool.Instance.GetOrAddComponentInChildren<Button>("Back").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
			PanelManager.Instance.SetPanelActive(PanelManager.Instance.Peek().UiType, true);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Home").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
			PanelManager.Instance.Push(StartPanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Garage").onClick.AddListener(() =>
		{
			PanelManager.Instance.SetPanelActive(UiType, false);
			PanelManager.Instance.Push(GaragePanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Settings").onClick.AddListener(() =>
		{
			PanelManager.Instance.Push(AudioPanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("ShopDiamond").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
			PanelManager.Instance.Push(ShopPanelDiamond.Instance);
		});
	}
}