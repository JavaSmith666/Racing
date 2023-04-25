using UnityEngine;
using TMPro;

public class LanguageText : MonoBehaviour
{
	public string m_key;

	private TMP_Text m_text;

	private void Start()
	{
		//m_text = GetComponent<Text>();
		m_text = GetComponent<TMP_Text>();
		m_text.text = LanguageManager.Instance.GetTextByKey(m_key);

		LanguageManager.Instance.RegisterLT(this);
	}

	public void OnLanguageChanged()
	{
		m_text.text = LanguageManager.Instance.GetTextByKey(m_key);
	}
}