using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class displayDialogue : MonoBehaviour {

	public TextAsset textFile; 
	public string[] textLines;
	public Text myText;

	private GameObject textBox;

	public int currentLine;
	public int lastLine;

	private fadeLevel fadeLevelScript;

	private float interval;
	private float pastTime;
	private float currentTime;
	private float timer;

	private float textAlpha = 1.0f;

	private float fadeSpeed = 1.1f;
	private float fadeDir = -1.0f;

	public bool canIterate;
	private bool fallingAsleep;
	public bool fading;

	private Animator anim;

	// Use this for initialization
	void Start () {

		textBox = GameObject.Find("TextBox");

		pastTime = 0;
		timer = 0;
		canIterate = true;
		fading = false;

		anim = GetComponent<Animator>();

		if (textFile != null) 
		{
			textLines = textFile.text.Split ("\n"[0]);
		}

		if (lastLine == 0)
		{
			lastLine = textLines.Length - 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad == 5)
		{
			canIterate = true;
		}

		IterateThroughText();
	}

	void IterateThroughText() {

		if (currentLine > lastLine)
		{
			canIterate = false;
			textBox.SetActive(false);
		}

		if (canIterate == true)
		{
			currentTime = Time.timeSinceLevelLoad;

			float newInterval = 0;
			float pause = 0.4f;

			switch (currentLine)
			{
				//line 1
				case 0:
					//newInterval is longer for game; 7 seconds
					newInterval = 3;
					break;
				//line 2
				case 1:
					newInterval = 6;
					break;
				//line 3
				case 2:
					newInterval = 5;
					break;
				//line 4
				case 3: 
					newInterval = 4;
					break;
				//line 5
				case 4:
					newInterval = 6;
					// pause = 3.0f;
					break;
				//line 6
				case 5:
					newInterval = 4;
					break;
				//line 7
				case 6:
					newInterval = 4;
					break;
				//line 8
				case 7:
					newInterval = 6;
					break;
				//line 9
				case 8:
					newInterval = 7;
					break;
				//line 10
				case 9:
					newInterval = 4;
					break;
				//line 11
				case 10:
					newInterval = 2;
					break;
				//line 12
				case 11:
					newInterval = 8;
					break;
				//line 13
				case 12:
					newInterval = 6;
					break;
				//line 14
				case 13:
					newInterval = 3;
					break;
				//line 15
				case 14: 
					newInterval = 2;
					break;
				//line 16
				case 15:
					newInterval = 3;
					break;
				//line 17
				case 16:
					newInterval = 3;
					break;
				//line 18
				case 17:
					newInterval = 4;
					break;
				//line 19
				case 18:
					newInterval = 4;
					break;
				//line 20
				case 19:
					// newInterval = 10;
					newInterval = 4;
					break;
				//line 21
				case 20: 
					newInterval = 6;
					// canIterate = false;
					// fallingAsleep = true;
					break;
				//line 22
				case 21:
					newInterval = 6;
					break;
				//line 23
				case 22:
					newInterval = 6;
					break;
				//line 24
				case 23:
					newInterval = 7;
					break;
				//line 25
				case 24:
					newInterval = 4;
					break;
				//line 26
				case 25:
					newInterval = 4;
					break;
				//line 27
				case 26:
					newInterval = 2;
					break;
				//line 28
				case 27:
					newInterval = 7;
					break;
				//line 29
				case 28:
					newInterval = 4;
					break;
				//line 30
				case 29:
					newInterval = 5;
					break;
				//line 31
				case 30:
					newInterval = 6;
					break;
				//line 32
				case 31:
					newInterval = 4;
					break;
				//line 33
				case 32:
					newInterval = 3;
					break;
				//line 34
				case 33:
					newInterval = 4;
					break;
				//line 35
				case 34:
					newInterval = 3;
					break;
				//line 36
				case 35:
					newInterval = 4;
					break;
				//line 37
				case 36:
					newInterval = 4;
					break;
				//line 38
				case 37:
					newInterval = 5;
					break;
				//line 39
				case 38:
					newInterval = 4;
					break;
				//line 40
				case 39:
					newInterval = 6;
					break;
				//line 41
				case 40:
					newInterval = 6;
					break;
				//line 42
				case 41:
					newInterval = 4;
					break;
				//line 43
			}

			interval = newInterval;

			if (currentTime >= pastTime + interval) 
			{
				fading = false;
				currentLine++;
				pastTime = Time.timeSinceLevelLoad;
			}
			else if (currentTime >= pastTime + interval - pause)
			{
				fading = true;
			}

			myText.text = textLines[currentLine];
		}


		anim.SetBool("fading", fading);
		anim.SetBool("canIterate", canIterate);
		Debug.Log(textAlpha + ", " + currentLine);
	}

	/*
	//use Time.deltaTime
	void StayAwake() {
		if (fallingAsleep)
		{
			float drowsiness = -1.0f;
			float multiplier = 1.0f; 
			//start fading canvas at exponential increasing rate
			// GameObject.Find("RawImage").GetComponent<fadeLevel>().BeginFade(0.5f * 1.5f);
			if (Input.GetMouseButton(0))
			{
				// Debug.Log("mousepressed");
				// drowsiness = drowsiness + Random.Range(-0.05f, 0.03f);
				// Mathf.Clamp(drowsiness, -1.0f, 1.0f);
				drowsiness = -0.6f;
				// drowsiness = Random.Range(-0.6f, 0.f);
			}
			else
			{
				// drowsiness = 0;
				// multiplier = 1.0f;
				drowsiness = 0.5f;
			}
			// fadeLevelScript.BeginFade(0.5f + 0.5f + drowsiness );
			fadeLevelScript.BeginFade(Random.Range(0.3f, 0.7f) + drowsiness);
			// Debug.Log (awakeForce);
			//if mouse is held then apply opposite force
		}
	}
	*/
}
