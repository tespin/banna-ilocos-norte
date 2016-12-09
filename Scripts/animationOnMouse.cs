using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class animationOnMouse : MonoBehaviour {

	private Animator anim;

	private float mouseResistance;

	private AudioSource grateAudio;

	private bool receivingInput;
	private bool grating;

	private float rate = 4.5f;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		grateAudio = GetComponent<AudioSource> ();

		grating = false;
		// mouseDown = false;
	}

	// Update is called once per frame
	void Update () {

		CheckInput ();
		ReceiveInput();
		Debug.Log (mouseResistance);

		if (receivingInput) {
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

		// receive input and clamp values
		if (Input.GetMouseButtonDown (0)) 
		{
			mouseResistance++;
		}

		mouseResistance = mouseResistance - rate * Time.deltaTime;
		mouseResistance = Mathf.Clamp (mouseResistance, 0, 2);

	}

	void ReceiveInput() {
		if (mouseResistance > 0.1f) {
			receivingInput = true;
		} else {
			receivingInput = false;
		}

	}

}
