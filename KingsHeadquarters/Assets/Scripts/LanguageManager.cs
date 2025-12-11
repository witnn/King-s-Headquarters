using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LanguageManager : MonoBehaviour
{
	public static LanguageManager Instance;

	private Dictionary<string, string> localizedText;
	public string currentLanguage;

	void Awake()
	{	
		if(PlayerPrefs.HasKey("language") == false)
		{
			PlayerPrefs.SetString("language","en");
		}
		currentLanguage = PlayerPrefs.GetString("language");

		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		LoadLanguage(currentLanguage);
	}

	public void LoadLanguage(string lang)
	{
		currentLanguage = lang;

		var file = Resources.Load<TextAsset>($"Languages/{lang}");
		localizedText = JsonUtility.FromJson<LanguageData>(file.text).ToDictionary();
	}

	public string GetText(string key)
	{
		if (localizedText.ContainsKey(key))
			return localizedText[key];

		return $"#{key}";
	}
}

[System.Serializable]
public class LanguageData
{
	public Entry[] entries;

	public Dictionary<string, string> ToDictionary()
	{
		Dictionary<string, string> dict = new Dictionary<string, string>();
		foreach (var e in entries)
			dict[e.key] = e.value;
		return dict;
	}
}

[System.Serializable]
public class Entry
{
	public string key;
	public string value;
}
