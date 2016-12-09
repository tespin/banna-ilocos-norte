using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	private float mouseResistance;

	public float rate;

	// Use this for initialization
	void Start () {
		mouseResistance = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) 
		{
			mouseResistance++;
		}

		mouseResistance = mouseResistance - rate * Time.deltaTime;
		mouseResistance = Mathf.Clamp (mouseResistance, 0, 100);

		Debug.Log (mouseResistance);
	}
}
