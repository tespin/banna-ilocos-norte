using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class chooseLevelMouse : MonoBehaviour {
	
	public Camera[] KabayoCameras;
	public Camera[] AswangCameras;
	public Camera[] ItoCameras;

	private Camera CurrentCamera;

	private GameObject Panel;
	private Image PanelImage;
	private CanvasRenderer PanelRenderer;

	private GameObject Title;
	private Text TitleText;

	private Text InstructionsText;

	private Animator textAnim;
	private Animator instructionsTextAnim;

	private int KabayoIndex;
	private int ItoIndex;
	private int AswangIndex;

	private int sceneIndex;

	private float imgAlpha = 1.0f;
	public float fadeSpeed = 3.0f;
	private float fadeDir;

	private bool light;
	private bool medium;
	private bool hard;
	private bool none;

	private bool receivingInput;

	private bool changingLevel;

	public Light ItoLight;

	private float mouseResistance = 0.0f;
	private float mouseResistanceThreshold = 0.0f;
	private float mouseRatio = 0.0f;
	public float rate;

	void Start() {

		Panel = GameObject.Find("Panel");
		PanelImage = Panel.GetComponent<Image>();

		Cursor.visible = false;

		TitleText = GameObject.Find("Title").GetComponent<Text>();
		textAnim = TitleText.GetComponent<Animator>();

		InstructionsText = GameObject.Find("Instructions").GetComponent<Text>();
		instructionsTextAnim = GameObject.Find("Instructions").GetComponent<Animator>();
		// TitleText.color = new Color (143, 61, 8, 1);
		// PanelRenderer = Panel.GetComponent<CanvasRenderer>();

		// PanelRenderer = GameObject.Find("Title").GetComponent<CanvasRenderer>();

		light = false;
		medium = false;
		hard = false;
		none = false;

		changingLevel = false;

	}

	void Update() {
		ReceiveInput ();

//		if (changingLevel == false) {
//			mouseResistance = mouseResistance;
//		}

		Debug.Log (fadeDir);

		CheckInput ();

		IterateCameras ();
		SelectLevel ();

	}

	void CheckInput() {

		// receive input and clamp values
		if (Input.GetMouseButtonDown (0)) 
		{
			mouseResistance += 2;
			mouseResistanceThreshold = mouseResistance;
		}

		mouseResistance = mouseResistance - rate * Time.deltaTime;
		mouseResistance = Mathf.Clamp (mouseResistance, 0, 100);

		// convert to bool values
		if (mouseResistance > 2 && mouseResistance <= 35) {
			light = true;
			medium = false;
			hard = false;
			none = false;
		} else if (mouseResistance > 35 && mouseResistance < 80) {
			light = false;
			medium = true;
			hard = false;
			none = false;
		} else if (mouseResistance >= 80) {
			light = false;
			medium = false;
			hard = true;
			none = false;
		} else {
			light = false;
			medium = false;
			hard = false;
		}

//		Debug.Log (light + ", " + medium + ", " + hard + ", " + none);
	}

	void ReceiveInput() {
		
//		if (mouseResistance < mouseResistance / 10 * 9.9) {
//			receivingInput = false;
//		} else {
//			receivingInput = true;
//		}

		mouseRatio = mouseResistance / mouseResistanceThreshold;

		if (mouseRatio > 0.85f) {
			receivingInput = true;
		} else {
			receivingInput = false;

			light = false;
			medium = false;
			hard = false;
			none = true;
		}

		Debug.Log (mouseRatio + ", " + receivingInput + ", " + none);


		textAnim.SetBool("sensorPressed", receivingInput);
		instructionsTextAnim.SetBool("sensorPressed", receivingInput);

	}

	void IterateCameras() {

		if (light) {
			sceneIndex = 2;
			ItoIndex = Random.Range(1, ItoCameras.Length);
			ItoLight.enabled = true;
			ItoCameras[ItoIndex].enabled = true;
			CurrentCamera = ItoCameras[ItoIndex];

			for (int i = 0; i < AswangCameras.Length; i++) {
				AswangCameras[i].enabled = false;
			}

			for (int i = 0; i < KabayoCameras.Length; i++) {
				KabayoCameras[i].enabled = false;
			}
		}
		else if (medium) {
			sceneIndex = 1;
			KabayoIndex = Random.Range(1, KabayoCameras.Length);
			ItoLight.enabled = false;
			KabayoCameras[KabayoIndex].enabled = true;
			CurrentCamera = KabayoCameras[KabayoIndex];

			for (int i = 0; i < AswangCameras.Length; i++) {
				AswangCameras[i].enabled = false;
			}

			for (int i = 0; i < ItoCameras.Length; i++) {
				ItoCameras[i].enabled = false;
			}
		}
		else if (hard) {
			sceneIndex = 3;
			AswangIndex = Random.Range(1, AswangCameras.Length);
			ItoLight.enabled = false;
			AswangCameras[AswangIndex].enabled = true;
			CurrentCamera = AswangCameras[AswangIndex];

			for (int i = 0; i < KabayoCameras.Length; i++) {
				KabayoCameras[i].enabled = false;
			}

			for (int i = 0; i < ItoCameras.Length; i++) {
				ItoCameras[i].enabled = false;
			}

		}
		else {

			for (int i = 0; i < AswangCameras.Length; i++) {
				AswangCameras[i].enabled = false;
			}

			for (int i = 0; i < KabayoCameras.Length; i++) {
				KabayoCameras[i].enabled = false;
			}

			for (int i = 0; i < ItoCameras.Length; i++) {
				ItoCameras[i].enabled = false;
			}

			CurrentCamera.enabled = true;

		}
	}

	void SelectLevel() {

		if (changingLevel == false)
		{
			// Debug.Log(TitleText.color);

			// imgAlpha += fadeDir * fadeSpeed * Time.deltaTime;
			// imgAlpha = Mathf.Clamp01(imgAlpha);

			if ((light && !none) || (medium && !none) || (hard && !none))
			{
				fadeDir = -1.5f;
				// fadeDir = -0.4f;
			}
			if (receivingInput == false)
			{
				fadeDir = 3.0f;
			}

			if (imgAlpha <= 0.05f)
			{
				changingLevel = true;
			}

		}
		else
		{
			fadeDir = 15.0f;

			if (imgAlpha >= 0.95)
			{
				StartCoroutine(loadLevel());
			}
		}

		imgAlpha += fadeDir * fadeSpeed * Time.deltaTime;
		imgAlpha = Mathf.Clamp01(imgAlpha);

		PanelImage.color = new Color(0, 0, 0, imgAlpha);

	}

	IEnumerator loadLevel() {
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
	}

}