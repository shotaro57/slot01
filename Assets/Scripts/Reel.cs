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
    private int reelTeishi = 1;
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
	private float reelSpeed = 3.0f;

    private Dictionary<int, string> reelCenterArray = new Dictionary<int, string>()
    {
        {21 ,"リプレイ"},
        {20 ,"ベル"},
        {19 ,"ブドウ"},
        {18 ,"チェリー"},
        {17 ,"リプレイ"},
        {16 ,"赤7"},
        {15 ,"ブドウ"},
        {14 ,"チェリー"},
        {13 ,"リプレイ"},
        {12 ,"ベル"},
        {11 ,"ブドウ"},
        {10 ,"チェリー"},
        {9  ,"リプレイ"},
        {8  ,"黒バー"},
        {7  ,"ブドウ"},
        {6  ,"チェリー"},
        {5  ,"ピエロ"},
        {4  ,"リプレイ"},
        {3  ,"赤7"},
        {2  ,"ブドウ"},
        {1  ,"チェリー"}
    };

    private Dictionary<int, string> reelLeftArray = new Dictionary<int, string>()
    {
        {21 ,"黒バー"},
        {20 ,"ブドウ"},
        {19 ,"リプレイ"},
        {18 ,"ブドウ"},
        {17 ,"ベル"},
        {16 ,"赤7"},
        {15 ,"リプレイ"},
        {14 ,"ブドウ"},
        {13 ,"リプレイ"},
        {12 ,"ブドウ"},
        {11 ,"黒バー"},
        {10 ,"チェリー"},
        {9  ,"ブドウ"},
        {8  ,"リプレイ"},
        {7  ,"ブドウ"},
        {6  ,"赤7"},
        {5  ,"ピエロ"},
        {4  ,"ブドウ"},
        {3  ,"リプレイ"},
        {2  ,"ブドウ"},
        {1  ,"チェリー"}
    };

    private Dictionary<int, string> reelRightArray = new Dictionary<int, string>()
    {
        {21 ,"ブドウ"},
        {20 ,"ピエロ"},
        {19 ,"ベル"},
        {18 ,"リプレイ"},
        {17 ,"ブドウ"},
        {16 ,"赤7"},
        {15 ,"黒バー"},
        {14 ,"ベル"},
        {13 ,"リプレイ"},
        {12 ,"ブドウ"},
        {11 ,"ピエロ"},
        {10 ,"ベル"},
        {9  ,"リプレイ"},
        {8  ,"ブドウ"},
        {7  ,"ピエロ"},
        {6  ,"ベル"},
        {5  ,"リプレイ"},
        {4  ,"ブドウ"},
        {3  ,"ピエロ"},
        {2  ,"ベル"},
        {1  ,"リプレイ"}
    };

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
        reelTeishi = 1;
        reelTimerFlag = true;
        reelCenterStopFlag = false;
        reelLeftStopFlag = false;
        reelRightStopFlag = false;
	}

    private int CalcReelBitaZugara(int beforeZugara, float reelRotateTime)
    {
        int afterZugara;
        int suberi;
        float reelRotateAngle;

        reelRotateAngle = CalcReelRotateAngle(reelRotateTime);
        suberi = 1 + (int)(reelRotateAngle / (360.0f / 21.0f));
        afterZugara = beforeZugara + suberi;
        afterZugara = ChangeZugaraRange(afterZugara);

        return afterZugara;
    }

    private int ChangeZugaraRange(int value)
    {
        while(value > 21){
            value -= 21;
        }

        while(value <= 0){
            value += 21;
        }

        return value;
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
            reelCenterSuberiKoma = ReelCenterSuberi(reelTeishi, reelCenterStopFlag, reelLeftStopFlag, reelRightStopFlag);
            reelCenterWaitTime = CalcReelWaitTime(reelCenterSuberiKoma, Time.time - reelTimer);
            reelCenterBitaZugara += reelCenterSuberiKoma;
            reelCenterWaitTimeFlag = false;
            reelTeishi++;
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
            reelLeftSuberiKoma = ReelLeftSuberi(reelTeishi, reelCenterStopFlag, reelLeftStopFlag, reelRightStopFlag);
            reelLeftWaitTime = CalcReelWaitTime(reelLeftSuberiKoma, Time.time - reelTimer);
            reelLeftBitaZugara += reelLeftSuberiKoma;
            reelLeftWaitTimeFlag = false;
            reelTeishi++;
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
            reelRightSuberiKoma = ReelRightSuberi(reelTeishi, reelCenterStopFlag, reelLeftStopFlag, reelRightStopFlag);
            reelRightWaitTime = CalcReelWaitTime(reelRightSuberiKoma, Time.time - reelTimer);
            reelRightBitaZugara += reelRightSuberiKoma;
            reelRightWaitTimeFlag = false;
            reelTeishi++;
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
        if(teishi == 1){
            if(Main.role == "リプレイ"){
                int hanni = SearchReelRightArray("リプレイ", 0);

                if(hanni == -1)     return 0;
                else if(hanni == 0) return 0;
                else if(hanni == 1) return Random.Range(0,2);
                else if(hanni == 2) return Random.Range(1,3);
                else if(hanni == 3) return Random.Range(2,4);
                else if(hanni == 4) return 3;
                else if(hanni == 5) return 4;
                else                return Random.Range(0,3);

            }else if(Main.role == "ブドウ"){
                int hanni = SearchReelRightArray("ブドウ", 0);

                if(hanni == -1)     return 0;
                else if(hanni == 0) return 0;
                else if(hanni == 1) return Random.Range(0,2);
                else if(hanni == 2) return Random.Range(1,3);
                else if(hanni == 3) return Random.Range(2,4);
                else if(hanni == 4) return 3;
                else if(hanni == 5) return 4;
                else                return Random.Range(0,3);

            }else if(Main.role == "チェリー"){
                return Random.Range(0,3);

            }else if(Main.role == "ピエロ"){
                int hanni = SearchReelRightArray("ピエロ", 0);
                
                if(hanni == -1)     return 0;
                else if(hanni == 0) return 0;
                else if(hanni == 1) return Random.Range(0,2);
                else if(hanni == 2) return Random.Range(1,3);
                else if(hanni == 3) return Random.Range(2,4);
                else if(hanni == 4) return 3;
                else if(hanni == 5) return 4;
                else                return Random.Range(0,3);

            }else if(Main.role == "ベル"){
                int hanni = SearchReelRightArray("ベル", 0);
                
                if(hanni == -1)     return 0;
                else if(hanni == 0) return 0;
                else if(hanni == 1) return Random.Range(0,2);
                else if(hanni == 2) return Random.Range(1,3);
                else if(hanni == 3) return Random.Range(2,4);
                else if(hanni == 4) return 3;
                else if(hanni == 5) return 4;
                else                return Random.Range(0,3);

            }else if(Main.role == "単b"){
                int hanni = SearchReelRightArray("赤7", 0);
                
                if(hanni == -1)     return 0;
                else if(hanni == 0) return 0;
                else if(hanni == 1) return Random.Range(0,2);
                else if(hanni == 2) return Random.Range(1,3);
                else if(hanni == 3) return Random.Range(2,4);
                else if(hanni == 4) return 3;
                else if(hanni == 5) return 4;
                else                return Random.Range(0,3);

            }else if(Main.role == "重b"){
                int hanni = SearchReelRightArray("赤7", 0);
                
                if(hanni == -1)     return 0;
                else if(hanni == 0) return 0;
                else if(hanni == 1) return Random.Range(0,2);
                else if(hanni == 2) return Random.Range(1,3);
                else if(hanni == 3) return Random.Range(2,4);
                else if(hanni == 4) return 3;
                else if(hanni == 5) return 4;
                else                return Random.Range(0,3);

            }else if(Main.role == "単r"){
                int hanni = SearchReelRightArray("黒バー", 0);
                
                if(hanni == -1)     return 0;
                else if(hanni == 0) return 0;
                else if(hanni == 1) return Random.Range(0,2);
                else if(hanni == 2) return Random.Range(1,3);
                else if(hanni == 3) return Random.Range(2,4);
                else if(hanni == 4) return 3;
                else if(hanni == 5) return 4;
                else                return Random.Range(0,3);

            }else if(Main.role == "重r"){
                int hanni = SearchReelRightArray("黒バー", 0);
                
                if(hanni == -1)     return 0;
                else if(hanni == 0) return 0;
                else if(hanni == 1) return Random.Range(0,2);
                else if(hanni == 2) return Random.Range(1,3);
                else if(hanni == 3) return Random.Range(2,4);
                else if(hanni == 4) return 3;
                else if(hanni == 5) return 4;
                else                return Random.Range(0,3);

            }else{
                return Random.Range(0,3);
            }

        }else if(teishi == 2){
            if(leftStopFlag == true && centerStopFlag == false){
                if(Main.role == "リプレイ"){
                    int leftPlace = SearchReelLeftArray("リプレイ", 0);

                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("リプレイ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("リプレイ", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("リプレイ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }

                }else if(Main.role == "ブドウ"){
                    int leftPlace = SearchReelLeftArray("ブドウ", 0);

                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("ブドウ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ブドウ", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("ブドウ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }

                }else if(Main.role == "チェリー"){
                    return Random.Range(0,3);

                }else if(Main.role == "ピエロ"){
                    int leftPlace = SearchReelLeftArray("ピエロ", 0);

                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }

                }else if(Main.role == "ベル"){
                    int leftPlace = SearchReelLeftArray("ベル", 0);

                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("ベル", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ベル", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("ベル", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }
                    
                }else if(Main.role == "単b"){
                    int leftPlace = SearchReelLeftArray("赤7", 0);

                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }

                }else if(Main.role == "重b"){
                    int leftPlace = SearchReelLeftArray("赤7", 0);

                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }
                  
                }else if(Main.role == "単r"){
                    int leftPlace = SearchReelLeftArray("赤7", 0);

                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }
                  
                }else if(Main.role == "重r"){
                    int leftPlace = SearchReelLeftArray("赤7", 0);

                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                }else{
                    return Random.Range(0,3);
                }

            }else if(leftStopFlag == false && centerStopFlag == true){
                if(Main.role == "リプレイ"){
                    int centerPlace = SearchReelCenterArray("リプレイ", 0);

                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("リプレイ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("リプレイ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("リプレイ", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }

                }else if(Main.role == "ブドウ"){
                    int centerPlace = SearchReelCenterArray("ブドウ", 0);

                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("ブドウ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ブドウ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("ブドウ", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }

                }else if(Main.role == "チェリー"){
                    return Random.Range(0,3);

                }else if(Main.role == "ピエロ"){
                    int centerPlace = SearchReelCenterArray("ピエロ", 0);

                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }

                }else if(Main.role == "ベル"){
                    int centerPlace = SearchReelCenterArray("ベル", 0);

                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("ベル", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ベル", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("ベル", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }

                }else if(Main.role == "単b"){
                    int centerPlace = SearchReelCenterArray("赤7", 0);

                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("赤7", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }

                }else if(Main.role == "重b"){
                    int centerPlace = SearchReelCenterArray("赤7", 0);

                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("赤7", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }

                }else if(Main.role == "単r"){
                    int centerPlace = SearchReelCenterArray("赤7", 0);

                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }

                }else if(Main.role == "重r"){
                    int centerPlace = SearchReelCenterArray("赤7", 0);

                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return Random.Range(0,2);
                        else if(hanni == 1) return Random.Range(0,3);
                        else if(hanni == 2) return Random.Range(1,3);
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }

                }else{
                    return Random.Range(0,3);
                }
            }

        }else if(teishi == 3){
            if(Main.role == "リプレイ"){
                int leftPlace = SearchReelLeftArray("リプレイ", 0);
                int centerPlace = SearchReelCenterArray("リプレイ", 0);

                if(centerPlace == -1)
                {
                    int hanni = SearchReelRightArray("リプレイ", 0);

                    if(hanni == -1)     return 0;
                    else if(hanni == 0) return 1;
                    else if(hanni == 1) return 2;
                    else if(hanni == 2) return 3;
                    else if(hanni == 3) return 4;
                    else                return Random.Range(0,3);
                }
                else if(centerPlace == 0)
                {
                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("リプレイ", 2);

                        if(hanni == 1)      return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("リプレイ", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("リプレイ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(centerPlace == 1)
                {
                    int hanni = SearchReelRightArray("リプレイ", 2);

                    if(hanni == 1)      return 0;
                    else if(hanni == 2) return 1;
                    else if(hanni == 3) return 2;
                    else if(hanni == 4) return 3;
                    else if(hanni == 5) return 4;
                    else                return Random.Range(0,3);
                }
                else
                {
                    int hanni = SearchReelRightArray("赤7", 0);

                    if(hanni == -1)     return Random.Range(1,3);
                    else if(hanni == 0) return Random.Range(2,5);
                    else if(hanni == 1) return Random.Range(3,5);
                    else if(hanni == 2) return 4;
                    else if(hanni == 3) return 0;
                    else if(hanni == 4) return Random.Range(0,2);
                    else                return Random.Range(0,3);
                }

            }else if(Main.role == "ブドウ"){
                int leftPlace = SearchReelLeftArray("ブドウ", 0);
                int centerPlace = SearchReelCenterArray("ブドウ", 0);

                if(centerPlace == -1)
                {
                    int hanni = SearchReelRightArray("ブドウ", 0);

                    if(hanni == -1)     return 0;
                    else if(hanni == 0) return 1;
                    else if(hanni == 1) return 2;
                    else if(hanni == 2) return 3;
                    else if(hanni == 3) return 4;
                    else                return Random.Range(0,3);
                }
                else if(centerPlace == 0)
                {
                    if(leftPlace == -1)
                    {
                        int hanni = SearchReelRightArray("ブドウ", 2);

                        if(hanni == 1)      return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ブドウ", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(leftPlace == 1)
                    {
                        int hanni = SearchReelRightArray("ブドウ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(centerPlace == 1)
                {
                    int hanni = SearchReelRightArray("ブドウ", 2);

                    if(hanni == 1)      return 0;
                    else if(hanni == 2) return 1;
                    else if(hanni == 3) return 2;
                    else if(hanni == 4) return 3;
                    else if(hanni == 5) return 4;
                    else                return Random.Range(0,3);
                }
                else
                {
                    int hanni = SearchReelRightArray("赤7", 0);

                    if(hanni == -1)     return Random.Range(1,3);
                    else if(hanni == 0) return Random.Range(2,5);
                    else if(hanni == 1) return Random.Range(3,5);
                    else if(hanni == 2) return 4;
                    else if(hanni == 3) return 0;
                    else if(hanni == 4) return Random.Range(0,2);
                    else                return Random.Range(0,3);
                }

            }else if(Main.role == "チェリー"){
                return Random.Range(0,3);
            
            }else if(Main.role == "ピエロ"){
                int leftPlace = SearchReelLeftArray("ピエロ", 0);
                int centerPlace = SearchReelCenterArray("ピエロ", 0);

                if(leftPlace == -1)
                {
                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 0)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 1)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("ピエロ", 2);

                        if(hanni == 1)      return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else
                {
                    int hanni = SearchReelRightArray("赤7", 0);

                    if(hanni == -1)     return Random.Range(1,3);
                    else if(hanni == 0) return Random.Range(2,5);
                    else if(hanni == 1) return Random.Range(3,5);
                    else if(hanni == 2) return 4;
                    else if(hanni == 3) return 0;
                    else if(hanni == 4) return Random.Range(0,2);
                    else                return Random.Range(0,3);
                }

            }else if(Main.role == "ベル"){
                int leftPlace = SearchReelLeftArray("ベル", 0);
                int centerPlace = SearchReelCenterArray("ベル", 0);

                if(leftPlace == -1)
                {
                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("ベル", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ベル", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 0)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ベル", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 1)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("ベル", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("ベル", 2);

                        if(hanni == 1)      return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else
                {
                    int hanni = SearchReelRightArray("赤7", 0);

                    if(hanni == -1)     return Random.Range(1,3);
                    else if(hanni == 0) return Random.Range(2,5);
                    else if(hanni == 1) return Random.Range(3,5);
                    else if(hanni == 2) return 4;
                    else if(hanni == 3) return 0;
                    else if(hanni == 4) return Random.Range(0,2);
                    else                return Random.Range(0,3);
                }

            }else if(Main.role == "単b"){
                int leftPlace = SearchReelLeftArray("赤7", 0);
                int centerPlace = SearchReelCenterArray("赤7", 0);

                if(leftPlace == -1)
                {
                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 0)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 1)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("赤7", 2);

                        if(hanni == 1)      return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }
                }
                else
                {
                    return Random.Range(0,3);
                }
            
            }else if(Main.role == "重b"){
                int leftPlace = SearchReelLeftArray("赤7", 0);
                int centerPlace = SearchReelCenterArray("赤7", 0);

                if(leftPlace == -1)
                {
                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 0)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 1)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(0,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("赤7", 2);

                        if(hanni == 1)      return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(0,3);
                    }
                    else
                    {
                        return Random.Range(0,3);
                    }
                }
                else
                {
                    int hanni = SearchReelRightArray("赤7", 0);

                    if(hanni == -1)     return Random.Range(1,3);
                    else if(hanni == 0) return 0;
                    else if(hanni == 1) return Random.Range(0,2);
                    else if(hanni == 2) return Random.Range(1,3);
                    else if(hanni == 3) return Random.Range(2,4);
                    else if(hanni == 4) return Random.Range(3,5);
                    else                return Random.Range(0,3);
                }

            }else if(Main.role == "単r"){
                int leftPlace = SearchReelLeftArray("赤7", 0);
                int centerPlace = SearchReelCenterArray("赤7", 0);

                if(leftPlace == -1)
                {
                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(1,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(1,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 0)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(1,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 1)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(1,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 2);

                        if(hanni == 1)      return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(1,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else
                {
                    int hanni = SearchReelRightArray("赤7", 0);

                    if(hanni == -1)     return Random.Range(1,3);
                    else if(hanni == 0) return Random.Range(2,5);
                    else if(hanni == 1) return Random.Range(3,5);
                    else if(hanni == 2) return 4;
                    else if(hanni == 3) return 0;
                    else if(hanni == 4) return Random.Range(0,2);
                    else                return Random.Range(0,3);
                }

            }else if(Main.role == "重r"){
                int leftPlace = SearchReelLeftArray("赤7", 0);
                int centerPlace = SearchReelCenterArray("赤7", 0);

                if(leftPlace == -1)
                {
                    if(centerPlace == -1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(1,3);
                    }
                    else if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 2);

                        if(hanni == 1) return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(1,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 0)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 1);

                        if(hanni == 0)      return 0;
                        else if(hanni == 1) return 1;
                        else if(hanni == 2) return 2;
                        else if(hanni == 3) return 3;
                        else if(hanni == 4) return 4;
                        else                return Random.Range(1,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else if(leftPlace == 1)
                {
                    if(centerPlace == 0)
                    {
                        int hanni = SearchReelRightArray("黒バー", 0);

                        if(hanni == -1)     return 0;
                        else if(hanni == 0) return 1;
                        else if(hanni == 1) return 2;
                        else if(hanni == 2) return 3;
                        else if(hanni == 3) return 4;
                        else                return Random.Range(1,3);
                    }
                    else if(centerPlace == 1)
                    {
                        int hanni = SearchReelRightArray("黒バー", 2);

                        if(hanni == 1)      return 0;
                        else if(hanni == 2) return 1;
                        else if(hanni == 3) return 2;
                        else if(hanni == 4) return 3;
                        else if(hanni == 5) return 4;
                        else                return Random.Range(1,3);
                    }
                    else
                    {
                        int hanni = SearchReelRightArray("赤7", 0);

                        if(hanni == -1)     return Random.Range(1,3);
                        else if(hanni == 0) return Random.Range(2,5);
                        else if(hanni == 1) return Random.Range(3,5);
                        else if(hanni == 2) return 4;
                        else if(hanni == 3) return 0;
                        else if(hanni == 4) return Random.Range(0,2);
                        else                return Random.Range(0,3);
                    }
                }
                else
                {
                    int hanni = SearchReelRightArray("赤7", 0);

                    if(hanni == -1)     return Random.Range(1,3);
                    else if(hanni == 0) return 0;
                    else if(hanni == 1) return Random.Range(0,2);
                    else if(hanni == 2) return Random.Range(1,3);
                    else if(hanni == 3) return Random.Range(2,4);
                    else if(hanni == 4) return Random.Range(3,5);
                    else                return Random.Range(0,3);
                }

            }else{
                List<int> suberiList = new List<int>();

                for(int i = 0; i <= 4; i++){
                    // 中段の有効ライン
                    if( reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara)] == reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] &&
                        reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i)] ){
                            continue;
                        }
                    if( (reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara)] == "赤7" || reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara)] == "黒バー") &&
                        (reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == "赤7" || reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == "黒バー") &&
                        (reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i)] == "赤7" || reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i)] == "黒バー") ){
                            continue;
                        }
                    // 上段の有効ライン
                    if( reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 1)] == reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara + 1)] &&
                        reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara + 1)] == reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i + 1)] ){
                            continue;
                        }
                    if( (reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 1)] == "赤7" || reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 1)] == "黒バー") &&
                        (reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara + 1)] == "赤7" || reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara + 1)] == "黒バー") &&
                        (reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i + 1)] == "赤7" || reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i + 1)] == "黒バー") ){
                            continue;
                        }
                    // 下段の有効ライン
                    if( reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara - 1)] == reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara - 1)] &&
                        reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara - 1)] == reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i - 1)] ){
                            continue;
                        }
                    if( (reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara - 1)] == "赤7" || reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara - 1)] == "黒バー") &&
                        (reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara - 1)] == "赤7" || reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara - 1)] == "黒バー") &&
                        (reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i - 1)] == "赤7" || reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i - 1)] == "黒バー") ){
                            continue;
                        }
                    // 右下がりの有効ライン
                    if( reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 1)] == reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] &&
                        reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i - 1)] ){
                            continue;
                        }
                    if( (reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 1)] == "赤7" || reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 1)] == "黒バー") &&
                        (reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == "赤7" || reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == "黒バー") &&
                        (reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i - 1)] == "赤7" || reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i - 1)] == "黒バー") ){
                            continue;
                        }
                    // 右上がりの有効ライン
                    if( reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara - 1)] == reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] &&
                        reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i + 1)] ){
                            continue;
                        }
                    if( (reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara - 1)] == "赤7" || reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara - 1)] == "黒バー") &&
                        (reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == "赤7" || reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == "黒バー") &&
                        (reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i + 1)] == "赤7" || reelRightArray[ChangeZugaraRange(reelRightBitaZugara + i + 1)] == "黒バー") ){
                            continue;
                        }

                    suberiList.Add(i);
                }

                return suberiList[Random.Range(0, suberiList.Count)];
            }
        }

        Debug.Log("debug");
        return 2;
    }

    private int SearchReelCenterArray(string rool, int searchFlag)
    {
        if(searchFlag <= 0)
        {
            if(reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara - 1)] == rool){
                return -1;
            }
        }
        if(searchFlag <= 1)
        {
            if(reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara)] == rool){
                return 0;
            }
        }

        if(reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara + 1)] == rool){
            return 1;
        }else if(reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara + 2)] == rool){
            return 2;
        }else if(reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara + 3)] == rool){
            return 3;
        }else if(reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara + 4)] == rool){
            return 4;
        }else if(reelCenterArray[ChangeZugaraRange(reelCenterBitaZugara + 5)] == rool){
            return 5;
        }

        return 6;
    }

    private int SearchReelLeftArray(string rool, int searchFlag)
    {
        if(searchFlag <= 0)
        {
            if(reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara - 1)] == rool){
                return -1;
            }
        }
        if(searchFlag <= 1)
        {
            if(reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara)] == rool){
                return 0;
            }
        }

        if(reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 1)] == rool){
            return 1;
        }else if(reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 2)] == rool){
            return 2;
        }else if(reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 3)] == rool){
            return 3;
        }else if(reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 4)] == rool){
            return 4;
        }else if(reelLeftArray[ChangeZugaraRange(reelLeftBitaZugara + 5)] == rool){
            return 5;
        }

        return 6;
    }

    private int SearchReelRightArray(string rool, int searchFlag)
    {
        if(searchFlag <= 0)
        {
            if(reelRightArray[ChangeZugaraRange(reelRightBitaZugara - 1)] == rool){
                return -1;
            }
        }
        if(searchFlag <= 1)
        {
            if(reelRightArray[ChangeZugaraRange(reelRightBitaZugara)] == rool){
                return 0;
            }
        }

        if(reelRightArray[ChangeZugaraRange(reelRightBitaZugara + 1)] == rool){
            return 1;
        }else if(reelRightArray[ChangeZugaraRange(reelRightBitaZugara + 2)] == rool){
            return 2;
        }else if(reelRightArray[ChangeZugaraRange(reelRightBitaZugara + 3)] == rool){
            return 3;
        }else if(reelRightArray[ChangeZugaraRange(reelRightBitaZugara + 4)] == rool){
            return 4;
        }else if(reelRightArray[ChangeZugaraRange(reelRightBitaZugara + 5)] == rool){
            return 5;
        }

        return 6;
    }
}
