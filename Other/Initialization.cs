using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        Cash.Init();
        UiController.Init();

        //ローカライズ
        L.Text.Init(Application.systemLanguage);
        //カードなどのポジション決め
        PlacePos placePos = new PlacePos();
        placePos.MakePosAndScale();
        //カードを生成する
        CardsGenerater cardsGenerater = new CardsGenerater();
        cardsGenerater.Init();

        if(Cash.preference.isRightHand == false){
            HandChanger handChanger = new HandChanger();
            handChanger.Init();
            handChanger.ChangePlayHand(Cash.preference.isRightHand);
        }

        UiManager.Init();
        UiManager.HomeUi(true);

	}
	
}
