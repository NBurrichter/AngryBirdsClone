using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

//public enum GameState {
//	idle,
//	playing,
//	levelEnd
//}

public class GameController : MonoBehaviour {

	public static GameController S;


	public Text gtLevel;
	public Text gtShots;
	public Text gtToDestroy;
	public Text gtDestroyed;
	public Text gtWon;
	public Text gtLost;
	public Text gtFinalShots;
	private float textEasing = 0.05f;

	//dynamic fields
	public int level;
	public int levelMax;

	private int shotsTaken;
	public int maxShots;
	
	public string showing = "Cannon"; //cam mode

	//counter for destroyed ships
	private int goalCouter;
	public int goalToWin;
	private bool won = false;
	public bool lost = false;

	public string thisLevel;
	public string nextLevel;

	//Testing variables
	public float x;
	public float y;
	public float z;

	void Awake(){
		S = this;
	}

	private void Start()
	{
		StartLevel ();
	}

	void StartLevel(){
		shotsTaken = 0;

		//reset the camera
		SwitchView("Both");
		ProjectileTrail.S.Clear();

		//switch the view to "both"
		//Goal.goalMet = false;
		UpdateGT();

		won = false;

	}

	void UpdateGT() {
		gtLevel.text = "Level:" + (level+1) + " of " + levelMax;
		gtShots.text = maxShots - shotsTaken + " Shots left";
		gtToDestroy.text = "Destroy " + goalToWin + " ships";
		gtDestroyed.text = goalToWin -goalCouter + " more to go";
	}

	void Update() {
		//update our gui texts
		UpdateGT();

		//move "you won"
		if (won) {
			gtWon.rectTransform.anchoredPosition3D = Vector3.Lerp(gtWon.rectTransform.anchoredPosition3D,Vector3.zero,textEasing);
		}

		if (lost) {
			gtLost.rectTransform.anchoredPosition3D = Vector3.Lerp(gtLost.rectTransform.anchoredPosition3D,new Vector3(225,-160,0),textEasing);
		}
	}

	public void SwitchView(string view){
		//Switch over all the posibilities "cannon", "both","planet"
			S.showing = view;
			//set the FollowCam.S.poi to the according value
			switch(S.showing){
			case "Cannon":
				FollowCam.S.poi = null;
				break;
			case "Planet":
				FollowCam.S.poi = GameObject.Find ("Planet");
				break;
			case "Both":
				FollowCam.S.poi = GameObject.Find ("ViewBoth");
			break;
			}
	}

	//Mute Function
	//public void Mute(){
	//	if (GetComponent<AudioSource> ().mute == false) {
	//		GetComponent<AudioSource> ().mute = true;
	//	} else {
	//		GetComponent<AudioSource> ().mute = false;
	//	}
	//}

	public static void ShotFired(){
		S.shotsTaken++;
		if (S.shotsTaken >= S.maxShots) {
			S.lost = true;
		}
	}

	public void RestartLevel(){
		Application.LoadLevel (thisLevel);
	}

	public void IncGoal(int value){
		goalCouter += value;
		Debug.Log (goalCouter);

		//Check if goal is meet
		if (goalCouter >= goalToWin && !won) {
			won = true;
			gtFinalShots.text = shotsTaken + " shots made";
		}
	}

	public void NextLevel(){
		Application.LoadLevel(nextLevel);
	}
}