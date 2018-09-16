using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	クラス名：Reel
	機能：	リールの回転の制御を行うクラス。
			遊技中であれば回転し、ボタンが押されれば停止する。
 */
public class Reel : MonoBehaviour {

	private GameObject reelCenter;
    private GameObject reelLeft;
    private GameObject reelRight;
	private float reelCenterSpeed;
    private float reelLeftSpeed;
    private float reelRightSpeed;
	private int reelCount = 0;
    private float reelTimer;
    private bool reelTimerFlag = true;
    private float reelCenterRotateTime = 0;
    private float reelLeftRotateTime = 0;
    private float reelRightRotateTime = 0;
    private float reelCenterWaitTime = 0;
    private float reelLeftWaitTime = 0;
    private float reelRightWaitTime = 0;
    private int reelCenterBitaZugara = 20;
    private int reelLeftBitaZugara = 20;
    private int reelRightBitaZugara = 20;
    private bool reelCenterBitaZugaraFlag = true;
    private bool reelLeftBitaZugaraFlag = true;
    private bool reelRightBitaZugaraFlag = true;
    private bool reelCenterWaitTimeFlag = true;
    private bool reelLeftWaitTimeFlag = true;
    private bool reelRightWaitTimeFlag = true;
    private bool reelCenterStopFlag = false;
    private bool reelLeftStopFlag = false;
    private bool reelRightStopFlag = false;
    private int reelCenterSuberiKoma = 0;
    private int reelLeftSuberiKoma = 0;
    private int reelRightSuberiKoma = 0;

	private float reelSpeed = 7.5f;

	// Use this for initialization
	void Start () {
		reelCenter = GameObject.Find("reelCenter");
        reelLeft = GameObject.Find("reelLeft");
        reelRight = GameObject.Find("reelRight");
        reelCenterSpeed = reelSpeed;
        reelLeftSpeed = reelSpeed;
        reelRightSpeed = reelSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (Main.playFlag){
            // リールの回転時間を計るためのタイマーをセット
            if(reelTimerFlag){
                reelTimer = Time.time;
                reelTimerFlag = false;
            }

            // 各ボタンが押されたらスベリを計算しリールを止める
			if (Main.isReelCenterStop)  ReelCenterStop();
            if (Main.isReelLeftStop)    ReelLeftStop();
            if (Main.isReelRightStop)   ReelRightStop();

            // リール回転
            ReelRotate();

            // リールが三つ停止した場合、遊戯フラグをfalseにして初期化
            if (reelCount == 3)
            {
                Main.playFlag = false;
                initReel();
            }
		}
		else
		{

		}
	}

	private void ReelRotate()
    {
        reelCenter.transform.Rotate(new Vector3(reelCenterSpeed, 0, 0));
        reelLeft.transform.Rotate(new Vector3(reelLeftSpeed, 0, 0));
        reelRight.transform.Rotate(new Vector3(reelRightSpeed, 0, 0));
    }

	private void initReel()
	{
		reelCenterSpeed = reelSpeed;
        reelLeftSpeed = reelSpeed;
        reelRightSpeed = reelSpeed;
		reelCount = 0;
        reelTimerFlag = true;
	}

    private int CalcReelBitaZugara(int beforeZugara, float reelRotateTime)
    {
        int afterZugara;
        int suberi;
        float reelRotateAngle;

        reelRotateAngle = CalcReelRotateAngle(reelRotateTime);
        suberi = 1 + (int)(reelRotateAngle / (360.0f / 21.0f));
        afterZugara = beforeZugara + suberi;
        while(afterZugara > 21){
            afterZugara -= 21;
        }

        return afterZugara;
    }

    private float CalcReelWaitTime(int suberikoma, float reelRotateTime)
    {
        float reelRotateAngle;
        float suberiAngle;
        float suberiTime;

        reelRotateAngle = CalcReelRotateAngle(reelRotateTime);
        suberiAngle = 360.0f / 21.0f;
        while(reelRotateAngle > suberiAngle){
            suberiAngle += 360.0f / 21.0f;
        }
        suberiAngle = suberiAngle - reelRotateAngle;
        suberiTime = suberiAngle / (60.0f * reelSpeed);

        return (360.0f / 21.0f) / (60.0f * reelSpeed) * suberikoma + suberiTime;
    }

    private float CalcReelRotateAngle(float reelRotateTime)
    {
        float reelRotateAngle;

        reelRotateAngle = 60.0f * reelSpeed * reelRotateTime;
        while(reelRotateAngle >= 360.0f){
            reelRotateAngle -= 360.0f;
        }

        return reelRotateAngle;
    }

