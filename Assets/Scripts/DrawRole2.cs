using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRole2 : MonoBehaviour {

	private string role;
	
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(!Main.playFlag)
		{
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				Main.role = JudgeRole(Main.settei);
				Debug.Log(Main.role);
			}
		}
	}
	
	/*
		子役を判定するメソット。stringを返す。
		文字列はMain.csのroleArrayを参照。
	 */
	private string JudgeRole(int settei)
	{
		int Value = Random.Range(0,65536);

		if(settei == 1){

			/*設定1の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";
			}else if((21036 <= Value) && (Value < 21196)){
				return "単B";
			}else if((21196 <= Value) && (Value < 21264)){
				return "重B";
			}else if((21264 <= Value) && (Value < 21364)){
				return "単R";
			}else if((21364 <= Value) && (Value < 21408)){
				return "重R";
			}else{
				return "はずれ";
			}

		}else if(settei == 2){

			/*設定2の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";
			}else if((21036 <= Value) && (Value < 21200)){
				return "単B";
			}else if((21200 <= Value) && (Value < 21268)){
				return "重B";
			}else if((21268 <= Value) && (Value < 21372)){
				return "単R";
			}else if((21372 <= Value) && (Value < 21416)){
				return "重R";
			}else{
				return "はずれ";
			}
		
		}else if(settei == 3){

			/*設定3の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";
			}else if((21036 <= Value) && (Value < 21200)){
				return "単B";
			}else if((21196 <= Value) && (Value < 21268)){
				return "重B";
			}else if((21268 <= Value) && (Value < 21400)){
				return "単R";
			}else if((21400 <= Value) && (Value < 21456)){
				return "重R";
			}else{
				return "はずれ";
			}

		}else if(settei == 4){

			/*設定4の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";
			}else if((21036 <= Value) && (Value < 21208)){
				return "単B";
			}else if((21208 <= Value) && (Value < 21280)){
				return "重B";
			}else if((21280 <= Value) && (Value < 21424)){
				return "単R";
			}else if((21424 <= Value) && (Value < 21484)){
				return "重R";
			}else{
				return "はずれ";
			}
		
		}else if(settei == 5){

			/*設定5の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";
			}else if((21036 <= Value) && (Value < 21208)){
				return "単B";
			}else if((21208 <= Value) && (Value < 21280)){
				return "重B";
			}else if((21280 <= Value) && (Value < 21452)){
				return "単R";
			}else if((21452 <= Value) && (Value < 21524)){
				return "重R";
			}else{
				return "はずれ";
			}

		}else if(settei == 6){

			/*設定6の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";
			}else if((8978 <= Value) && (Value < 19583)){
				return "ブドウ";
			}else if((19583 <= Value) && (Value < 21423)){
				return "チェリー";
			}else if((21423 <= Value) && (Value < 21463)){
				return "ピエロ";
			}else if((21463 <= Value) && (Value < 21523)){
				return "ベル";
			}else if((21523 <= Value) && (Value < 21695)){
				return "単B";
			}else if((21695 <= Value) && (Value < 21767)){
				return "重B";
			}else if((21767 <= Value) && (Value < 21939)){
				return "単R";
			}else if((21939 <= Value) && (Value < 22011)){
				return "重R";
			}else{
				return "はずれ";
			}
	
		}else{
			Debug.Log("設定エラー！");
			Application.Quit();
			return null;
		}


	}
}
