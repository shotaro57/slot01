using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	private GameObject buttonCenter;
    private GameObject buttonLeft;
    private GameObject buttonRight;
	private bool isButtonCenterStop = false;
	private bool isButtonLeftStop = false;
	private bool isButtonRightStop = false;
	private Color tmpButtonCenterColor;
	private Color tmpButtonLeftColor;
	private Color tmpButtonRightColor;
	private int reelCount = 0;

	// Use this for initialization
	void Start () {
		buttonCenter = GameObject.Find("buttonCenter");
        buttonLeft = GameObject.Find("buttonLeft");
        buttonRight = GameObject.Find("buttonRight");
        buttonCenter.GetComponent<Renderer>().material.color = Color.white;
        buttonLeft.GetComponent<Renderer>().material.color = Color.white;
        buttonRight.GetComponent<Renderer>().material.color = Color.white;
		tmpButtonCenterColor = Color.blue;
		tmpButtonLeftColor = Color.blue;
		tmpButtonRightColor = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		if (Main.playFlag){
			if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
			{
				buttonCenter.GetComponent<Renderer>().material.color = Color.red;
				buttonLeft.GetComponent<Renderer>().material.color = Color.red;
				buttonRight.GetComponent<Renderer>().material.color = Color.red;
			}
			else if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
			{
				buttonCenter.GetComponent<Renderer>().material.color = tmpButtonCenterColor;
				buttonLeft.GetComponent<Renderer>().material.color = tmpButtonLeftColor;
				buttonRight.GetComponent<Renderer>().material.color = tmpButtonRightColor;
			}
			else
			{
				tmpButtonCenterColor = buttonCenter.GetComponent<Renderer>().material.color;
				tmpButtonLeftColor = buttonLeft.GetComponent<Renderer>().material.color;
				tmpButtonRightColor = buttonRight.GetComponent<Renderer>().material.color;

				if(!isButtonCenterStop)
				{
					if (Input.GetKeyDown(KeyCode.DownArrow))
            		{
                		buttonCenter.GetComponent<Renderer>().material.color = Color.red;
						Main.isReelCenterStop = true;
						isButtonCenterStop = true;
                		reelCount++;
            		}
				}
				if(!isButtonLeftStop)
				{
					if (Input.GetKeyDown(KeyCode.LeftArrow))
            		{
                		buttonLeft.GetComponent<Renderer>().material.color = Color.red;
						Main.isReelLeftStop = true;
						isButtonLeftStop = true;
                		reelCount++;
            		}
				}
				if(!isButtonRightStop)
				{
					if (Input.GetKeyDown(KeyCode.RightArrow))
            		{
                		buttonRight.GetComponent<Renderer>().material.color = Color.red;
						Main.isReelRightStop = true;
						isButtonRightStop = true;
                		reelCount++;
            		}
				}

				if (reelCount == 3)
            	{
					//Main.playFlag = false;
                	initButton();
            	}		
			}

		}
		else
		{

		}		
		
	}

	private void initButton(){
		
		isButtonCenterStop = false;
		isButtonLeftStop = false;
		isButtonRightStop = false;
		tmpButtonCenterColor = Color.blue;
		tmpButtonLeftColor = Color.blue;
		tmpButtonRightColor = Color.blue;
        reelCount = 0;
	}
}
