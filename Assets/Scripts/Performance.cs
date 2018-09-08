using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Performance : MonoBehaviour {
	private int peka;
	private int pekakanri = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Main.jyunbiFlag){
			if(!Main.Lump){
				if(pekakanri == 0){
					peka = Random.Range(0,4);
					pekakanri=1;
				}
				if(peka == 0){
							Main.Lump = true;
							Debug.Log("先点灯中");
							pekakanri=0;
				}else{
                    if(Main.playFlag == false){
							Main.Lump = true;
							Debug.Log("後点灯中");
							pekakanri=0;
					}
					
				}
			}	
		}
	}
}
