using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Performance : MonoBehaviour {
	private int peka;
	private int pekakanri = 0;
    private Light pointComp;
    private Light spotComp;       
	private GameObject pointLight;
	private GameObject spotLight;
    // Use this for initialization
    void Start () {
 		pointLight = GameObject.Find("PointLight");
		spotLight = GameObject.Find("SpotLight");
		pointComp = pointLight.GetComponent<Light>();
		spotComp = spotLight.GetComponent<Light>();
		pointComp.intensity = 0f;
		spotComp.intensity = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Main.jyunbiFlag = false;
		}
		*/

		if(Main.jyunbiFlag){
			if(!Main.Lump){
				if(pekakanri == 0){
					peka = Random.Range(0,4);
					pekakanri = 1;
				}
				if(peka == 0){
					Main.Lump = true;
					Debug.Log("先点灯中");
					pointComp.intensity = 2.36f;
					spotComp.intensity = 78.48f;
					pekakanri = 0;
				}else{
                    if(Main.playFlag == false){
						Main.Lump = true;
						Debug.Log("後点灯中");
						pointComp.intensity = 2.36f;
						spotComp.intensity = 78.48f;
                        pekakanri = 0;
					}				
				}
			}

		}else{
			pointComp.intensity = 0f;
			spotComp.intensity = 0f;
			Main.Lump = false;
		}
		
	}
}