    private void ReelCenterStop()
    {
        if(reelCenterBitaZugaraFlag){
            reelCenterBitaZugara = CalcReelBitaZugara(reelCenterBitaZugara, Time.time - reelTimer);
            reelCenterBitaZugaraFlag = false;
            reelCenterStopFlag = true;
            Debug.Log("center:" + reelCenterBitaZugara);
        }
        if(reelCenterWaitTimeFlag){
            reelCenterSuberiKoma = ReelCenterSuberi(reelCount + 1, reelCenterStopFlag, reelLeftStopFlag, reelRightStopFlag);
            reelCenterWaitTime = CalcReelWaitTime(reelCenterSuberiKoma, Time.time - reelTimer);
            reelCenterBitaZugara += reelCenterSuberiKoma;
            reelCenterWaitTimeFlag = false;
        }
                
        reelCenterRotateTime += Time.deltaTime;
        if(reelCenterRotateTime >= reelCenterWaitTime){
            reelCenter.transform.rotation = Quaternion.Euler(-(float)(reelCenterBitaZugara + 1) * (360.0f / 21.0f) - 90.0f, 0, -180);
            reelCenterSpeed = 0;
    	    Main.isReelCenterStop = false;
            reelCenterRotateTime = 0;
            reelCenterBitaZugaraFlag = true;
            reelCenterWaitTimeFlag = true;
            reelCount++;
        }
    }

    private void ReelLeftStop()
    {
        if(reelLeftBitaZugaraFlag){
            reelLeftBitaZugara = CalcReelBitaZugara(reelLeftBitaZugara, Time.time - reelTimer);
            reelLeftBitaZugaraFlag = false;
            reelLeftStopFlag = true;
            Debug.Log("left:" + reelLeftBitaZugara);
        }
        if(reelLeftWaitTimeFlag){
            reelLeftSuberiKoma = ReelLeftSuberi(reelCount + 1, reelCenterStopFlag, reelLeftStopFlag, reelRightStopFlag);
            reelLeftWaitTime = CalcReelWaitTime(reelLeftSuberiKoma, Time.time - reelTimer);
            reelLeftBitaZugara += reelLeftSuberiKoma;
            reelLeftWaitTimeFlag = false;
        }
              
        reelLeftRotateTime += Time.deltaTime;
        if(reelLeftRotateTime >= reelLeftWaitTime){
            reelLeft.transform.rotation = Quaternion.Euler(-(float)(reelLeftBitaZugara + 1) * (360.0f / 21.0f) - 90.0f, 0, -180);
            reelLeftSpeed = 0;
		    Main.isReelLeftStop = false;
            reelLeftRotateTime = 0;
            reelLeftBitaZugaraFlag = true;
            reelLeftWaitTimeFlag = true;
            reelCount++;
        }
    }

    private void ReelRightStop()
    {
        if(reelRightBitaZugaraFlag){
            reelRightBitaZugara = CalcReelBitaZugara(reelRightBitaZugara, Time.time - reelTimer);
            reelRightBitaZugaraFlag = false;
            reelRightStopFlag = true;
            Debug.Log("right:" + reelRightBitaZugara);
        }
        if(reelRightWaitTimeFlag){
            reelRightSuberiKoma = ReelRightSuberi(reelCount + 1, reelCenterStopFlag, reelLeftStopFlag, reelRightStopFlag);
            reelRightWaitTime = CalcReelWaitTime(reelRightSuberiKoma, Time.time - reelTimer);
            reelRightBitaZugara += reelRightSuberiKoma;
            reelRightWaitTimeFlag = false;
        }
               
        reelRightRotateTime += Time.deltaTime;
        if(reelRightRotateTime >= reelRightWaitTime){
            reelRight.transform.rotation = Quaternion.Euler(-(float)(reelRightBitaZugara + 1) * (360.0f / 21.0f) - 90.0f, 0, -180);
            reelRightSpeed = 0;
		    Main.isReelRightStop = false;
            reelRightRotateTime = 0;
            reelRightBitaZugaraFlag = true;
            reelRightWaitTimeFlag = true;
            reelCount++;
        }
    }









    private int ReelCenterSuberi(int teishi, bool centerStopFlag, bool leftStopFlag, bool rightStopFlag)
    {
        return 0;
    }

    private int ReelLeftSuberi(int teishi, bool centerStopFlag, bool leftStopFlag, bool rightStopFlag)
    {
        return 0;
    }

    private int ReelRightSuberi(int teishi, bool centerStopFlag, bool leftStopFlag, bool rightStopFlag)
    {
        return 0;
    }
}
