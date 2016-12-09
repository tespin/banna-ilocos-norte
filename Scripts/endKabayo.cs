using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Uniduino;

public class endKabayo : MonoBehaviour {

	public Arduino arduino;

	private int AnalogReading;
	private int pin0 = 0;

	private float timeSinceSensorPress;

	private float kabayoTimer;

	private bool sensorPressed;

	// Use this for initialization
	void Start () {

		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);

		timeSinceSensorPress = 0;
		kabayoTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		AnalogReading = arduino.analogRead(pin0);

		CheckInput();

		if (sensorPressed) {
			kabayoTimer = Time.timeSinceLevelLoad - timeSinceSensorPress;
		} else {
			timeSinceSensorPress = Time.timeSinceLevelLoad;
		}
			

		if (kabayoTimer >= 35.0f) {
			StartCoroutine (changeLevel());
		}

		Debug.Log (kabayoTimer);
		
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
			Debug.Log("sensorNotPressed");
		}
	}

	void ConfigurePins() {
		arduino.pinMode(pin0, PinMode.ANALOG);
		arduino.reportAnalog(pin0, 1);
	}

	IEnumerator changeLevel() {
		float fadeTime = GameObject.Find("RawImage").GetComponent<fadeLevel>().BeginFade(4.0f);
		yield return new WaitForSeconds(fadeTime * 215);
//		Application.LoadLevel(0);
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
