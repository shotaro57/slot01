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

	[SerializeField] private float reelSpeed = 1;

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
			ReelRotate();

			if (Main.isReelCenterStop)
            {
                reelCenterSpeed = 0;
				Main.isReelCenterStop = false;
                reelCount++;
            }
            if (Main.isReelLeftStop)
            {
                reelLeftSpeed = 0;
				Main.isReelLeftStop = false;
                reelCount++;
            }
            if (Main.isReelRightStop)
            {
                reelRightSpeed = 0;
				Main.isReelRightStop = false;
                reelCount++;
            }

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
	}
}
