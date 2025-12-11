using UnityEngine;

/// <summary>
/// Hafif otonom kamera hareketi: Perlin noise ile yumuþak yaw/pitch (saða-sola / yukarý-aþaðý)
/// ve isteðe baðlý "breathing" (pozisyon bob) oluþturur.
/// Attach this to your Camera (use localRotation/localPosition).
/// </summary>
[DisallowMultipleComponent]
public class CameraBreath : MonoBehaviour
{
	[Header("Rotation (degrees)")]
	[Tooltip("Yaw (y ekseni) maksimum açýsý, sað-sol")]
	public float yawAmplitude = 1.8f;
	[Tooltip("Pitch (x ekseni) maksimum açýsý, yukarý-aþaðý")]
	public float pitchAmplitude = 1.0f;
	[Tooltip("Hareket hýzýný kontrol eder (daha büyük => daha hýzlý)")]
	public float rotationSpeed = 0.6f;

	[Header("Breathing / Position bob (local)")]
	[Tooltip("Pozisyon bob (metre) — 0 kapatýr")]
	public float bobAmplitude = 0.02f;
	[Tooltip("Bob hýzý")]
	public float bobSpeed = 0.9f;

	[Header("Smoothing")]
	[Tooltip("Daha yüksek => daha pürüzsüz (daha geç takip eder)")]
	public float smoothness = 4f;

	[Header("Optional")]
	[Tooltip("Eðer true ise sadece yaw (y) uygula, pitch kapalý")]
	public bool yawOnly = false;

	// iç state
	private Quaternion baseLocalRot;
	private Vector3 baseLocalPos;
	private float seedYaw, seedPitch, seedBob;

	void Start()
	{
		baseLocalRot = transform.localRotation;
		baseLocalPos = transform.localPosition;
		seedYaw = Random.Range(0f, 1000f);
		seedPitch = Random.Range(0f, 1000f);
		seedBob = Random.Range(0f, 1000f);
	}

	void Update()
	{
		float t = Time.time;

		// Perlin tabanlý pürüzsüz deðerler [-1..1]
		float rawYaw = (Mathf.PerlinNoise(seedYaw, t * rotationSpeed) - 0.5f) * 2f;
		float rawPitch = (Mathf.PerlinNoise(seedPitch, t * rotationSpeed * 1.1f) - 0.5f) * 2f;

		float yaw = rawYaw * yawAmplitude;
		float pitch = yawOnly ? 0f : rawPitch * pitchAmplitude;

		Quaternion target = baseLocalRot * Quaternion.Euler(pitch, yaw, 0f);

		// yumuþak dönüþ
		transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smoothness);

		// pozisyon bob (breathing)
		if (bobAmplitude > 0f)
		{
			// basit sinüs ile bob (Perlin de kullanabilirsin)
			float bob = Mathf.Sin(t * bobSpeed + seedBob) * bobAmplitude;
			// lokal forward ve up karýþýmý hafif derinlik hissi için:
			Vector3 offset = transform.localRotation * (Vector3.forward * bob * 0.35f) + transform.localRotation * (Vector3.up * bob * 0.65f);
			transform.localPosition = Vector3.Lerp(transform.localPosition, baseLocalPos + offset, Time.deltaTime * smoothness);
		}
		else
		{
			// bob kapalýysa pozisyonu stabilize et
			transform.localPosition = Vector3.Lerp(transform.localPosition, baseLocalPos, Time.deltaTime * smoothness);
		}
	}

	// Editörde base pozisyon/rot kaydetme butonu happy-to-have deðil ama
	// runtime'da resetlemek istersen:
	public void ResetBasePose()
	{
		baseLocalRot = transform.localRotation;
		baseLocalPos = transform.localPosition;
	}
}
