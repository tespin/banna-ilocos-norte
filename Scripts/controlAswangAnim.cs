using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class controlAswangAnim : MonoBehaviour {

	private fadeLevel fadeLevelScript;

	private GameObject aswang;
	private Animator aswangAnim;

	private bool entered;
	private bool exiting;

	// Use this for initialization
	void Start () {
		aswang = GameObject.Find("Aswang");
		aswangAnim = aswang.GetComponent<Animator>();

		entered = false;
		exiting = false;
	}
	
	// Update is called once per frame
	void Update () {
		//when sphere collider is entered, fade quickly
		// Debug.Log(fadeLevelScript.fadeSpeed);

		aswangAnim.SetBool("entered", entered);

		if (exiting)
		{
			StartCoroutine(changeLevel());
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Aswang")
		{
			entered = true;
		}
	}

	void OnTriggerExit(Collider other) {
		exiting = true;
	}

	IEnumerator changeLevel() {
		float fadeTime = GameObject.Find("RawImage").GetComponent<fadeLevel>().BeginFade(2.0f);
		yield return new WaitForSeconds(fadeTime * 10.0f);
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
