using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceSystem : MonoBehaviour
{
    public float religion = 50; // x
    public float military = 50; // y
    public float happiness = 50; // z
    public float money = 50; // t

	public float score = 0;

	public Agents[] agents;

	private DialogSystem diyalog;

	void Start()
	{
		diyalog = GetComponent<DialogSystem>();
		score = 0;
		PlayerPrefs.SetFloat("score", score);
	}


	public void AddReligion(float amount)
    {
        religion += amount;
        if(amount > 0)
        {
			Debug.Log("din arttý");
			foreach(Agents agent in agents)
			{
				agent.Reaction(true, Agents.AgentType.Priest);
			}
		}		
        else
        {
            Debug.Log("din azaldý");
			foreach (Agents agent in agents)
			{
				agent.Reaction(false, Agents.AgentType.Priest);
			}
			if (religion <= 0)
			{
				SceneManager.LoadScene("SonReligion");
			}
		}
	}

    public void AddMilitary(float amount)
    { 
        military += amount;
		if (amount > 0)
		{
			Debug.Log("askeri güç arttý");
			foreach (Agents agent in agents)
			{
				agent.Reaction(true, Agents.AgentType.General);
			}
		}
		else
		{
			Debug.Log("askeri güç azaldý");
			foreach (Agents agent in agents)
			{
				agent.Reaction(false, Agents.AgentType.General);
			}
			if (military<= 0)
			{
				SceneManager.LoadScene("SonAskeri");
			}
		}
		
	}
    public void AddHappiness(float amount)
    { 
        happiness += amount;
		if (amount > 0)
		{
			Debug.Log("mutluluk arttý");
			foreach (Agents agent in agents)
			{
				agent.Reaction(true, Agents.AgentType.Guild_Master);
			}
		}
		else
		{
			Debug.Log("mutluluk azaldý");
			foreach (Agents agent in agents)
			{
				agent.Reaction(false, Agents.AgentType.Guild_Master);
			}
			if (happiness <= 0)
			{
				SceneManager.LoadScene("SonHalk");
			}
		}
		
	}
    public void AddMoney(float amount)   
    { 
        money += amount;
		if (amount > 0)
		{
			Debug.Log("para arttý");
			foreach (Agents agent in agents)
			{
				agent.Reaction(true, Agents.AgentType.Treasurer);
			}

		}
		else
		{
			Debug.Log("para azaldý");
			foreach (Agents agent in agents)
			{
				agent.Reaction(false, Agents.AgentType.Treasurer);
			}
			if(money <= 0)
			{
				SceneManager.LoadScene("SonPara");
			}
		}
		
	}

	


}
