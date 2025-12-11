using UnityEngine;

public class CharacterRandomization : MonoBehaviour
{
	public GameObject hair;
	public GameObject moustache;
	public GameObject beard;
	public GameObject goatBeard;

	public GameObject shirt;

	private bool biyik;
	private bool sac;
	private bool sakal;
	private bool topSakal;

	private void Start()
	{
		biyik = Random.value > 0.5f;
		sac = Random.value > 0.5f;
		sakal = Random.value > 0.5f;
		topSakal = Random.value > 0.5f;

		if (hair != null)
		{
			hair.SetActive(sac);
		}
		if (moustache != null)
		{
			moustache.SetActive(biyik);
		}
		if (beard != null)
		{
			beard.SetActive(sakal);
		}
		if (goatBeard != null)
		{
			goatBeard.SetActive(topSakal);
		}

		Color hairColor = GetRandomHairColor();


		moustache.GetComponent<SkinnedMeshRenderer>().material.color = hairColor;
		beard.GetComponent<SkinnedMeshRenderer>().material.color = hairColor;
		hair.GetComponent<SkinnedMeshRenderer>().material.color = hairColor;

		for (int i = 0; i < goatBeard.transform.childCount; i++)
		{
			Transform child = goatBeard.transform.GetChild(i);
			Renderer childRenderer = child.GetComponent<Renderer>();
			if (childRenderer != null)
			{
				childRenderer.material.color = hairColor;
			}
		}

		if (shirt != null)
		{
			Renderer shirtRenderer = shirt.GetComponent<Renderer>();
			if (shirtRenderer != null)
			{
				shirtRenderer.material.color = GetRandomShirtColor();
			}
		}

	}
	Color GetRandomHairColor()
	{
		// Temel saç renkleri
		Color black = new Color(0.05f, 0.05f, 0.05f);   // Siyah
		Color brown = new Color(0.36f, 0.22f, 0.09f);   // Kahverengi
		Color blonde = new Color(0.9f, 0.8f, 0.45f);     // Sarı

		// 0 = siyah, 0.5 = kahverengi, 1 = sarı
		float t = Random.value;

		if (t < 0.5f)
			return Color.Lerp(black, brown, t * 2f);       // Siyah → Kahverengi
		else
			return Color.Lerp(brown, blonde, (t - 0.5f) * 2f); // Kahverengi → Sarı
	}
	Color GetRandomShirtColor()
	{
		Color darkBrown = new Color(0.25f, 0.15f, 0.05f);  // koyu kahverengi
		Color strawYellow = new Color(0.80f, 0.72f, 0.35f);  // saman sarısı
		Color grey = new Color(0.55f, 0.55f, 0.55f);  // doğal gri

		float t = Random.value;

		if (t < 0.5f)
		{
			// Koyu kahverengi → saman sarısı
			float tt = t / 0.5f;
			return Color.Lerp(darkBrown, strawYellow, tt);
		}
		else
		{
			// Saman sarısı → gri
			float tt = (t - 0.5f) / 0.5f;
			return Color.Lerp(strawYellow, grey, tt);
		}
	}

}
