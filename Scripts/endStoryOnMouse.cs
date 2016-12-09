﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class endStoryOnMouse : MonoBehaviour {

	private Animator anim;

	private waitOnMouseClick waitOnMouseClickScript;

	private bool leaving;

	private float timeSince;
	private float storyTimer;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		waitOnMouseClickScript = GetComponent<waitOnMouseClick>();

		leaving = false;

		timeSince = 0;
		storyTimer = 0;
	}

	// Update is called once per frame
	void Update () {
		if (waitOnMouseClickScript.waiting == false)
		{
			storyTimer = Time.timeSinceLevelLoad - timeSince;
		}
		else 
		{
			timeSince = Time.timeSinceLevelLoad;
		}

		if (storyTimer >= 15.0)
		{
			leaving = true;
			StartCoroutine(changeLevel());
		}
		// Debug.Log(storyTimer);
		anim.SetBool("leaving", leaving);
	}

	IEnumerator changeLevel() {
		float fadeTime = GameObject.Find("RawImage").GetComponent<fadeLevel>().BeginFade(0.87f);
		yield return new WaitForSeconds(fadeTime * 4.5f);
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
