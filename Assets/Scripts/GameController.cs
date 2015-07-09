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
	
	//Public Inspector fields
	//public GameObject[] levels;
	//public Vector3 levelPos;

	public Text gtLevel;
	public Text gtShots;
	public Text gtToDestroy;
	public Text gtDestroyed;

	//dynamic fields
	public int level;
	public int levelMax;

	public int shotsTaken;

	//public GameObject planetLevel; // the currnt Level Layout
	
	public string showing = "Cannon"; //cam mode

	//public GameState state = GameState.idle;

	//counter for destroyed ships
	private int goalCouter;
	public int goalToWin;

	public string thisLevel;
	public string nextLevel;

	void Awake(){
		S = this;

		//level = 0;
		//levelMax = levels.Length;

		//StartLevel ();
	}

	private void Start()
	{
		StartLevel ();
	}

	void StartLevel(){
		//if there is a planet, destroy it
		//if(planetLevel != null) {
		//	Destroy (planetLevel);
		//}

		//destroy all remaininng projectiles
		//GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
		//foreach(GameObject p in projectiles){
		//	Destroy(p);
		//}

		//Insttiate a new planet
		//planetLevel = Instantiate (levels[level]) as GameObject;
		//planetLevel.transform.position = levelPos;
		shotsTaken = 0;

		//reset the camera
		SwitchView("Both");
		ProjectileTrail.S.Clear();

		//switch the view to "both"
		//Goal.goalMet = false;
		UpdateGT();
		
		//state = GameState.playing;

		//clear all the projectile Trails

	}

	void UpdateGT() {
		gtLevel.text = "Level:" + (level+1) + " of " + levelMax;
		gtShots.text = "Shots Taken: " + shotsTaken;
		gtToDestroy.text = "Destroy " + goalToWin + " ships";
		gtDestroyed.text = goalToWin -goalCouter + " more to go";
	}

	void Update() {
		//update our gui texts
		UpdateGT();

		//check for level end
		//if(state == GameState.playing && Goal.goalMet) {
		//	if(FollowCam.S.poi.tag == "Projectile" &&  FollowCam.S.poi.GetComponent<Rigidbody>().IsSleeping()) {
		//		// Change state to stop checking for level end
		//		state = GameState.levelEnd;
		//		// Zoom out
		//		SwitchView("Both");
		//		// Start next level in 2 seconds
		//		//Invoke("NextLevel", 2f);
		//		//Application.LoadLevel(nextLevel);
		//	}
		//}
	}

	//void NextLevel() {
	//	level++;
	//	if(level == levelMax){
	//		level = 0;
	//	}
	//	StartLevel();
	//}

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
	}

	public void RestartLevel(){
		Application.LoadLevel (thisLevel);
	}

	public void IncGoal(int value){
		goalCouter += value;
		Debug.Log (goalCouter);

		//Check if goal is meet
		if (goalCouter >= goalToWin) {
			Application.LoadLevel(nextLevel);
		}
	}
}
/*
 * public class GameController : MonoBehaviour {

	public static GameController S; // Singleton

	// Fields set in Unity Inspector pane
	public GameObject[] castles; // An array with all castles
	public Text gtLevel; // Level GUI Text
	public Text gtScore; // Score GUI Text
	public Vector3 castlePos; // Place to put castles

	// Dynamic fields
	public int level; // Current level
	public int levelMax; // Number of levels
	public int shotsTaken;
	public GameObject castle; // The current castle
	public GameState state = GameState.idle;
	public string showing = "Slingshot"; // FollowCam mode
	
	void Start(){
		S = this;
		level = 0;
		levelMax = castles.Length;
		StartLevel();
	}

	void StartLevel() {
		// If a castle exists, get rid of it
		if(castle != null) {
			Destroy (castle);
		}

		// Destroy the old projectiles
		GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
		foreach(GameObject p in projectiles){
			Destroy(p);
		}

		// Instantiate the new castle
		castle = Instantiate (castles[level]) as GameObject;
		castle.transform.position = castlePos;
		shotsTaken = 0;

		// Reset the camera
		SwitchView("Both");
		ProjectileLine.S.Clear();

		// Reset the Goal
		Goal.goalMet = false;
		UpdateGT();

		state = GameState.playing;
	
	}

	void UpdateGT() {
		gtLevel.text = "Level:" + (level+1) + " of " + levelMax;
		gtScore.text = "Shots Taken: " + shotsTaken;
	}

	void Update() {
		UpdateGT();

		// Check for level end
		if(state == GameState.playing && Goal.goalMet) {
			if(FollowCam.S.poi.tag == "Projectile" &&  FollowCam.S.poi.GetComponent<Rigidbody>().IsSleeping()) {
				// Change state to stop checking for level end
				state = GameState.levelEnd;
				// Zoom out
				SwitchView("Both");
				// Start next level in 2 seconds
				Invoke("NextLevel", 2f);
			}
		}
	}

	void NextLevel() {
		level++;
		if(level == levelMax){
			level = 0;
		}
		StartLevel();
	}

	// Static to change the view point
	public void SwitchView(string view) {
		S.showing = view;
		switch(S.showing){
		case "Slingshot":
			FollowCam.S.poi = null;
			break;
		case "Castle":
			FollowCam.S.poi = S.castle;
			break;
		case "Both":
			FollowCam.S.poi = GameObject.Find ("ViewBoth");
			break;
		}
	}

	// Static function that allows to increment the score
	public static void ShotFired(){
		S.shotsTaken++;
	}


}
*/