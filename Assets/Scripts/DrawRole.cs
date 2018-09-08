using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRole : MonoBehaviour {

	

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
			if(!Main.playFlag)
			{
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
				{
					if(Main.bigFlag){
						Main.role = JudgeRole(9);
						Debug.Log(Main.role);
					}
					else if(Main.regFlag){
						Main.role = JudgeRole(8);
						Debug.Log(Main.role);
					}
					else if(Main.jyunbiFlag){
						Main.role = JudgeRole(7);
						Debug.Log(Main.role);
					}
					else{
						Main.role = JudgeRole(Main.settei);
						Debug.Log(Main.role);
						if((Main.role == "単b" || Main.role == "重b" || Main.role == "単r" || Main.role == "重r")){
							Main.jyunbiFlag = true;
						}
					}
					
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
				return "リプレイ";//リプレイ
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";//ブドウ
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";//チェリー
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";//ピエロ
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";//ベル
			}else if((21036 <= Value) && (Value < 21196)){
				return "単b";//単独BIG
			}else if((21196 <= Value) && (Value < 21264)){
				return "重b";//重複BIG
			}else if((21264 <= Value) && (Value < 21364)){
				return "単r";//単独REG
			}else if((21364 <= Value) && (Value < 21408)){
				return "単r";//重複REG
			}else{
				return "はずれ";//はずれ
			}

		}else if(settei == 2){

			/*設定2の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";//リプレイ
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";//ブドウ
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";//チェリー
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";//ピエロ
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";//ベル
			}else if((21036 <= Value) && (Value < 21200)){
				return "単b";//単B
			}else if((21200 <= Value) && (Value < 21268)){
				return "重b";//重B
			}else if((21268 <= Value) && (Value < 21372)){
				return "単r";//単R
			}else if((21372 <= Value) && (Value < 21416)){
				return "重r";//重R
			}else{
				return "はずれ";//はずれ
			}
		
		}else if(settei == 3){

			/*設定3の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";//リプレイ
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";//ブドウ
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";//チェリー
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";//ピエロ
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";//ベル
			}else if((21036 <= Value) && (Value < 21200)){
				return "単b";//単B
			}else if((21196 <= Value) && (Value < 21268)){
				return "重b";//重B
			}else if((21268 <= Value) && (Value < 21400)){
				return "単r";//単R
			}else if((21400 <= Value) && (Value < 21456)){
				return "重r";//重R
			}else{
				return "はずれ";//はずれ
			}

		}else if(settei == 4){

			/*設定4の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";//リプレイ
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";//ブドウ
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";//チェリー
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";//ピエロ
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";//ベル
			}else if((21036 <= Value) && (Value < 21208)){
				return "単b";//単B
			}else if((21208 <= Value) && (Value < 21280)){
				return "重b";//重B
			}else if((21280 <= Value) && (Value < 21424)){
				return "単r";//単R
			}else if((21424 <= Value) && (Value < 21484)){
				return "重r";//重R
			}else{
				return "はずれ";//はずれ
			}
		
		}else if(settei == 5){

			/*設定5の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";//リプレイ
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";//ブドウ
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";//チェリー
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";//ピエロ
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";//ベル
			}else if((21036 <= Value) && (Value < 21208)){
				return "単b";//単B
			}else if((21208 <= Value) && (Value < 21280)){
				return "重b";//重B
			}else if((21280 <= Value) && (Value < 21452)){
				return "単r";//単R
			}else if((21452 <= Value) && (Value < 21524)){
				return "重r";//重R
			}else{
				return "はずれ";//はずれ
			}

		}else if(settei == 6){

			/*設定6の場合 */
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";//リプレイ
			}else if((8978 <= Value) && (Value < 19583)){
				return "ブドウ";//ブドウ
			}else if((19583 <= Value) && (Value < 21423)){
				return "チェリー";//チェリー
			}else if((21423 <= Value) && (Value < 21463)){
				return "ピエロ";//ピエロ
			}else if((21463 <= Value) && (Value < 21523)){
				return "ベル";//ベル
			}else if((21523 <= Value) && (Value < 21695)){
				return "単b";//単B
			}else if((21695 <= Value) && (Value < 21767)){
				return "重b";//重B
			}else if((21767 <= Value) && (Value < 21939)){
				return "単r";//単R
			}else if((21939 <= Value) && (Value < 22011)){
				return "重r";//重R
			}else{
				return "はずれ";//はずれ
			}

		}else if(settei == 7){
			//ボーナス成立後ボーナス図柄揃いまで
			if((0 <= Value) && (Value < 8978)){
				return "リプレイ";//リプレイ
			}else if((8978 <= Value) && (Value < 19076)){
				return "ブドウ";//ブドウ
			}else if((19076 <= Value) && (Value < 20916)){
				return "チェリー";//チェリー
			}else if((20916 <= Value) && (Value < 20976)){
				return "ピエロ";//ピエロ
			}else if((20976 <= Value) && (Value < 21036)){
				return "ベル";//ベル
			}else {
				return "はずれ";//はずれ
			}

		}else if(settei == 9){
			//BIGボーナス消化中
			if((0 <= Value) && (Value < 1840)){
				return "チェリー";//チェリー
			}else if((1840 <= Value) && (Value < 1900)){
				return "ピエロ";//ピエロ
			}else if((1900 <= Value) && (Value < 1960)){
				return "ベル";//ベル
			}else{
				return "ブドウ";//ブドウ
			}

		}else if(settei == 8){
			//REGボーナス消化中
			return "ブドウ";//ブドウ
		
		}else{
			Debug.Log("設定エラー！");
			Application.Quit();
			return null;
		}


	}
}
