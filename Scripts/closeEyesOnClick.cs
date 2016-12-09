using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class closeEyesOnClick : MonoBehaviour {

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

	private float aswangInterval;
	private float aswangPastTime;
	private float aswangCurrentTime;
	private float aswangTimer;

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

		aswangInterval = 9.0f;
	}
	
	// Update is called once per frame
	void Update () {

		aswangCurrentTime = Time.timeSinceLevelLoad;

		if (aswangCurrentTime >= aswangPastTime + aswangInterval)
		{
			// instructionsText.enabled = true;
			canSleep = true;

			// FadeOutOnClick();

			if (canSleep)
			{
				if(Input.GetMouseButton(0))
				{
					mouseDown = true;
				}
				else 
				{
					mouseDown = false;
				}
			}

		}

		FadeOutOnClick();

		aswangAnim.SetBool("sleeping", sleeping);
		// anim.SetBool("fadeIn", fadeIn);

		Debug.Log(canSleep + ", " + Time.timeSinceLevelLoad);

	}

	void FadeOutOnClick() {

		if (canSleep == true && !mouseDown)
		{ 
			fadeIn = true;
			fadeDir = -0.8f;
			sleeping = false;
		}
		else if (canSleep && mouseDown)
		{
			fadeIn = false;
			fadeDir = 8.0f;
			sleeping = true;
		}

		fadeLevelScript.BeginFade(fadeDir);
	}
}
