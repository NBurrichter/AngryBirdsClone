using UnityEngine;
using System.Collections;

public class MuteSound : MonoBehaviour {

	private bool muteState;

	void Awake(){
		AudioListener.pause = false;
		muteState = false;
	}

	public void mute(){
		if (muteState) {
			AudioListener.volume = 1;
			muteState = false;
		} else {
			AudioListener.volume = 0;
			muteState = true;
		}
	}
}
