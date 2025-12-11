using System.Collections;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{   
    public GameObject npc;
    public Transform spawnPoint;
	public Agents[] agents;

	private bool tutorial = false;

	void Start()
    {
		if (PlayerPrefs.GetInt("tutorial") == 1)
		{
			tutorial = true;
		}
		else if (PlayerPrefs.GetInt("tutorial") == 0)
		{
			tutorial = false;
		}
		if (tutorial == true)
		{
			StartCoroutine(GameEntry());
		}		
		else
		{
			SpawnNPC();
		}		
	}

	IEnumerator GameEntry()
    {
		yield return new WaitForSeconds(2);
		agents[0].SetTalking(true);
        yield return new WaitForSeconds(5);
		agents[1].SetTalking(true);
		yield return new WaitForSeconds(5);
		agents[2].SetTalking(true);
		yield return new WaitForSeconds(5);
		agents[3].SetTalking(true);
		yield return new WaitForSeconds(5);
		agents[4].SetTalking(true);
		
		yield return new WaitForSeconds(6);
		agents[4].gameObject.SetActive(false);
		SpawnNPC();
	}

	public void SpawnNPC()
    {
        Instantiate(npc, spawnPoint.position, spawnPoint.rotation);
	}
}
