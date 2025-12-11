using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IK_Look : MonoBehaviour
{
	private Transform player;           // Oyuncunun transform’u
	public float lookAtDistance = 8f;  // Bu mesafeye kadar sana bakar
	public float lookAtWeight = 1f;    // Bakýþ gücü (0-1 arasý)
	public float smoothSpeed = 2f;     // Bakýþý ne kadar yumuþak yapacaðý

	private Animator animator;
	public float currentWeight;
	private  Vector3 lookPos = Vector3.zero;

	void Start()
	{
		animator = GetComponent<Animator>();
		player = Camera.main.gameObject.transform;
	}

	void OnAnimatorIK(int layerIndex)
	{
		if (player == null) return;

		float distance = Vector3.Distance(transform.position, player.position);

		Vector3 playerPos = player.position + Vector3.up * 1.6f;
		Vector3 forwardPos = transform.forward;
		

		if (distance < lookAtDistance)
		{
			lookPos = Vector3.Slerp(lookPos, playerPos, Time.deltaTime * smoothSpeed*2);

			currentWeight = Mathf.Lerp(currentWeight, lookAtWeight, Time.deltaTime * smoothSpeed);
			animator.SetLookAtPosition(lookPos); // kafaya hizalamak için biraz yukarý
			animator.SetLookAtWeight(currentWeight, 0.3f, 0.8f, 0);
		}
		else
		{
			// uzaktaysa bakýþý býrak
			//lookPos = Vector3.Slerp(lookPos, forwardPos, Time.deltaTime * smoothSpeed/6);

			currentWeight = Mathf.Lerp(currentWeight, 0f, Time.deltaTime * smoothSpeed / 6);
			animator.SetLookAtPosition(lookPos); 
			animator.SetLookAtWeight(currentWeight, 0.3f, 0.8f, 0);
		}
	}
}
