using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Uniduino;

public class closeEyesOnSensor : MonoBehaviour {

	public Arduino arduino;

	private int AnalogReading;
	private int pin0 = 0;

	private bool sensorPressed;

	private fadeLevel fadeLevelScript;

	private Animator anim;

	private GameObject aswang;
	private Animator aswangAnim;

	// private Text instructionsText;

	public float fadeDir;

	private bool canSleep;
	private bool mouseDown;
	private bool fadeIn;

	private bool sleeping;
	private bool closingEyes;

	private float aswangInterval;
	private float aswangPastTime;
	private float aswangCurrentTime;
	private float aswangTimer;

	// Use this for initialization
	void Start () {

		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);

		aswang = GameObject.Find("Aswang");
		aswangAnim = aswang.GetComponent<Animator>();

		// instructionsText = GameObject.Find("Instructions").GetComponent<Text>();
		// instructionsText.enabled = false;
		// anim = instructionsText.GetComponent<Animator>();

		fadeDir = -0.5f;
		fadeLevelScript = GameObject.Find("RawImage").GetComponent<fadeLevel>();

		aswangPastTime = 0;
		aswangTimer = 0;

		canSleep = false;
		mouseDown = false;
		fadeIn = false;
		sleeping = false;
		closingEyes = false;

		aswangInterval = 9.0f;
	}
	
	// Update is called once per frame
	void Update () {

		AnalogReading = arduino.analogRead(pin0);

		CheckInput();

		aswangCurrentTime = Time.timeSinceLevelLoad;

		if (aswangCurrentTime >= aswangPastTime + aswangInterval)
		{
			// instructionsText.enabled = true;
			canSleep = true;

			// FadeOutOnClick();

			if (canSleep)
			{
				if(sensorPressed)
				{
					closingEyes = true;
				}
				else 
				{
					closingEyes = false;
				}
			}

		}

		FadeOutOnSensorPress();

		aswangAnim.SetBool("sleeping", sleeping);
		// anim.SetBool("fadeIn", fadeIn);

		Debug.Log(canSleep + ", " + Time.timeSinceLevelLoad);

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

	void FadeOutOnSensorPress() {

		if (canSleep == true && !closingEyes)
		{ 
			fadeIn = true;
			fadeDir = -0.8f;
			sleeping = false;
		}
		else if (canSleep && closingEyes)
		{
			fadeIn = false;
			fadeDir = 8.0f;
			sleeping = true;
		}

		fadeLevelScript.BeginFade(fadeDir);
	}
}
