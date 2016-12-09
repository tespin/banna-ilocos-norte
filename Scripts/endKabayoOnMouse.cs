using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class endKabayoOnMouse : MonoBehaviour {

	private float mouseResistance;

	private bool receivingInput;

	private float timeSinceSensorPress;

	private float kabayoTimer;

	private float rate = 1.8f;

	// Use this for initialization
	void Start () {
		timeSinceSensorPress = 0;
		kabayoTimer = 0;
	}

	// Update is called once per frame
	void Update () {

		CheckInput ();
		ReceiveInput();

		if (receivingInput) {
			kabayoTimer = Time.timeSinceLevelLoad - timeSinceSensorPress;
		} else {
			timeSinceSensorPress = Time.timeSinceLevelLoad;
		}


		if (kabayoTimer >= 35.0f) {
			StartCoroutine (changeLevel());
		}

		Debug.Log (kabayoTimer);

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


	IEnumerator changeLevel() {
		float fadeTime = GameObject.Find("RawImage").GetComponent<fadeLevel>().BeginFade(4.0f);
		yield return new WaitForSeconds(fadeTime * 215);
		//		Application.LoadLevel(0);
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
