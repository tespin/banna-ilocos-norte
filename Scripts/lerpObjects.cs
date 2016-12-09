using UnityEngine;
using System.Collections;

public class lerpObjects : MonoBehaviour {

	// public Transform lerpedTransform1;
	// public Transform lerpedTransform2;
	// public Transform lerpedTransform3;
	// public Transform lerpedTransform4;
	// public Transform lerpedTransform5;

	public Transform[] lerpedTransforms;

	private GameObject mainCamera;
	private GameObject sculptureObject;

	private int index;

	private int maxObjects;	
	private int numberOfObjects;

	public float lerpValue;

	private bool canSculpture;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");

		maxObjects = 5;
		numberOfObjects = 0;

		canSculpture = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (numberOfObjects > maxObjects)
		{
			canSculpture = false;
		}

		GetObjectToLerp();

		if (canSculpture)
		{
			LerpObject(sculptureObject);
		}
		else
		{

		}
	}

	void LerpObject(GameObject obj) {

		LerpTransform(obj.transform, lerpedTransforms[index], lerpValue);

		// obj.transform = Vector3.Lerp(obj.transform, lerpedTransforms[index].transform, Time.deltaTime * lerpValue);
	}

	void GetObjectToLerp() {
		if (Input.GetMouseButtonDown(0))
		{
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (Vector3.Distance(hit.collider.transform.position, transform.position) <= 2.0f)
				{
					if (hit.collider.gameObject.tag == "Sculpture")
					{
						// hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
						index = Random.Range(1, lerpedTransforms.Length);
						sculptureObject = hit.collider.gameObject;
						// Debug.Log("newObject!");
						Debug.Log(numberOfObjects);
						numberOfObjects += 1;
					}
				}
			}
		}
	}

	void LerpTransform(Transform t1, Transform t2, float smoothing) {

		t1.position = Vector3.Lerp(t1.position, t2.position, smoothing);
		t1.rotation = Quaternion.Lerp(t1.rotation, t2.rotation, smoothing);
	}
}
