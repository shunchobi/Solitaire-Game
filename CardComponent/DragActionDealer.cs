using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DragActionDealer : MonoBehaviour {

    CardInfo cardInfo;
    GameObject willOya = null;

    List<GameObject> willOyas;


    private void Start()
    {
        cardInfo = this.gameObject.GetComponent<CardInfo>();
        willOyas = new List<GameObject>();
    }



    private void OnTriggerEnter2D(Collider2D co)
    {
        bool flag = false;
        for (int i = 0; i < willOyas.Count; i++)
        {
            GameObject target = willOyas[i];
            if (target == co.gameObject)
                flag = true;
        }

        if (!flag)
            willOyas.Add(co.gameObject);
    }

    private void OnTriggerExit2D(Collider2D co)
    {
        bool flag = false;
        for (int i = 0; i < willOyas.Count; i++){
            GameObject target = willOyas[i];
            if (target == co.gameObject)
                flag = true;
        }

        if (flag)
            willOyas.Remove(co.gameObject);
    }



    void OnMouseUp(){

        float oridinalPos = this.gameObject.GetComponent<Dragger>().oridinalPos.x;
        float nowPos = this.gameObject.transform.position.x;
        float abs = Math.Abs(oridinalPos - nowPos);
        if (abs > 15f)
            cardInfo.isDragged = true;

        if(UiManager.isMovedCard == false)
        {
            UiManager.GetPlayUi(false);
            UiManager.PlayUi(true);
            UiManager.isMovedCard = true;
        }

        DealCardsByActions();
    }




    void DealCardsByActions()
    {
        this.gameObject.GetComponent<TapActionDealer>().DealTapAction();

        //カードがドラッグされた場合
        if (cardInfo.isDragged && willOyas.Count > 0)
        {

            bool canBeOya = false;

            for (int i = 0; i < willOyas.Count; i++)
            {
                willOya = willOyas[i];
                if (willOya.name == "row8") continue;

                //oyaになれるか調べる
                string oyaPlace = willOya.GetComponent<CardInfo>().place;

                if (oyaPlace == Cash.yama || oyaPlace == Cash.yama_empty) canBeOya = RuleYama.CheckAcceptability(this.gameObject, willOya);
                else if (oyaPlace == Cash.retu || oyaPlace == Cash.retu_empty) canBeOya = RuleRetu.CheckAcceptability(this.gameObject, willOya, true);

                if (canBeOya == true)
                    break;
            }

            willOyas.Clear();

            //oyaになるカードが見つからなかったら
            if (canBeOya == false)
            {
                //カードをドラッグする前の場所に戻す
                this.gameObject.GetComponent<Dragger>().MoveBackToOridinalPos();
                willOya = null;
                cardInfo.isDragged = false;
                return;
            }
            //可能だったらwillOyaのchildになる
            ChildBecomer.BecameChild(this.gameObject, willOya);

        }
        else if (cardInfo.isDragged && willOya == null){
            //カードをドラッグする前の場所に戻す
            this.gameObject.GetComponent<Dragger>().MoveBackToOridinalPos();
        }
        else if(cardInfo.isDragged && willOya.name == "row8"){
            //カードをドラッグする前の場所に戻す
            this.gameObject.GetComponent<Dragger>().MoveBackToOridinalPos();

        }

        willOya = null;
        cardInfo.isDragged = false;
    }


}
