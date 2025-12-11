using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
	public string key;

	void Start()
	{
		UpdateText();
	}

	public void UpdateText()
	{
		var t = GetComponent<TMP_Text>();
		t.text = LanguageManager.Instance.GetText(key);
	}
}
