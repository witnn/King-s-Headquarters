using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NPC_System : MonoBehaviour
{	
	private NavMeshAgent agent;
	private DialogSystem diyalog;
	
	private Transform targetPosition;
	private Transform spawnPosition;
	
	private bool goingToThrone = true;
	public bool isTalking = false;
	private bool isMoving = false;
	
	private Animator animator;
	public bool work = true;

	public AudioClip konusma;
	AudioSource source;

	private void Start()
	{
		Attachments();
		Come(true);
		source = GetComponent<AudioSource>();
	}

	private void Attachments()
	{
		diyalog = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogSystem>();
		targetPosition = GameObject.FindGameObjectWithTag("TargetPosition").transform;
		spawnPosition = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
		agent = GetComponent<NavMeshAgent>();
		animator = transform.GetChild(0).GetComponent<Animator>();
		diyalog.SetMainNPC(this);
	}

	public void Come(bool come)
	{	
		if(work == false) { return; }
		if(come == true)
		{
			agent.SetDestination(targetPosition.position);
			goingToThrone = true;
		}
		else
		{
			agent.SetDestination(spawnPosition.position);
			goingToThrone = false;
		}
	}

	private void Update()
	{
		HandleNPC();
		RotateToCameraWhenArrived();
	}

	private void RotateToCameraWhenArrived()
	{
		if (!agent.pathPending && agent.remainingDistance <= 0.5f)
		{
			// Kamera pozisyonuna doðru bakýlacak yön
			Vector3 lookPos = Camera.main.transform.position - transform.position;
			lookPos.y = 0; // Yukarý eðilmesin, sadece yatay eksende dönsün

			if (lookPos.sqrMagnitude > 0.01f)
			{
				Quaternion targetRot = Quaternion.LookRotation(lookPos);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 5);
			}
		}
	}

	private void HandleNPC()
	{	
		if(work) {
			animator.SetBool("move", agent.velocity.magnitude > 0.1f);
			animator.SetBool("talk", isTalking);
			}


			if (goingToThrone == true)
		{	
			if (agent.remainingDistance < 0.1f)
			{	
				if(isTalking == false)
				{
					isTalking = true;
					HandeDialogue();
					diyalog.OpenDialogPanel(true);
					source.pitch = Random.Range(0.8f, 1.2f);
					source.PlayOneShot(konusma);
				}
				
			}
		}
		if (goingToThrone == false)
		{
			
			if (Vector3.Distance(transform.position, spawnPosition.position) < 1)
			{
				Destroy(gameObject,1);
			}
		}
	}

	private void HandeDialogue()
	{
		diyalog.SetQuestion();
	}

}
