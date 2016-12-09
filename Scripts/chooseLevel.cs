using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Uniduino;

public class chooseLevel : MonoBehaviour {

	public Arduino arduino;

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

	private int AnalogReading;
	private int pin0 = 0;

	private int KabayoIndex;
	private int ItoIndex;
	private int AswangIndex;

	private int sceneIndex;

	private float imgAlpha = 1.0f;
	public float fadeSpeed = 5.0f;
	private float fadeDir;

	private bool light;
	private bool medium;
	private bool hard;
	private bool none;

	private bool sensorPressed;

	private bool changingLevel;

	public Light ItoLight;

	void Start() {
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);

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
		// Debug.Log(AnalogReading);

		CheckInput();

		Debug.Log(changingLevel);

		if (changingLevel == false)
		{
			AnalogReading = arduino.analogRead(pin0);
		}
		else
		{

		}

		if (AnalogReading > 5 && AnalogReading <= 400)
		{
			light = true;
			medium = false;
			hard = false;
			none = false;
		}
		else if (AnalogReading > 400 && AnalogReading < 900)
		{
			light = false;
			medium = true;
			hard = false;
			none = false;
		}
		else if (AnalogReading >= 900)
		{
			light = false;
			medium = false;
			hard = true;
			none = false;
		}
		else
		{
			light = false;
			medium = false;
			hard = false;
			none = true;
		}

		IterateCameras();
		SelectLevel();
	}

	void CheckInput() {
		if (AnalogReading > 5)
		{
			sensorPressed = true;
		}
		else
		{
			sensorPressed = false;
		}

		textAnim.SetBool("sensorPressed", sensorPressed);
		instructionsTextAnim.SetBool("sensorPressed", sensorPressed);

	}

	void ConfigurePins() {
		arduino.pinMode(pin0, PinMode.ANALOG);
		arduino.reportAnalog(pin0, 1);
	}

	void IterateCameras() {
		if (light)
		{
			sceneIndex = 2;
			ItoIndex = Random.Range(1, ItoCameras.Length);
			ItoLight.enabled = true;
			ItoCameras[ItoIndex].enabled = true;
			CurrentCamera = ItoCameras[ItoIndex];

			for (int i = 0; i < AswangCameras.Length; i++)
			{
				AswangCameras[i].enabled = false;
			}

			for (int i = 0; i < KabayoCameras.Length; i++)
			{
				KabayoCameras[i].enabled = false;
			}
		}
		else if (medium)
		{
			sceneIndex = 1;
			KabayoIndex = Random.Range(1, KabayoCameras.Length);
			ItoLight.enabled = false;
			KabayoCameras[KabayoIndex].enabled = true;
			CurrentCamera = KabayoCameras[KabayoIndex];

			for (int i = 0; i < AswangCameras.Length; i++)
			{
				AswangCameras[i].enabled = false;
			}

			for (int i = 0; i < ItoCameras.Length; i++)
			{
				ItoCameras[i].enabled = false;
			}
		}
		else if (hard)
		{

			sceneIndex = 3;
			AswangIndex = Random.Range(1, AswangCameras.Length);
			ItoLight.enabled = false;
			AswangCameras[AswangIndex].enabled = true;
			CurrentCamera = AswangCameras[AswangIndex];

			for (int i = 0; i < KabayoCameras.Length; i++)
			{
				KabayoCameras[i].enabled = false;
			}

			for (int i = 0; i < ItoCameras.Length; i++)
			{
				ItoCameras[i].enabled = false;
			}

		}
		else 
		{

			for (int i = 0; i < AswangCameras.Length; i++)
			{
				AswangCameras[i].enabled = false;
			}

			for (int i = 0; i < KabayoCameras.Length; i++)
			{
				KabayoCameras[i].enabled = false;
			}

			for (int i = 0; i < ItoCameras.Length; i++)
			{
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

			if (light || medium || hard)
			{
				fadeDir = -1.5f;
				// fadeDir = -0.4f;
			}
			else if (none)
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

		// TextAlpha += fadeDir * fadeSpeed * Time.deltaTime;
		// TextAlpha = Mathf.Clamp01(TextAlpha);

		// TitleText.color = new Color(0, 0, 0, TextAlpha);

		imgAlpha += fadeDir * fadeSpeed * Time.deltaTime;
		imgAlpha = Mathf.Clamp01(imgAlpha);

		PanelImage.color = new Color(0, 0, 0, imgAlpha);

	}

	IEnumerator loadLevel() {
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
	}

}