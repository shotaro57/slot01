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
				ButtonColorRed();	// ボタンの色を赤に変更
			}
			else if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
			{
				ButtonColorTmp();	// ボタンの色をtmpより反映
			}
			else
			{
				// ボタンの色をtmpに格納
				tmpButtonCenterColor = buttonCenter.GetComponent<Renderer>().material.color;
				tmpButtonLeftColor = buttonLeft.GetComponent<Renderer>().material.color;
				tmpButtonRightColor = buttonRight.GetComponent<Renderer>().material.color;

				// ボタンが押されたときの処理
				if(!isButtonCenterStop)	ButtonCenterStop();
				if(!isButtonLeftStop)	ButtonLeftStop();
				if(!isButtonRightStop)	ButtonRightStop();

				// ボタンが三つ押された場合、初期化
				if (reelCount == 3)
            	{
					//Main.playFlag = false;
                	initButton();
            	}		
			}
		}
		else
		{
			reelCount = 0;
		}		
		
	}

	private void initButton(){
		
		isButtonCenterStop = false;
		isButtonLeftStop = false;
		isButtonRightStop = false;
		tmpButtonCenterColor = Color.blue;
		tmpButtonLeftColor = Color.blue;
		tmpButtonRightColor = Color.blue;
        //reelCount = 0;
	}

	private void ButtonColorRed()
	{
		buttonCenter.GetComponent<Renderer>().material.color = Color.red;
		buttonLeft.GetComponent<Renderer>().material.color = Color.red;
		buttonRight.GetComponent<Renderer>().material.color = Color.red;
	}

	private void ButtonColorTmp()
	{
		buttonCenter.GetComponent<Renderer>().material.color = tmpButtonCenterColor;
		buttonLeft.GetComponent<Renderer>().material.color = tmpButtonLeftColor;
		buttonRight.GetComponent<Renderer>().material.color = tmpButtonRightColor;
	}

	private void ButtonCenterStop()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow))
        {
        	buttonCenter.GetComponent<Renderer>().material.color = Color.red;
			Main.isReelCenterStop = true;
			isButtonCenterStop = true;
        	reelCount++;
        }
	}

	private void ButtonLeftStop()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
        	buttonLeft.GetComponent<Renderer>().material.color = Color.red;
			Main.isReelLeftStop = true;
			isButtonLeftStop = true;
        	reelCount++;
        }
	}

	private void ButtonRightStop()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
        {
       		buttonRight.GetComponent<Renderer>().material.color = Color.red;
			Main.isReelRightStop = true;
			isButtonRightStop = true;
       		reelCount++;
 		}
	}
}
