using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	private GameObject launchPoint;

	void Awake() {
		Transform launchPointTrans = transform.Find("Launchpoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
	}

    void OnMouseEnter() {
		//print ("Slingshot:MouseEnter");
		launchPoint.SetActive (true);
		//launchPoint.GetComponent<halo> ().SetActive (true);
	}

	void OnMouseExit(){
		//print ("Slingshot:MouseExit");
		launchPoint.SetActive (false);
	}
}
