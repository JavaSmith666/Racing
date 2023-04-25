using UnityEngine.UI;

public class ShopPanelDiamond : BasePanel
{
	static readonly string path = "Prefabs/UI_Init/Panels/ShopPanelDiamond";

	private static readonly ShopPanelDiamond instance;

	public static ShopPanelDiamond Instance { get { return instance ?? new ShopPanelDiamond(); } }

	private ShopPanelDiamond() : base(new UIType(path)) { }

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

		UITool.Instance.GetOrAddComponentInChildren<Button>("ShopGold").onClick.AddListener(() =>
		{
			PanelManager.Instance.Pop();
			PanelManager.Instance.Push(ShopPanelGold.Instance);
		});
	}
}