using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBecomer : MonoBehaviour
{









    /// <summary>
    /// childをoyaの子供にする
    /// </summary>
    public static void BecameChild(GameObject child, GameObject oya)
    {

        List<GameObject> oyaList = OwnListReturner.GetList(oya);
        List<GameObject> childList = OwnListReturner.GetList(child);

        string oyaPlace = oya.GetComponent<CardInfo>().place;
        string childPlace = child.GetComponent<CardInfo>().place;

        Vector3 oyaPos = oya.transform.position;
        Mover childMover = child.GetComponent<Mover>();

        int oyaListPlace = oya.GetComponent<CardInfo>().placeListInt;
        GameObject oyaEmpty = null;
        if (oyaListPlace <= 6)
            oyaEmpty = EmptyObjectReturner.GetEmptyObj(oyaListPlace + 1);
        else if (oyaListPlace >= 9)
            oyaEmpty = EmptyObjectReturner.GetEmptyObj(oyaListPlace);

        int childOwnListLastNum = childList.Count - 1;
        int childIndexNum = child.GetComponent<CardInfo>().intInList;

        List<GameObject> forUndo;


        //undo用
        if (childPlace == Cash.opendDeck)
        {
            UndoListHolder.AddRetuReturned(false);
            //score
            ScoreCounter.AddScore(ScorePoint.fromDeck);
        }
        //undo用
        if (childPlace == Cash.yama)
            UndoListHolder.AddRetuReturned(false);


        //子供がその列に一枚しかなくて移動する場合、空オブジェクトのBoxColliderをfalse,trueにする
        if (childIndexNum == 0 && childPlace != Cash.opendDeck)
        {
            GameObject empty = null;
            if (childPlace == Cash.retu)
                empty = EmptyObjectReturner.GetEmptyObj(child.GetComponent<CardInfo>().placeListInt + 1);
            if (childPlace == Cash.yama)
                empty = EmptyObjectReturner.GetEmptyObj(child.GetComponent<CardInfo>().placeListInt);
            empty.GetComponent<BoxCollider2D>().enabled = true;
        }

        //childがyamaの場合で、2以上のカードだったらchildの下にいるカードをtrueにする。
        if (childPlace == Cash.yama && childIndexNum >= 2)
        {
            childList[childIndexNum - 1].GetComponent<BoxCollider2D>().enabled = true;
        }


        if (childPlace == Cash.retu && childList.Count >= 2 && childIndexNum > 0)
        {
            //childがretuで、上のカードが表向きだったら、そのカードのBoxColliを元の大きさに戻す
            if (childList[childIndexNum - 1].GetComponent<CardInfo>().isFront == true)
            {
                childList[childIndexNum - 1].GetComponent<BC2DSizeChanger>().ChangeBxcSizeToFull();
                //undo用
                UndoListHolder.AddRetuReturned(false);
            }
            //childがretuで、上にあるカードが裏向きだったらめくる
            if (childList[childIndexNum - 1].GetComponent<CardInfo>().isFront == false)
            {
                //childList[childIndexNum - 1].GetComponent<Rotater>().RotateToFront();
                childList[childIndexNum - 1].GetComponent<BoxCollider2D>().enabled = true;
                //undo用
                UndoListHolder.AddRetuReturned(true);
                //score
                ScoreCounter.AddScore(ScorePoint.flipedRetu);
            }

        }

        //undo用
        if (childPlace == Cash.retu && childIndexNum == 0)
        {
            UndoListHolder.AddRetuReturned(false);
        }





        if (oyaPlace == Cash.yama || oyaPlace == Cash.yama_empty)
        {
            //undo用
            forUndo = new List<GameObject>() { child };
            UndoListHolder.AddUndoCardsLists(forUndo);
            UndoListHolder.AddUndoListPlace(child.GetComponent<CardInfo>().placeListInt);

            childMover.MoveToPosition(oyaEmpty.transform.position, Cash.speedToRetuYama);
            CardMoverBtwLists.ChangeCardGameList(child, oyaList, childList);
            oya.GetComponent<BoxCollider2D>().enabled = false;

            //score
            if (childPlace != Cash.yama)
                ScoreCounter.AddScore(ScorePoint.toYama);
        }


        else if (oyaPlace == Cash.retu || oyaPlace == Cash.retu_empty)
        {
            int childListCount = childList.Count;
            if (oyaPlace == Cash.retu_empty)
                oya.GetComponent<BoxCollider2D>().enabled = false;
            if (oyaPlace == Cash.retu)
            {
                oya.GetComponent<BC2DSizeChanger>().ChangeBxcSizeToVisible();
                if (childList.Count >= 2 && childPlace == Cash.retu && childIndexNum > 0)
                {
                    if (childList[childIndexNum - 1].GetComponent<CardInfo>().isFront == true)
                        childList[childIndexNum - 1].GetComponent<BC2DSizeChanger>().ChangeBxcSizeToFull();
                }
            }

            //sore
            if (childPlace == Cash.yama)
                ScoreCounter.AddScore(ScorePoint.backFromYama);

            //undo用
            forUndo = new List<GameObject>();
            UndoListHolder.AddUndoListPlace(child.GetComponent<CardInfo>().placeListInt);

            Vector3 oyaPosi = Vector3.zero;
            if (oyaPlace == Cash.retu_empty)
                oyaPosi = oyaEmpty.transform.position;
            else
                oyaPosi = CardPosReturner.GetCardPosFromObj(oya);


            for (int i = 0; i < childListCount - childIndexNum; i++)
            { //自身を含む子供の数を合わせた数分回す。自身のみなら一回になる。
                GameObject target = childList[childIndexNum];
                //undo用
                forUndo.Add(target);

                float yDelta = 0;
                if (oyaPlace == Cash.retu)
                    yDelta = Cash.cardPos.spaceBtwRetu_Front * (i + 1);
                if (oyaPlace == Cash.retu_empty)
                    yDelta = Cash.cardPos.spaceBtwRetu_Front * i;

                Vector3 posToMove = new Vector3(oyaPosi.x, oyaPosi.y - yDelta, oyaPosi.z);
                target.GetComponent<Mover>().MoveToPosition(posToMove, Cash.speedToRetuYama);

                CardMoverBtwLists.ChangeCardGameList(target, oyaList, childList);
            }
            //undo用
            UndoListHolder.AddUndoCardsLists(forUndo);
        }

        //childがopenDeckだったら、openDeckのカードたちを広げる
        if (childPlace == Cash.opendDeck && childList.Count > 0)
            RuleOpenDeck.MoveOpenDeckCardsOpen();

        bool ableToComp = AutoCompleteChecker.CheckAbleToAutoComplete();
        if (ableToComp == true)
            UiController.autoCompleteObjDirec.AbleAutoCompObj();





        bool isCompleste = CompleateChecker.GetIsComplete();
        if (isCompleste == true)
        {
            UiManager.isMovedCard = false;
            UiManager.PlayUi(false);
            UiManager.ClearUi(true);
            NewGameCardsArrenger.isCompleteSecen = true;

            CardsAction.MoveAllCards(new Vector3(2000, 2000, 0));
            UiController.autoCompleteObjDirec.UnableAutoCompObj();


            NewGameCardsArrenger.ArrengeCardsList();
            UndoListHolder.undoCardsLists.Clear();
            UndoListHolder.undoListPlace.Clear();
            UndoListHolder.retuReturned.Clear();

        }



    }







}
