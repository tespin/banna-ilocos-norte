using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class testForceFade : MonoBehaviour {

	private Image PanelImage;
	private float imgAlpha = 1.0f;
	public float fadeSpeed = 5.0f;
	private float fadeDir = 0;

	private bool changingLevel; 

	// Use this for initialization
	void Start () {
		changingLevel = false;

		PanelImage = GameObject.Find("Panel").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		FadeOut();
	
	}

	void FadeOut() {

		if (changingLevel == false)
		{
			// Debug.Log(TitleText.color);

			// imgAlpha += fadeDir * fadeSpeed * Time.deltaTime;
			// imgAlpha = Mathf.Clamp01(imgAlpha);

			if (Input.GetMouseButton(0))
			{
				fadeDir = -0.9f;
				// fadeDir = -0.4f;
			}
			else
			{
				fadeDir = 1.0f;
			}

			if (imgAlpha <= 0.15f)
			{
				changingLevel = true;
		
				// Debug.Log("chooselevel");

				// StartCoroutine("loadLevel");

			}

			// TitleText.color = new Color (1, 1, 1, imgAlpha);
		}
		else
		{
			fadeDir = 2.0f;

			if (imgAlpha >= 0.95)
			{
				// changingLevel = false;
				Debug.Log("changingLevel!");
			}
			// if (imgAlpha <= 0.15f)
			// {
			// 	changingLevel = true;
			// 	fadeDir = 4.0f;
		
			// 	// Debug.Log("chooselevel");

			// 	// StartCoroutine("loadLevel");

			// }
		}

		imgAlpha += fadeDir * fadeSpeed * Time.deltaTime;
		imgAlpha = Mathf.Clamp01(imgAlpha);

		PanelImage.color = new Color(0, 0, 0, imgAlpha);

	}
}
