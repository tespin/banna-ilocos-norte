using UnityEngine;
using System.Collections;

public class fadeTextBox : MonoBehaviour {

	private bool fadeBoxIn;
	private GameObject dialogueTextObject;


	private displayDialogue displayDialogueScript;

	private Animator anim;

	// Use this for initialization
	void Start () {
		displayDialogueScript = GameObject.Find("DialogueText").GetComponent<displayDialogue>();

		anim = GetComponent<Animator>();

		fadeBoxIn = false;
	}
	
	// Update is called once per frame
	void Update () {
		// fadingOutBox = displayDialogueScript.fading;

		// anim.SetBool("fadeOut", fadingOutBox);

		// if (displayDialogueScript.currentLine >= 1 && displayDialogueScript.currentLine <= 42)
		// {
		// 	fadeBoxIn = true;
		// }
		// else if (displayDialogueScript.currentLine > displayDialogueScript.lastLine - 1)
		// {
		// 	fadeBoxIn = false;
		// }

		// anim.SetBool("fadeBoxIn", fadeBoxIn);
		anim.SetBool("fadeBoxIn", !displayDialogueScript.fading);
		anim.SetBool("isIterating", displayDialogueScript.canIterate);
	}
}
