using UnityEngine.UI;

public class GaragePanel : BasePanel
{
	static readonly string path = "Prefabs/UI_Init/Panels/GaragePanel";
	public readonly string FerrariPath = "Prefabs/UI_Game/Cars/Ferrari";
	public readonly string BenzPath = "Prefabs/UI_Game/Cars/Mercedes Benz";
	public readonly string PorschePath = "Prefabs/UI_Game/Cars/Porsche 918 Spyder";

	private static readonly GaragePanel instance;

	public static GaragePanel Instance { get { return instance ?? new GaragePanel(); } }

	private GaragePanel() : base(new UIType(path)) { }

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

		UITool.Instance.GetOrAddComponentInChildren<Button>("Shop").onClick.AddListener(() =>
		{
			PanelManager.Instance.SetPanelActive(UiType, false);
			PanelManager.Instance.Push(ShopPanelGold.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Settings").onClick.AddListener(() =>
		{
			PanelManager.Instance.Push(AudioPanel.Instance);
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Ferrari").onClick.AddListener(() =>
		{
			UIManager.Instance.UpdateCarInGarage(new UIType(FerrariPath));
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Benz").onClick.AddListener(() =>
		{
			UIManager.Instance.UpdateCarInGarage(new UIType(BenzPath));
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Porsche").onClick.AddListener(() =>
		{
			UIManager.Instance.UpdateCarInGarage(new UIType(PorschePath));
		});
	}

	public string GetDefaultCarPath()
	{
		return PorschePath;
	}
}