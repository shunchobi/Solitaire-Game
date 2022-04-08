using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleDeck : MonoBehaviour {



    BoxCollider2D bc2d;



    private void Start()
    {
        bc2d = this.gameObject.GetComponent<BoxCollider2D>();
    }


    public void OnMouseUpAsButton(){
        StartCoroutine("TouchDeck");
    }



    IEnumerator TouchDeck()
    {
        if (UiManager.isMovedCard == false)
        {
            UiManager.GetPlayUi(false);
            UiManager.PlayUi(true);
            UiManager.isMovedCard = true;
        }


        //deckにもopenDeckにもカードがない場合はreturn
        if (GameListHolder.gameLists[7].Count == 0
            && GameListHolder.gameLists[8].Count == 0)
            yield break;
        

        //openDeckカードをdeckへ戻す
        if (GameListHolder.gameLists[7].Count == 0
            && GameListHolder.gameLists[8].Count > 0)
        {
            bc2d.enabled = false;
            FlipOpenCardsBack();
            yield return new WaitForSeconds(Cash.speedDeckToOpenDeck);
            bc2d.enabled = true;
            yield break;
        }


        //deckカードをめくる
        int openCardsAmount = 0;
        if (Cash.preference.isOneCardOpen) openCardsAmount = 1;
        else if (!Cash.preference.isOneCardOpen)
        {
            if(GameListHolder.gameLists[7].Count >= 3)
              openCardsAmount = 3;
            if(GameListHolder.gameLists[7].Count <= 2)
                openCardsAmount = GameListHolder.gameLists[7].Count;
        }

        bc2d.enabled = false;
        //opendeckにカードがあれば詰める
        RuleOpenDeck.MoveOpenDeckCardsCompact(openCardsAmount);
        FlipDeckCards(openCardsAmount);
        yield return new WaitForSeconds(Cash.speedDeckToOpenDeck);
        bc2d.enabled = true;
    }




    static void FlipDeckCards(int flipAmount)
    {

        //undo用
        List<GameObject> forUndo = new List<GameObject>();


        for (int i = 0; i < flipAmount; i++)
        {
            GameObject deckCard = GameListHolder.gameLists[7][GameListHolder.gameLists[7].Count - 1];

            //undo用
            forUndo.Add(deckCard);

            //カードの座標を移動
            List<Vector3> pos;
            if (Cash.preference.isRightHand == true)
                pos = Cash.cardPos.deck;
            else
                pos = Cash.cardPos.deck_Left;

            if(flipAmount == 1 && GameListHolder.gameLists[8].Count == 0)
                deckCard.GetComponent<Mover>().MoveToPosition(pos[1],Cash.speedDeckToOpenDeck);
            if (flipAmount == 1 && GameListHolder.gameLists[8].Count == 1)
                deckCard.GetComponent<Mover>().MoveToPosition(pos[2], Cash.speedDeckToOpenDeck);
            if (flipAmount == 1 && GameListHolder.gameLists[8].Count >= 2)
                deckCard.GetComponent<Mover>().MoveToPosition(pos[3], Cash.speedDeckToOpenDeck);
            if (flipAmount >= 2)
                deckCard.GetComponent<Mover>().MoveToPosition(pos[1 + i], Cash.speedDeckToOpenDeck);

            //カードを表側へ回転
            deckCard.GetComponent<Rotater>().RotateToFront();
            //めくった最後のカードのBoxCollider2Dをtrueにする
            if (i == flipAmount - 1)
                deckCard.GetComponent<BoxCollider2D>().enabled = true;

            //カーどのリスト移動
            CardMoverBtwLists.ChangeCardGameList(deckCard,GameListHolder.gameLists[8],GameListHolder.gameLists[7]);
        }

        //undo用
        UndoListHolder.AddUndoCardsLists(forUndo);
        UndoListHolder.AddUndoListPlace(7);
        UndoListHolder.AddRetuReturned(false);
    }



    public static void FlipOpenCardsBack()
    {
        if (GameListHolder.gameLists[8].Count == 0) return;

        GameObject lastOpenCard = GameListHolder.gameLists[8][GameListHolder.gameLists[8].Count - 1];
        lastOpenCard.GetComponent<BoxCollider2D>().enabled = false;

        //undo用
        List<GameObject> forUndo = new List<GameObject>();

        for (int i = 0; i < GameListHolder.gameLists[8].Count;)
        {
            GameObject opendCard = GameListHolder.gameLists[8][GameListHolder.gameLists[8].Count - 1];
            if (Cash.preference.isRightHand == true)
                opendCard.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck[0], Cash.speedDeckToOpenDeck);
            else
                opendCard.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck_Left[0], Cash.speedDeckToOpenDeck);

            opendCard.GetComponent<Rotater>().RotateToBack();

            //undo用
            forUndo.Add(opendCard);

            CardMoverBtwLists.ChangeCardGameList(opendCard, GameListHolder.gameLists[7], GameListHolder.gameLists[8]);
        }

        //undo用
        UndoListHolder.AddUndoCardsLists(forUndo);
        UndoListHolder.AddUndoListPlace(8);
        UndoListHolder.AddRetuReturned(false);
    }



}
