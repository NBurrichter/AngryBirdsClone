using UnityEngine;
using System.Collections;

public class CloudCrafterRight : MonoBehaviour {

	//Inspector fields
	public int numClouds = 40;
	
	public Vector3 cloudPosMin;
	public Vector3 cloudPosMax;
	
	public float cloudScaleMin = 1.0f;
	public float cloudScaleMax = 5.0f;
	
	public float cloudSpeedMult = 0.5f;
	
	public GameObject[] cloudPrefabs;
	
	//Internal fields
	private GameObject[] cloudInstances;
	
	void Awake(){
		//create an array large enough to store all cloud instances
		cloudInstances = new GameObject[numClouds];
		
		//find the cloud anchor in the hierachy
		GameObject anchor = GameObject.Find("Clouds");
		
		//Iterate through array and create a cloud for each slot
		GameObject cloud;
		for(int i = 0; i < cloudInstances.Length; i ++){
			
			//Randomly pick one of the cloud instances
			int prefabNum = Random.Range(0,cloudPrefabs.Length);
			
			//create that instance
			cloud = Instantiate(cloudPrefabs[prefabNum]) as GameObject;
			
			//Position and scale the cloud
			Vector3 cPos = Vector3.zero;
			cPos.x = Random.Range(cloudPosMin.x,cloudPosMax.x);
			cPos.y = Random.Range(cloudPosMin.y,cloudPosMax.y);
			
			float scaleU = Random.value;
			float scaleVal = Mathf.Lerp(cloudScaleMin,cloudScaleMax,scaleU);
			
			//cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
			
			cPos.z =  100 - 90 * scaleU;
			
			//apply changes to our instance
			cloud.transform.position = cPos;
			cloud.transform.localScale = Vector3.one * scaleVal;

			cloud.transform.localScale = new Vector3(cloud.transform.localScale.x * -1,cloud.transform.localScale.y,cloud.transform.localScale.z);

			//Direction ofclouds
			//cloud.transform.localScale = new Vector3(-1,cloud.transform.localScale.y,cloud.transform.localScale.z);
			
			//Make the cloud a child of our anchor
			cloud.transform.parent = anchor.transform;
			
			//Put the cloud into our instances array
			cloudInstances[i] = cloud;
			
		}
	}
	
	void Update() {
		//iterate through all cloud instances
		foreach (GameObject cloud in cloudInstances) {
			
			//Get the position and scale
			float scaleVal = cloud.transform.localScale.x;
			Vector3 cPos = cloud.transform.position;
			
			cPos.x -= Time.deltaTime * cloudSpeedMult * scaleVal;
			
			//check if clouds x position is to small
			if(cPos.x > cloudPosMax.x) {
				
				//set it to the maximum x position
				cPos.x = cloudPosMax.x;
			}
			
			cloud.transform.position = cPos;
			
		}
	}
}
