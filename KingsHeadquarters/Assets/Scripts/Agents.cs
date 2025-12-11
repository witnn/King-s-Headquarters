using UnityEngine;

public class Agents : MonoBehaviour
{   
    public ResourceSystem kaynaklar;
    public Animator animator;

	public string helloText;
	private SpeakBubble speakBubble;
	float focusPoint;

	[System.Serializable]
	public struct AgentData
	{
        public AgentType _agentType;
        [Space]
        public string veryPositive;
        public string positive;
        public string littlePositive;
        public string veryLittlePositive;
        [Space]
		public string veryNegative;
		public string negative;
		public string littleNegative;
		public string veryLittleNegative;

		public AgentData(
	   AgentType agentType,
	   string veryPositive,
	   string positive,
	   string littlePositive,
	   string veryLittlePositive,
	   string veryNegative,
	   string negative,
	   string littleNegative,
	   string veryLittleNegative
		)
		{
			this._agentType = agentType;

			this.veryPositive = veryPositive;
			this.positive = positive;
			this.littlePositive = littlePositive;
			this.veryLittlePositive = veryLittlePositive;

			this.veryNegative = veryNegative;
			this.negative = negative;
			this.littleNegative = littleNegative;
			this.veryLittleNegative = veryLittleNegative;
		}

	}

	public void SetTalking(bool t)
	{
		animator.SetTrigger("happy");
		speakBubble.SetString(LanguageManager.Instance.GetText(helloText));
	}

	public AgentData data;

	public enum AgentType
    {
        Priest,
        General,
        Guild_Master,
        Treasurer,
		_null
	}
    private AgentType agentType;

	private void Start()
	{
		speakBubble = transform.GetChild(0).GetComponent<SpeakBubble>();
		if(animator != null)
		{
			animator.SetFloat("idleSpeed", Random.Range(0.8f, 1.2f));
		}
			
	}

	public void Reaction(bool positive, AgentType type)
    {
		if (type != data._agentType)
		{
			return;
		}
		switch (type)
        {
			case AgentType.Priest:
				focusPoint = kaynaklar.religion;
				break;
			case AgentType.General:
				focusPoint = kaynaklar.military;
				break;
			case AgentType.Guild_Master:
				focusPoint = kaynaklar.happiness;
				break;
			case AgentType.Treasurer:
				focusPoint = kaynaklar.money;
				break;
		}
		if (positive)
		{
			if (animator != null)
			{
				animator.SetTrigger("happy");
			}
			Debug.Log("Focus Point: " + focusPoint);
			switch (focusPoint)
			{
				case > 75:
					
					Debug.Log(data.veryPositive);
					speakBubble.SetString(LanguageManager.Instance.GetText(data.veryPositive));
					break;
				case > 50:
					Debug.Log(data.positive);
					speakBubble.SetString(LanguageManager.Instance.GetText(data.positive));
					break;
				case > 25:
					Debug.Log(data.littlePositive);
					speakBubble.SetString(LanguageManager.Instance.GetText(data.littlePositive));
					break;
				default:
					Debug.Log(data.veryLittlePositive);
					speakBubble.SetString(LanguageManager.Instance.GetText(data.veryLittlePositive));
					break;
			}
		}
		else
		{
			Debug.Log("Focus Point: " + focusPoint);
			if (animator != null)
			{
				animator.SetTrigger("sad");
			}

			switch (focusPoint)
			{
				case > 75:
					Debug.Log(data.veryLittleNegative);
					speakBubble.SetString(LanguageManager.Instance.GetText(data.veryLittleNegative));
					break;
				case > 50:
					Debug.Log(data.littleNegative);
					speakBubble.SetString(LanguageManager.Instance.GetText(data.littleNegative));
					break;
				case > 25:
					Debug.Log(data.negative);
					speakBubble.SetString(LanguageManager.Instance.GetText(data.negative));
					break;
				default:
					Debug.Log(data.veryNegative);
					speakBubble.SetString(LanguageManager.Instance.GetText(data.veryNegative));
					break;
			}
		}
	}
}
