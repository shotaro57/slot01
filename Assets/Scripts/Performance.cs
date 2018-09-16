using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Performance : MonoBehaviour {
	
	private int peka;
	private bool pekaFlag = true;
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
		LightOff();
	}
	
	// Update is called once per frame
	void Update () {
		if(Main.jyunbiFlag){
			if(!Main.lump){
				// 先ペカ、後ペカの判定
				if(pekaFlag){
					peka = Random.Range(0,4);
					pekaFlag = false;
				}

				// ランプ点灯処理
				if(peka == 0){
					Main.lump = true;
					pekaFlag = true;
					LightOn();
				}else{
                    if(!Main.playFlag){
						Main.lump = true;
                        pekaFlag = true;
						LightOn();
					}				
				}
			}
		}else{
			// ランプを消す処理
			if(Main.lump)
			{
				LightOff();
				Main.lump = false;
			}
		}
		
	}

	private void LightOn()
	{
		pointComp.intensity = 2.36f;
		spotComp.intensity = 78.48f;
	}

	private void LightOff()
	{
		pointComp.intensity = 0f;
		spotComp.intensity = 0f;
	}
}
