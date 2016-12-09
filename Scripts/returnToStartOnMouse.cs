using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class returnToStartOnMouse : MonoBehaviour {

	private float mouseResistance;

	private bool receivingInput;

	private float timeSince;
	private float levelTimer;

	float rate = 1.8f;

	// Use this for initialization
	void Start () {

		receivingInput = false;

		timeSince = 0; 
		levelTimer = 0;

	}

	// Update is called once per frame
	void Update () {

		ReceiveInput ();
		CheckInput ();

		levelTimer = Time.timeSinceLevelLoad;

		if (receivingInput)
		{
			timeSince = Time.timeSinceLevelLoad;
		}
		else 
		{
			levelTimer = Time.timeSinceLevelLoad - timeSince;
		}

		LoadStart();

	}

	void ReceiveInput() {
		if (mouseResistance > 2) {
			receivingInput = true;
		} else {
			receivingInput = false;
		}

	}

	void CheckInput() {

		// receive input and clamp values
		if (Input.GetMouseButtonDown (0)) 
		{
			mouseResistance++;
		}

		mouseResistance = mouseResistance - rate * Time.deltaTime;
		mouseResistance = Mathf.Clamp (mouseResistance, 0, 100);

	}

	void LoadStart() {
		if (receivingInput == false && levelTimer >= 60.0f)
		{
			Debug.Log("changingLevel");
			StartCoroutine(changeLevel());
		}
	}

	IEnumerator changeLevel() {
		float fadeTime = GameObject.Find("RawImage").GetComponent<fadeLevel>().BeginFade(10.0f);
		yield return new WaitForSeconds(fadeTime * 60);

		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
