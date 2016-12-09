using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Uniduino;

public class returnToStart : MonoBehaviour {

	public Arduino arduino;

	private int AnalogReading;
	private int pin0 = 0;

	private bool sensorPressed;

	private float timeSince;
	private float levelTimer;

	// Use this for initialization
	void Start () {
	
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);

		sensorPressed = false;

		timeSince = 0; 
		levelTimer = 0;

	}
	
	// Update is called once per frame
	void Update () {

		AnalogReading = arduino.analogRead(pin0);

		CheckInput();

		levelTimer = Time.timeSinceLevelLoad;

		if (sensorPressed)
		{
			timeSince = Time.timeSinceLevelLoad;
		}
		else 
		{
			levelTimer = Time.timeSinceLevelLoad - timeSince;
		}

		LoadStart();

	}

	void CheckInput() {

		if (AnalogReading > 100)
		{
			sensorPressed = true;
			Debug.Log("sensorPressed");
		}
		else
		{
			sensorPressed = false;
			Debug.Log("notPressed");
		}

		Debug.Log(levelTimer + ", " + sensorPressed);

	}

	void ConfigurePins() {
		arduino.pinMode(pin0, PinMode.ANALOG);
		arduino.reportAnalog(pin0, 1);
	}

	void LoadStart() {
		if (sensorPressed == false && levelTimer >= 60.0f)
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
