using UnityEngine;
using System.Collections;

public class lookAtCoconut : MonoBehaviour {

	public Transform coconutTransform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(coconutTransform);
	}
}
