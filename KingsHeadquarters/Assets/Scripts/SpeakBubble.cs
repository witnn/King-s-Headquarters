using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SpeakBubble : MonoBehaviour
{
    public TextMeshPro text;
    private MeshRenderer renderer;
	private Color color = Color.white;

	
	private void Start()
	{
		renderer = GetComponent<MeshRenderer>();
		renderer.enabled = false;
		text.enabled = false;
	}

	private void Update()
	{
		transform.LookAt(Camera.main.transform.position);
		transform.Rotate(0, 180, 0);
	}

	public void SetString(string str)
    {
        text.text = str;
		renderer.enabled = true;
		
		StartCoroutine(ShowBubble());
    }

	IEnumerator ShowBubble()
	{	
		text.enabled = true;
		yield return new WaitForSeconds(4);
		renderer.enabled = false;
		text.enabled = false;
	}
}
