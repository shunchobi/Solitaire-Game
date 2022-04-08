using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoCardsDealer : MonoBehaviour
{





    public static void DealUndoCards(List<GameObject> undoCards, int exListNum, bool retuReturned)
    {

        GameObject referencer = undoCards[0];
        List<GameObject> exList = GameListHolder.gameLists[exListNum];
        List<GameObject> nowList = OwnListReturner.GetList(referencer);

        GameObject oya = null;
        if (exList.Count > 0)
            oya = exList[exList.Count - 1];
        else if (exListNum >= 9 && exList.Count == 0)
            oya = EmptyObjectReturner.GetEmptyObj(exListNum);
        else if (exListNum <= 8 && exList.Count == 0)
            oya = EmptyObjectReturner.GetEmptyObj(exListNum + 1);

        GameObject nowEmptyObj = null;
        int undoCardListInt = referencer.GetComponent<CardInfo>().placeListInt;
        if (undoCardListInt >= 9)
            nowEmptyObj = EmptyObjectReturner.GetEmptyObj(undoCardListInt);
        else if (exListNum <= 8)
            nowEmptyObj = EmptyObjectReturner.GetEmptyObj(undoCardListInt + 1);


        string nowPlace = referencer.GetComponent<CardInfo>().place;
        string willPlace = PlaceReturner.GetPlaceFromInt(exListNum);


        GameObject undoCard = undoCards[undoCards.Count - 1];
        Vector3 pos = Vector3.zero;
        SpriteRenderer spriteRenderer = undoCard.GetComponent<SpriteRenderer>();


        //yama to yama へ戻る(Aが空のyama間の移動のみ対象）
        //BoxColliのtrue,false
        if (nowPlace == Cash.yama && willPlace == Cash.yama)
        {
            oya.GetComponent<BoxCollider2D>().enabled = false;
            nowEmptyObj.GetComponent<BoxCollider2D>().enabled = true;
            pos = oya.transform.position;

            spriteRenderer.sortingOrder = 90;
            undoCard.GetComponent<Mover>().MoveToPosition(pos, Cash.speedToRetuYama);
            CardMoverBtwLists.ChangeCardGameList(undoCard, exList, nowList);
        }



        //yama to retu へ戻る
        //BoxColliのtrue,false
        //BoxColliのサイズ
        if (nowPlace == Cash.yama && willPlace == Cash.retu)
        {
            if (nowList.Count == 1)
                nowEmptyObj.GetComponent<BoxCollider2D>().enabled = true;
            else if (nowList.Count >= 2)
                nowList[nowList.Count - 2].GetComponent<BoxCollider2D>().enabled = true;

            if (exList.Count == 0)
            {
                oya.GetComponent<BoxCollider2D>().enabled = false;
                pos = oya.transform.position;
            }

            if (exList.Count == 1)
            {
                oya.GetComponent<BoxCollider2D>().enabled = false;
                oya.GetComponent<Rotater>().RotateToBack();
                pos = new Vector3(oya.transform.position.x, oya.transform.position.y - Cash.cardPos.spaceBtwRetu_Back, 0);
                //score
                ScoreCounter.AddScore(-ScorePoint.flipedRetu);
            }

            if (exList.Count >= 2)
            {
                if (exList[exList.Count - 2].GetComponent<CardInfo>().isFront == false)
                {
                    oya.GetComponent<Rotater>().RotateToBack();
                    oya.GetComponent<BoxCollider2D>().enabled = false;
                    pos = new Vector3(oya.transform.position.x, oya.transform.position.y - Cash.cardPos.spaceBtwRetu_Back, 0);
                    //score
                    ScoreCounter.AddScore(-ScorePoint.flipedRetu);
                }
                else
                {
                    oya.GetComponent<BC2DSizeChanger>().ChangeBxcSizeToVisible();
                    pos = new Vector3(oya.transform.position.x, oya.transform.position.y - Cash.cardPos.spaceBtwRetu_Front, 0);
                }
            }

            spriteRenderer.sortingOrder = 90;
            undoCard.GetComponent<Mover>().MoveToPosition(pos, Cash.speedToRetuYama);
            CardMoverBtwLists.ChangeCardGameList(undoCard, exList, nowList);

            //score
            ScoreCounter.AddScore(-ScorePoint.toYama);
        }


        //yama to opendDeck へ戻る 
        //BoxColliのtrue,false
        //opendDeckを詰める
        if (nowPlace == Cash.yama && willPlace == Cash.opendDeck)
        {
            if (nowList.Count == 1)
                nowEmptyObj.GetComponent<BoxCollider2D>().enabled = true;
            else if (nowList.Count >= 2)
                nowList[nowList.Count - 2].GetComponent<BoxCollider2D>().enabled = true;

            if (exList.Count == 0)
            {
                if (Cash.preference.isRightHand == true)
                    pos = Cash.cardPos.deck[1];
                else
                    pos = Cash.cardPos.deck_Left[1];
            }
            if (exList.Count == 1)
            {
                if (Cash.preference.isRightHand == true)
                    pos = Cash.cardPos.deck[2];
                else
                    pos = Cash.cardPos.deck_Left[2];
            }
            if (exList.Count >= 2)
            {
                if (Cash.preference.isRightHand == true)
                    pos = Cash.cardPos.deck[3];
                else
                    pos = Cash.cardPos.deck_Left[3];
            }

            spriteRenderer.sortingOrder = 90;
            RuleOpenDeck.MoveOpenDeckCardsCompact(1);
            undoCard.GetComponent<Mover>().MoveToPosition(pos, Cash.speedToRetuYama);
            CardMoverBtwLists.ChangeCardGameList(undoCard, exList, nowList);

            //score
            ScoreCounter.AddScore(-ScorePoint.toYama);
            ScoreCounter.AddScore(-ScorePoint.fromDeck);
        }




        //opendDeck to deck へ戻る
        //BoxColliのtrue,false
        //opendDeckを広げる
        if (nowPlace == Cash.opendDeck && willPlace == Cash.deck)
        {
            pos = oya.transform.position;

            for (int i = 0; i < undoCards.Count; i++)
            {
                GameObject target = undoCards[undoCards.Count - 1 - i];
                target.GetComponent<SpriteRenderer>().sortingOrder = 90;
                target.GetComponent<Mover>().MoveToPosition(pos, Cash.speedDeckToOpenDeck);
                target.GetComponent<Rotater>().RotateToBack();
                target.GetComponent<BoxCollider2D>().enabled = false;
                CardMoverBtwLists.ChangeCardGameList(target, exList, nowList);
            }

            RuleOpenDeck.MoveOpenDeckCardsOpen();
        }



        //deck to opendDeck へ戻る
        //BoxColliのtrue,false
        if (nowPlace == Cash.deck && willPlace == Cash.opendDeck)
        {
            for (int i = 0; i < undoCards.Count; i++)
            {
                if (undoCards.Count <= 3)
                {
                    if (Cash.preference.isRightHand == true)
                        pos = Cash.cardPos.deck[1 + i];
                    else
                        pos = Cash.cardPos.deck_Left[1 + i];
                }
                if (undoCards.Count >= 4 && i <= undoCards.Count - 3)
                {
                    if (Cash.preference.isRightHand == true)
                        pos = Cash.cardPos.deck[1];
                    else
                        pos = Cash.cardPos.deck_Left[1];
                }
                if (undoCards.Count >= 4 && i == undoCards.Count - 2)
                {
                    if (Cash.preference.isRightHand == true)
                        pos = Cash.cardPos.deck[2];
                    else
                        pos = Cash.cardPos.deck_Left[2];
                }
                if (undoCards.Count >= 4 && i == undoCards.Count - 1)
                {
                    if (Cash.preference.isRightHand == true)
                        pos = Cash.cardPos.deck[3];
                    else
                        pos = Cash.cardPos.deck_Left[3];
                }

                
                GameObject target = undoCards[undoCards.Count - 1 - i];
                target.GetComponent<SpriteRenderer>().sortingOrder = 90;
                target.GetComponent<Mover>().MoveToPosition(pos, Cash.speedDeckToOpenDeck);
                target.GetComponent<Rotater>().RotateToFront();
                if (i == undoCards.Count - 1)
                    target.GetComponent<BoxCollider2D>().enabled = true;
                CardMoverBtwLists.ChangeCardGameList(target, exList, nowList);
            }
        }



        //retu to yama へ戻る
        //BoxColliのtrue,false
        //BoxColliのサイズ
        if (nowPlace == Cash.retu && willPlace == Cash.yama)
        {
            oya.GetComponent<BoxCollider2D>().enabled = false;
            pos = oya.transform.position;

            if (nowList.Count == 1)
                nowEmptyObj.GetComponent<BoxCollider2D>().enabled = true;
            else
                nowList[nowList.Count - 2].GetComponent<BC2DSizeChanger>().ChangeBxcSizeToFull();

            spriteRenderer.sortingOrder = 90;
            undoCard.GetComponent<Mover>().MoveToPosition(pos, Cash.speedToRetuYama);
            CardMoverBtwLists.ChangeCardGameList(undoCard, exList, nowList);

            //score
            ScoreCounter.AddScore(-ScorePoint.backFromYama);
        }


        //retu to opendDeck へ戻る
        //BoxColliのtrue,false
        //BoxColliのサイズ
        if (nowPlace == Cash.retu && willPlace == Cash.opendDeck)
        {
            if (nowList.Count >= 2)
                nowList[nowList.Count - 1].GetComponent<BC2DSizeChanger>().ChangeBxcSizeToFull();
            else if (nowList.Count == 1)
                nowEmptyObj.GetComponent<BoxCollider2D>().enabled = true;

            if (exList.Count == 0)
            {
                if (Cash.preference.isRightHand == true)
                    pos = Cash.cardPos.deck[1];
                else
                    pos = Cash.cardPos.deck_Left[1];
            }
            if (exList.Count == 1)
            {
                if (Cash.preference.isRightHand == true)
                    pos = Cash.cardPos.deck[2];
                else
                    pos = Cash.cardPos.deck_Left[2];
            }
            if (exList.Count >= 2)
            {
                if (Cash.preference.isRightHand == true)
                    pos = Cash.cardPos.deck[3];
                else
                    pos = Cash.cardPos.deck_Left[3];
            }


            RuleOpenDeck.MoveOpenDeckCardsCompact(1);
            spriteRenderer.sortingOrder = 100;
            undoCard.GetComponent<BC2DSizeChanger>().ChangeBxcSizeToFull();
            undoCard.GetComponent<Mover>().MoveToPosition(pos, Cash.speedToRetuYama);
            CardMoverBtwLists.ChangeCardGameList(undoCard, exList, nowList);

            //score
            ScoreCounter.AddScore(-ScorePoint.fromDeck);
        }


        //４枚ぐらいのカードが戻る時、一番下以外のカード以外すべて裏面になって戻っていた

        //retu to retu へ戻る
        //BoxColliのtrue,false
        //カードを裏へ
        //BoxColliのサイズ
        if (nowPlace == Cash.retu && willPlace == Cash.retu)
        {
            if (exList.Count == 0){
                oya.GetComponent<BoxCollider2D>().enabled = false;
                pos = oya.transform.position;
            }
            if (exList.Count == 1 && retuReturned == false){
                oya.GetComponent<BC2DSizeChanger>().ChangeBxcSizeToVisible();
                pos = new Vector3(oya.transform.position.x, oya.transform.position.y - Cash.cardPos.spaceBtwRetu_Front, 0);
            }
            if (exList.Count == 1 && retuReturned == true){
                oya.GetComponent<Rotater>().RotateToBack();
                pos = new Vector3(oya.transform.position.x, oya.transform.position.y - Cash.cardPos.spaceBtwRetu_Back, 0);
            }
            if (exList.Count >= 2){
                if (exList[exList.Count - 2].GetComponent<CardInfo>().isFront == false && retuReturned == true){
                    oya.GetComponent<BC2DSizeChanger>().ChangeBxcSizeToFull();
                    oya.GetComponent<Rotater>().RotateToBack();
                    oya.GetComponent<BoxCollider2D>().enabled = false;
                    pos = new Vector3(oya.transform.position.x, oya.transform.position.y - Cash.cardPos.spaceBtwRetu_Back, 0);
                    //score
                    ScoreCounter.AddScore(-ScorePoint.flipedRetu);
                }
                else if (exList[exList.Count - 2].GetComponent<CardInfo>().isFront == false && retuReturned == false){
                    oya.GetComponent<BC2DSizeChanger>().ChangeBxcSizeToVisible();
                    pos = new Vector3(oya.transform.position.x, oya.transform.position.y - Cash.cardPos.spaceBtwRetu_Front, 0);
                }
                else if(exList[exList.Count - 2].GetComponent<CardInfo>().isFront == true){
                    oya.GetComponent<BC2DSizeChanger>().ChangeBxcSizeToVisible();
                    pos = new Vector3(oya.transform.position.x, oya.transform.position.y - Cash.cardPos.spaceBtwRetu_Front, 0);
                }
            }


            for (int i = 0; i < undoCards.Count; i++){
                GameObject target = undoCards[i];
                target.GetComponent<SpriteRenderer>().sortingOrder = 90 + i;
                target.GetComponent<Mover>().MoveToPosition(pos, Cash.speedToRetuYama);
                CardMoverBtwLists.ChangeCardGameList(target, exList, nowList);
                pos = new Vector3(pos.x, pos.y - Cash.cardPos.spaceBtwRetu_Front, 0);
            }

            if (nowList.Count == 0)
                nowEmptyObj.GetComponent<BoxCollider2D>().enabled = true;
        }



    }

}
