using UnityEngine;
using System.Collections;

public class importText : MonoBehaviour {

	public TextAsset textFile; 
	public string[] textLines;

	// Use this for initialization
	void Start () {
		if (textFile != null) {
			textLines = textFile.text.Split ("\n"[0]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
