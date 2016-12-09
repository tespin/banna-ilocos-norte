using UnityEngine;
using System.Collections;
using Uniduino;

public class waitOnSensorPress : MonoBehaviour {

	public Arduino arduino;

	private int AnalogReading;
	private int pin0 = 0;

	private bool sensorPressed;

	private Animator anim;

	private int analogReading;

	public bool waiting;

	// Use this for initialization
	void Start () {

		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);

		anim = GetComponent<Animator>();

		waiting = false;

	}
	
	// Update is called once per frame
	void Update () {

		AnalogReading = arduino.analogRead(pin0);

		CheckInput();

		if (sensorPressed)
		{
			waiting = true;
		}
		else 
		{
			waiting = false;
		}

		Debug.Log(waiting);

		anim.SetBool("waiting", waiting);
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

}
