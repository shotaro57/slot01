using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rever : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(!Main.playFlag)
			{
				Main.playFlag = true;
			}
		}

		if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
		{
			transform.rotation = Quaternion.Euler(-45, 0, 0);
		}
		
		if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}	
	}
}
