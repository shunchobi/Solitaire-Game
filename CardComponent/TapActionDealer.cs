using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapActionDealer : MonoBehaviour {



    CardInfo cardInfo;


    private void Start()
    {
        cardInfo = this.gameObject.GetComponent<CardInfo>();
    }



    public void DealTapAction()
    {
        //カードがタップされた場合
        if (!cardInfo.isDragged) {

            //retuの裏カードをタップでひっくり返す
            if (cardInfo.isFront == false && cardInfo.place == Cash.retu)
            {
                this.gameObject.GetComponent<Rotater>().RotateToFront();
                bool ableToComp = AutoCompleteChecker.CheckAbleToAutoComplete();
                if (ableToComp == true)
                    UiController.autoCompleteObjDirec.AbleAutoCompObj();
                return;
            }


            bool isOyaFound = false;
            GameObject willOya = null;

            //oyaになれる空オブジェクトがいるか調べる(yamaの空を優先でoyaにしたいから)
            for (int n = 12; n >= 9; n--)
            {
                if (n == 8) continue;
                willOya = EmptyObjectReturner.GetEmptyObj(n);
                string oyaPlace = willOya.GetComponent<CardInfo>().place;

                if (oyaPlace == Cash.yama_empty) isOyaFound = RuleYama.CheckAcceptability(this.gameObject, willOya);
                else if (oyaPlace == Cash.retu_empty) isOyaFound = RuleRetu.CheckAcceptability(this.gameObject, willOya, false);

                if (isOyaFound == true) break;
            }


            //oyaになるカードがいるか調べる
            if (isOyaFound == false){
                for (int i = 0; i < 4; i++)
                {
                    if (GameListHolder.gameLists[12 - i].Count == 0) continue;

                    willOya = GameListHolder.gameLists[12 - i][GameListHolder.gameLists[12 - i].Count - 1];
                    string oyaPlace = willOya.GetComponent<CardInfo>().place;

                    if (oyaPlace == Cash.yama || oyaPlace == Cash.yama_empty) isOyaFound = RuleYama.CheckAcceptability(this.gameObject, willOya);
                    else if (oyaPlace == Cash.retu || oyaPlace == Cash.retu_empty) isOyaFound = RuleRetu.CheckAcceptability(this.gameObject, willOya, false);

                    if (isOyaFound == true) break;
                }
            }


            //oyaになるカードが見つからなかったら
            if (isOyaFound == false){
                willOya = null;
                cardInfo.isDragged = false;

                //カードをドラッグする前の場所に戻す
                this.gameObject.GetComponent<Dragger>().MoveBackToOridinalPos();

                return;
                //yield break;
            }

            //CardsUntouchabler.UntouchableAllCards();

            //いたらそのoyaのchildになる
            ChildBecomer.BecameChild(this.gameObject, willOya);

            //yield return new WaitForSeconds(Cash.speedToRetuYama);
            //CardsUntouchabler.TouchableAllCards();
        }


    }


}
