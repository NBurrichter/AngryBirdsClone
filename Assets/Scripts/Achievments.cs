using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Achievments : MonoBehaviour {

	public static Achievments S;

	public Canvas targetCanvasParent;
	public Image achievmentPanelPrefab;

	//internal achievment array
	private Image[] achievmentPanelInstances;
	public int achievmentNumb = 0;

	void Awake(){
		S = this; 
	}

	public void GetAchievment(int index){
		achievmentNumb++;
		achievmentPanelInstances = new Image[achievmentNumb];

		Image achievmentPanel;
		achievmentPanel = Instantiate (achievmentPanelPrefab);
		achievmentPanel.transform.SetParent(targetCanvasParent.transform);
		achievmentPanel.rectTransform.position = new Vector3 (0,0,0);

		achievmentPanelInstances[achievmentNumb -1] = achievmentPanel;

	}

	// Use this for initialization
	void Start () {
		//achievmentPanel = Instantiate (achievmentPanelPrefab) as Image;
		//achievmentPanel.transform.parent = targetCanvasParent.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
