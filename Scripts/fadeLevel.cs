using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fadeLevel : MonoBehaviour {

	private Image image;

	private float imgAlpha = 1.0f;

	public float fadeSpeed = 0.8f;
	private float fadeDir = -1.0f;

	private bool canFade;

	// Use this for initialization
	void Start () {
		//imgAlpha = GetComponent<RawImage>().texture.color.a;
		canFade = false;
		Cursor.visible = false;
		GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

		GetComponent<CanvasRenderer>().SetAlpha(imgAlpha);
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.timeSinceLevelLoad > 4)
		{
			canFade = true;
		}

		if(canFade)
		{
		imgAlpha += fadeDir * fadeSpeed * Time.deltaTime;
		imgAlpha = Mathf.Clamp01(imgAlpha);
//		Debug.Log(imgAlpha);
		GetComponent<CanvasRenderer>().SetAlpha(imgAlpha);		
		}
		else 
		{
			
		}
	}

	public float BeginFade(float direction) {
		fadeDir = direction;
		return (fadeSpeed);
	}

	void OnLevelWasLoaded() {
		BeginFade(-1);
	}

	public void ChangeFadeSpeed(float _fadeSpeed) {
		fadeSpeed = _fadeSpeed;
	}
}
