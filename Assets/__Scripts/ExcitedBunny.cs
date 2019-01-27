using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcitedBunny : MonoBehaviour
{
	private Animator animator;
	private HouseComparer houseComparer;
	private int currentScore;
	private int previousScore;

	private FMODUnity.StudioEventEmitter excitedAudio;

	private void Start()
	{
		excitedAudio = AudioManager.Instance.InitializeAudioOnObject(gameObject, "event:/excitedBunny");
		animator = GetComponent<Animator>();
		houseComparer = FindObjectOfType<HouseComparer>();
		currentScore = houseComparer.score;
		MakeIdle();
	}

	private void Update()
	{
		currentScore = houseComparer.score;

		if (currentScore / 10 > previousScore / 10)
		{
			MakeExcited();
		}

		previousScore = currentScore;
	}

	public void MakeExcited()
	{
		animator.SetBool("excited", true);
		Invoke("MakeIdle", 1.5f);
		AudioManager.Instance.PlayEmitterOnce(excitedAudio);
	}

	public void MakeIdle()
	{
		animator.SetBool("excited", false);
	}
}
