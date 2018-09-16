using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public static bool playFlag = false;
	public static bool isReelCenterStop = false;
	public static bool isReelLeftStop = false;
	public static bool isReelRightStop = false;
	public static string role = "はずれ";
	public static int settei;
	public static bool bigFlag = false;
	public static bool regFlag = false;
	public static bool jyunbiFlag = false;
	public static bool lump = false;
	// Use this for initialization
	void Start () {
        //settei = Random.Range(1, 7);
		settei = 10;
		Debug.Log(settei);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
