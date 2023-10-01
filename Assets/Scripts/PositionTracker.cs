using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
	public float distanceLimit = 2.5f;
	public GameObject mainChar;
	public GameObject death;

	private Vector3 mainCharPos;

	private byte frame;

	// Start is called before the first frame update
	void Start()
	{
		if(!mainChar)mainChar = GameObject.Find("Main_Char");
		if(!death)death = gameObject;
	}

	// Update is called once per frame
	void Update()
	{
		mainCharPos = mainChar.transform.position;

		frame++;
		if(frame > 10){
			FindLimits();
			frame = 0;
		}
		CheckIfDead();
	}

	//stick a sphere of out of bounds to the "nearest" object
	public bool FindLimits(){
		GameObject[] platforms = GameObject.FindGameObjectsWithTag("MaterialPlatform");
		foreach(GameObject plat in platforms){
			Vector3 newPos = plat.transform.position;
			if(Vector3.Distance(mainCharPos, newPos) < distanceLimit){
				death.transform.position = newPos;
				return true;
			}
		}
		return false;
	}

	public bool CheckIfDead(){
		if(Vector3.Distance(mainCharPos, death.transform.position) > distanceLimit*2){
			Debug.Log("You are Dead!");
			return true;
		}
		return false;
	}
}
