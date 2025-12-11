using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;


public class DialogSystem : MonoBehaviour
{
	private ResourceSystem kaynaklar;

	[Header("Attachments")]
	public NPC_Manager npcManager;

	[Header("UI Panels")]
	public GameObject QuestionPanel;
	public TextMeshProUGUI questionText;
	public TextMeshProUGUI op_A_Text;
	public TextMeshProUGUI op_B_Text;

	public float effectAmount = 20f;

	[System.Serializable]
	public struct DiyalogData
	{
		public string question;
		public string optionA;
		public string optionB;

		public DiyalogData(string _question, string _optionA, string _optionB)
		{
			question = _question;
			optionA = _optionA;
			optionB = _optionB;
		}
	}

	public DiyalogData[] diyaloglar;

	private DiyalogData selectedDialog;

	private NPC_System mainNPC;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		kaynaklar = GetComponent<ResourceSystem>();	
	}

	public void SetQuestion()
	{	
		selectedDialog = diyaloglar[Random.Range(0, diyaloglar.Length)];

	}

	private void SelectOption(string option)
	{
		kaynaklar.score += 10;
		PlayerPrefs.SetFloat("score", kaynaklar.score);

		if (option.Contains("x+"))
		{
			kaynaklar.AddReligion(effectAmount);
		}
		if (option.Contains("x-"))
		{
			kaynaklar.AddReligion(-effectAmount);
		}
		if (option.Contains("y+"))
		{
			kaynaklar.AddMilitary(effectAmount);
		}
		if (option.Contains("y-"))
		{
			kaynaklar.AddMilitary(-effectAmount);
		}
		if (option.Contains("z+"))
		{
			kaynaklar.AddHappiness(effectAmount);
		}
		if (option.Contains("z-"))
		{
			kaynaklar.AddHappiness(-effectAmount);
		}
		if (option.Contains("t+"))
		{
			kaynaklar.AddMoney(effectAmount);
		}
		if (option.Contains("t-"))
		{	
			kaynaklar.AddMoney(-effectAmount);
		}
	}

	private string RemovePrefixes(string input)
	{
		// Tüm prefixleri bir diziye koyuyoruz
		string[] prefixes = { "x+", "x-", "y+", "y-", "z+", "z-", "t+", "t-" };

		string output = input;

		foreach (var p in prefixes)
		{
			output = output.Replace(p, "");
		}

		// Kalan boþluklarý da düzeltelim
		return output.Trim();
	}


	public void OpenDialogPanel(bool open)
	{
		if (open)
		{
			
			QuestionPanel.SetActive(true);
			questionText.text = LanguageManager.Instance.GetText(selectedDialog.question);
			op_A_Text.text = RemovePrefixes(LanguageManager.Instance.GetText(selectedDialog.optionA));
			op_B_Text.text = RemovePrefixes(LanguageManager.Instance.GetText(selectedDialog.optionB));
		}
		else
		{
			QuestionPanel.SetActive(false);		
			mainNPC.isTalking = false;
		}
	}

	public void SetMainNPC(NPC_System npc)
	{
		mainNPC = npc;
	}

	public void OptionA()
	{	
		string optionText = LanguageManager.Instance.GetText(selectedDialog.optionA);
		SelectOption(optionText);
		mainNPC.Come(false);
		OpenDialogPanel(false);
		npcManager.SpawnNPC();
		EventSystem.current.SetSelectedGameObject(null);
	}
	public void OptionB()
	{
		string optionText = LanguageManager.Instance.GetText(selectedDialog.optionB);
		SelectOption(optionText);
		mainNPC.Come(false);
		OpenDialogPanel(false);
		npcManager.SpawnNPC();
		EventSystem.current.SetSelectedGameObject(null);
	}
}
