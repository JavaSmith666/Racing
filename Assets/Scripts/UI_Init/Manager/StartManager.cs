using UnityEngine;

public class StartManager : MonoBehaviour
{
	private void Start()
	{
		PanelManager.Instance.Push(StartPanel.Instance);
	}
}
