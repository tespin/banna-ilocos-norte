using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class closeEyesOnMouse : MonoBehaviour {

	private bool receivingInput;

	private fadeLevel fadeLevelScript;

	private Animator anim;

	private GameObject aswang;
	private Animator aswangAnim;

	// private Text instructionsText;

	public float fadeDir;
	private float mouseResistance;

	private bool canSleep;
	private bool mouseDown;
	private bool fadeIn;

	private bool sleeping;
	private bool closingEyes;

	private float aswangInterval;
	private float aswangPastTime;
	private float aswangCurrentTime;
	private float aswangTimer;

	private float rate = 1.8f;

	// Use this for initialization
	void Start () {

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

		CheckInput ();
		ReceiveInput ();

		aswangCurrentTime = Time.timeSinceLevelLoad;

		if (aswangCurrentTime >= aswangPastTime + aswangInterval)
		{
			// instructionsText.enabled = true;
			canSleep = true;

			// FadeOutOnClick();

			if (canSleep)
			{
				if(receivingInput)
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

	void ReceiveInput() {
		if (mouseResistance > 30) {
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
