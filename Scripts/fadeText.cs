using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fadeText : MonoBehaviour {

	private float textAlpha = 1.0f;

	public float fadeSpeed = 0.8f;

	private int fadeDir = -1;

	private float interval;
	private float pastTime;
	private float currentTime;
	private float timer;

	private bool fadingOut;

	private Animator anim;

	// Use this for initialization
	void Start () {

		interval = 3;
		pastTime = 0;
		timer = 0;
		// canIterate = true;
		fadingOut = false;

		anim = GetComponent<Animator>();
		
	}

	// Update is called once per frame
	void Update () {
		// textAlpha += fadeDir * fadeSpeed * Time.deltaTime;
		// textAlpha = Mathf.Clamp01(textAlpha);
		// //		Debug.Log(textAlpha);
		// GetComponent<CanvasRenderer>().SetAlpha(textAlpha);

		currentTime = Time.timeSinceLevelLoad;

		if (currentTime >= pastTime + interval)
		{
			fadingOut = true;
			// pastTime = Time.timeSinceLevelLoad;
		}
		// else if (currentTime >= pastTime + interval - pause)
		// {
		// 	fading = false;
		// }

		Debug.Log(fadingOut);

		anim.SetBool("fadingOut", fadingOut);

	}

}
