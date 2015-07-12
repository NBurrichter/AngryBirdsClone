using UnityEngine;
using System.Collections;

public class VictoryEffects : MonoBehaviour {
	  
	public static VictoryEffects S;
	public GameObject fireworkPrefab;
	private GameObject[] fireworkInstances;
	public int fireworkNum;

	void Awake(){
		S = this;
	}

	public void CreateEffects(){
		fireworkInstances = new GameObject[fireworkNum];

		GameObject firework;
	}

	void Update(){

	}
}
