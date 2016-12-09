using UnityEngine;
using System.Collections;
using Uniduino;

public class testForceSensor : MonoBehaviour {

	public Camera TestCamera1;

	public Arduino arduino;

	private int AnalogReading;
	private int pin0 = 0;

	// Use this for initialization
	void Start () {

		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
	
	}

	void ConfigurePins() {
		arduino.pinMode(pin0, PinMode.ANALOG);
		arduino.reportAnalog(pin0, 1);
	}
	
	// Update is called once per frame
	void Update () {

		AnalogReading = arduino.analogRead(pin0); 
		Debug.Log(AnalogReading);

		if (AnalogReading > 600)
		{
			TestCamera1.enabled = true;
		}
		else
		{
			TestCamera1.enabled = false;
		}

	}
}
