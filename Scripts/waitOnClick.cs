using UnityEngine;
using System.Collections;

public class waitOnClick : MonoBehaviour {

	private Animator anim;

	private int analogReading;

	public bool waiting;

	private float timeSince;
	private float timer;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		waiting = false;

		timeSince = 0;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			waiting = true;
		}
		else 
		{
			waiting = false;
		}

		anim.SetBool("waiting", waiting);
	}
}
