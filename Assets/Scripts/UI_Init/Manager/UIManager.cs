using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
	private static readonly UIManager instance = new();

	public static UIManager Instance { get { return instance; } }  //singleton

	private Dictionary<UIType, GameObject> dicUI;

	public UIType ActiveCarInGarage { get; private set; }

	public UIType ActiveBackgroundInSelectMap { get; private set; }

	private UIManager()
	{
		dicUI = new Dictionary<UIType, GameObject>();

		//show the default car in garage.
		UIType carType = new(GaragePanel.Instance.GetDefaultCarPath());
		GameObject car = GameObject.Instantiate(Resources.Load<GameObject>(carType.Path));
		car.name = carType.Name;
		car.transform.position = new Vector3(-9, 0, 0);
		car.transform.rotation = Quaternion.Euler(new Vector3(0, -135, 0));
		car.transform.localScale = new Vector3(1, 1, 1);
		dicUI[carType] = car;
		ActiveCarInGarage = carType;

		//show the default background in SelectMapPanel
		UIType backgroundType = new(SelectMapPanel.Instance.GetDefaultBackgroundPath());
	}

	public ref Dictionary<UIType,GameObject> GetDicUI()
	{
		return ref dicUI;
	}

	public GameObject GetSingleUI(UIType type, string parentPath = "")
	{
		GameObject parent = null;
		if (parentPath != "")
		{
			parent = GameObject.Find(parentPath);
			if (parent == null)
			{
				Debug.LogError("Parent GameObject not found!");
				return null;
			}
		}
		if (dicUI.ContainsKey(type))
		{
			return dicUI[type];
		}
		GameObject ui;
		if(parentPath == "")
		{
			ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path));
		}
		else
		{
			ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
		}
		ui.name = type.Name;
		dicUI[type] = ui;
		return ui;
	}

	public void UpdateCarInGarage(UIType type)
	{
		//destroy the old car
		DestroyUI(ActiveCarInGarage);

		//construct the new car
		GameObject car = GetSingleUI(type);
		if(type.Path == GaragePanel.Instance.FerrariPath)
		{
			car.transform.position = new Vector3(-9, 0.46f, 0);
			car.transform.rotation = Quaternion.Euler(new Vector3(0, -135, 0));
			car.transform.localScale = new Vector3(0.085f, 0.085f, 0.085f);
		}
		else if(type.Path == GaragePanel.Instance.BenzPath)
		{
			car.transform.position = new Vector3(-9, 0, 0);
			car.transform.rotation = Quaternion.Euler(new Vector3(-90, -135, 0));
			car.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
		}
		else
		{
			car.transform.position = new Vector3(-9, 0, 0);
			car.transform.rotation = Quaternion.Euler(new Vector3(0, -135, 0));
			car.transform.localScale = new Vector3(1, 1, 1);
		}

		ActiveCarInGarage = type;
	}

	public void UpdateBackgroundInSelectMap(UIType type)
	{
		Sprite background = Resources.Load<Sprite>(type.Path);
		UITool.Instance.GetOrAddComponentInChildren<Image>("Background").sprite = background;
		ActiveBackgroundInSelectMap = type;
	}

	public void DestroyUI(UIType type)
	{
		if (dicUI.ContainsKey(type))
		{
			GameObject.Destroy(dicUI[type]);
			dicUI.Remove(type);
		}
	}
}