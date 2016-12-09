using UnityEngine;
using System.Collections;

public class spinFan : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		//speed = 300;
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0,speed,0) * Time.deltaTime);
	}
}
