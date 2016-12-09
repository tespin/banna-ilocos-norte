using UnityEngine;
using System.Collections;

public class playAswangSounds : MonoBehaviour {

	public AudioClip[] aswangSounds;

	private AudioSource aswangSource;

	private float soundInterval;

	private bool play;

	// Use this for initialization
	void Start () {
		aswangSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		play = true;
		// PlayAudio();
		StartCoroutine(PlayAudio());
	}

	IEnumerator PlayAudio() {

		int n = Random.Range(1, aswangSounds.Length);

		aswangSource.clip = aswangSounds[n];

		if (!aswangSource.isPlaying)
		{
			aswangSource.PlayOneShot(aswangSource.clip);
		}
		else if (aswangSource.isPlaying)
		{
			yield return new WaitForSeconds(0.5f);
			aswangSource.Stop();
		}

		aswangSounds[n] = aswangSounds[0];
		aswangSounds[0] = aswangSource.clip;

		yield return null;



		// if (play == true)
		// {
		// 	if (!aswangSource.isPlaying)
		// 	{
		// 		aswangSource.PlayOneShot(aswangSource.clip);
		// 	}
		// 	play = false;
		// }
		// else if (play == false)
		// {
		// 	aswangSource.Stop();
		// 	aswangSounds[n] = aswangSounds[0];
		// 	aswangSounds[0] = aswangSource.clip;
		// }
	}
}
