using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelController : MonoBehaviour {

    private GameObject reelCenter;
    private GameObject reelLeft;
    private GameObject reelRight;
    private float reelCenterSpeed;
    private float reelLeftSpeed;
    private float reelRightSpeed;
    private GameObject buttonCenter;
    private GameObject buttonLeft;
    private GameObject buttonRight;
    private int playFlag = 0;
    private int reelCount = 0;
    private GameObject rever;

    [SerializeField] private float reelSpeed = 1;

    // Use this for initialization
    void Start () {
        reelCenter = GameObject.Find("reelCenter");
        reelLeft = GameObject.Find("reelLeft");
        reelRight = GameObject.Find("reelRight");
        reelCenterSpeed = reelSpeed;
        reelLeftSpeed = reelSpeed;
        reelRightSpeed = reelSpeed;
        buttonCenter = GameObject.Find("buttonCenter");
        buttonLeft = GameObject.Find("buttonLeft");
        buttonRight = GameObject.Find("buttonRight");
        buttonCenter.GetComponent<Renderer>().material.color = Color.white;
        buttonLeft.GetComponent<Renderer>().material.color = Color.white;
        buttonRight.GetComponent<Renderer>().material.color = Color.white;
        rever = GameObject.Find("rever");
    }
	
	// Update is called once per frame
	void Update () {
        if (playFlag == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                ReverAnimetion();

                playFlag = 1;
                reelCenterSpeed = reelSpeed;
                reelLeftSpeed = reelSpeed;
                reelRightSpeed = reelSpeed;
                buttonCenter.GetComponent<Renderer>().material.color = Color.blue;
                buttonLeft.GetComponent<Renderer>().material.color = Color.blue;
                buttonRight.GetComponent<Renderer>().material.color = Color.blue;
            }
        }else if (playFlag == 1)
        {
            ReelRotate();
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                reelCenterSpeed = 0;
                buttonCenter.GetComponent<Renderer>().material.color = Color.red;
                reelCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                reelLeftSpeed = 0;
                buttonLeft.GetComponent<Renderer>().material.color = Color.red;
                reelCount++;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                reelRightSpeed = 0;
                buttonRight.GetComponent<Renderer>().material.color = Color.red;
                reelCount++;
            }
            if (reelCount == 3)
            {
                playFlag = 0;
                reelCount = 0;
            }
        }


        
    }

    private void ReelRotate()
    {
        reelCenter.transform.Rotate(new Vector3(reelCenterSpeed, 0, 0));
        reelLeft.transform.Rotate(new Vector3(reelLeftSpeed, 0, 0));
        reelRight.transform.Rotate(new Vector3(reelRightSpeed, 0, 0));
    }

    private void ReverAnimetion()
    {

    }
}
/*
練習
 */