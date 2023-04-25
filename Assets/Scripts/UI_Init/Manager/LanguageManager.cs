using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageManager
{
	private static readonly LanguageManager instance = new();

	public static LanguageManager Instance { get { return instance; } }

	private List<LanguageText> m_lt;

	private Dictionary<string, string> m_dic_lt;

	public enum LanguageList
	{
		cn,
		en
	}

	private LanguageList m_currentLanguage;

	private LanguageManager()
	{
		m_lt = new List<LanguageText>();
		m_dic_lt = new Dictionary<string, string>();

		m_currentLanguage = LanguageList.en;

		OnLanguageChanged();
	}

	public void RegisterLT(LanguageText lt)
	{
		m_lt.Add(lt);
	}

	public void ChangeLanguage(LanguageList ll)
	{
		if (m_currentLanguage == ll)
		{
			return;
		}

		m_currentLanguage = ll;

		m_dic_lt.Clear();

		OnLanguageChanged();
	}

	public void OnLanguageChanged()
	{
		LoadLanguage();

		foreach(var item in m_lt)
		{
			item.OnLanguageChanged();
		}
	}

	public string GetTextByKey(string key)
	{
		if (m_dic_lt.ContainsKey(key))
		{
			return m_dic_lt[key];
		}
		Debug.LogError("Invalid key!");
		return null;
	}

	public void LoadLanguage()
	{
		TextAsset asset;
		switch (m_currentLanguage)
		{
			case LanguageList.en:
				{
					asset = Resources.Load<TextAsset>("Texts/en");
					break;
				}
			default:
				{
					asset = Resources.Load<TextAsset>("Texts/cn");
					break;
				}
		}
		Stream st = new MemoryStream(asset.bytes);
		StreamReader sr = new(st);
		while (!sr.EndOfStream)
		{
			string line = sr.ReadLine();
			string[] tempStrs = line.Split('=');
			m_dic_lt[tempStrs[0]] = tempStrs[1];
		}
	}
}