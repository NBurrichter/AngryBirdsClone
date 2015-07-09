using UnityEngine;
using System.Collections;

public class GoalCouter : MonoBehaviour {

	public int value = 1;

	void OnDestroy(){
		GameController.S.IncGoal (value);
	}
}
