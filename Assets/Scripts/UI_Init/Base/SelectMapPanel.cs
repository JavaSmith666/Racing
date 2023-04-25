using UnityEngine.UI;

public class SelectMapPanel : BasePanel
{
	static readonly string path = "Prefabs/UI_Init/Panels/SelectMapPanel";
	public readonly string MiddleEasternDesertPath = "Images/UI_SelectMap/middle_eastern_desert_2";
	public readonly string MountCookPath = "Images/UI_SelectMap/mount_cook_2";
	public readonly string RockyMountainsPath = "Images/UI_SelectMap/rocky_mountains_2";

	private static readonly SelectMapPanel instance;

	public static SelectMapPanel Instance { get { return instance ?? new SelectMapPanel(); } }

	private SelectMapPanel() : base(new UIType(path)) { }

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

		UITool.Instance.GetOrAddComponentInChildren<Button>("Middle Eastern Desert").onClick.AddListener(() =>
		{
			UIManager.Instance.UpdateBackgroundInSelectMap(new UIType(MiddleEasternDesertPath));
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Mount Cook").onClick.AddListener(() =>
		{
			UIManager.Instance.UpdateBackgroundInSelectMap(new UIType(MountCookPath));
		});

		UITool.Instance.GetOrAddComponentInChildren<Button>("Rocky Mountains").onClick.AddListener(() =>
		{
			UIManager.Instance.UpdateBackgroundInSelectMap(new UIType(RockyMountainsPath));
		});
	}

	public string GetDefaultBackgroundPath()
	{
		return RockyMountainsPath;
	}
}