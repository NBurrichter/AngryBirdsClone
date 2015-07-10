using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialMover : MonoBehaviour {

	public Image image;
	public float easing;
	public float yPos;
	private Vector3 startPos;
	private bool buttonPressed = false;

	void Awake(){
		startPos = image.rectTransform.anchoredPosition3D;
	}

	// Use this for initialization
	void Start () {
		buttonPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!buttonPressed) {
			image.rectTransform.anchoredPosition3D = Vector3.Lerp (image.rectTransform.anchoredPosition3D, new Vector3 (0, yPos, 0), easing);
		} else {
			image.rectTransform.anchoredPosition3D = Vector3.Lerp (image.rectTransform.anchoredPosition3D, startPos, easing);
		}
	}

	public void Button(){
		buttonPressed = true;
	}

}
