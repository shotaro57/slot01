using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRole : MonoBehaviour {

	private string role;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(!Main.playFlag)
		{
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				Main.role = JudgeRole();
				Debug.Log(Main.role);
			}
		}
	}
	
	/*
		子役を判定するメソット。stringを返す。
		文字列はMain.csのroleArrayを参照。
	 */
	private string JudgeRole()
	{

		return "リプレイ";
	}
}
