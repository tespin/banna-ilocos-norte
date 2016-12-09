using UnityEngine;
using System.Collections;

public class waitOnMouseClick : MonoBehaviour {

	private float mouseResistance;

	private bool receivingInput;

	private Animator anim;

	public bool waiting;

	private float rate = 4.5f;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();

		waiting = false;

	}
	
	// Update is called once per frame
	void Update () {

		ReceiveInput ();
		CheckInput();

		if (receivingInput)
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
