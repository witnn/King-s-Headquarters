using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LastScreen : MonoBehaviour
{
	public TextMeshProUGUI skor;
	private void Start()
	{	
		
		skor.text = PlayerPrefs.GetFloat("score").ToString();
	}


	public void MainMenu(string deger)
	{
		SceneManager.LoadScene(deger);
	}
}
