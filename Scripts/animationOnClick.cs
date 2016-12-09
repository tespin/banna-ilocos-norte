using UnityEngine;
using System.Collections;
using System.IO.Ports;
using Uniduino;

public class animationOnClick : MonoBehaviour {

	public Arduino arduino;

	private int AnalogReading;
	private int pin0 = 0;

	private Animator anim;

	private AudioSource grateAudio;

	private bool sensorPressed;
	private bool grating;
	// private bool mouseDown;

	// Use this for initialization
	void Start () {

		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);

		anim = GetComponent<Animator> ();
		grateAudio = GetComponent<AudioSource> ();

		grating = false;
		// mouseDown = false;
	}

	// Update is called once per frame
	void Update () {

		AnalogReading = arduino.analogRead(pin0);

		CheckInput();

		if (sensorPressed) {
			grating = true;
			// mouseDown = true;
		} 
		else {
			grating = false;
			// mouseDown = false;
		}

		anim.SetBool ("grating", grating);

		if (grating == true) {
			if (!grateAudio.isPlaying) {
				grateAudio.Play ();
			}
		} else if (grating == false) {
			grateAudio.Stop ();
		}


		// analogReading = sp.ReadByte ();
		// Debug.Log (analogReading);

		// if (sp.IsOpen) {

		// 	if (analogReading == 1) {
		// 		grating = true;
		// 	} else {
		// 		grating = false;
		// 	}

		// 	try {
		// 		//playAnimation();
		// 	}
		// 	catch(System.Exception) {
		// 		throw;
		// 	}

		// 	if (grating == true) {
		// 		if (!grateAudio.isPlaying) {
				
		// 			grateAudio.Play ();
		// 		}
		// 	} else if (grating == false) {
		// 		grateAudio.Stop ();
		// 	}

		// 	anim.SetBool ("grating", grating);
		// }
	}

	void CheckInput() {
		if (AnalogReading > 100)
		{
			sensorPressed = true;
		}
		else 
		{
			sensorPressed = false;
		}
	}

	void ConfigurePins() {
		arduino.pinMode(pin0, PinMode.ANALOG);
		arduino.reportAnalog(pin0, 1);
	}

}
