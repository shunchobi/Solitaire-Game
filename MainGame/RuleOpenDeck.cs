using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleOpenDeck : MonoBehaviour
{

    /// <summary>
    /// opendeckにカードがあれば詰める
    /// </summary>
    public static void MoveOpenDeckCardsCompact(int flipAmount)
    {
        //最後のopendeckカードをさわれなくする
        if(GameListHolder.gameLists[8].Count > 0){
            GameObject lastOpenCard = GameListHolder.gameLists[8][GameListHolder.gameLists[8].Count - 1];
            lastOpenCard.GetComponent<BoxCollider2D>().enabled = false;

        }


        if (flipAmount >= 2)
        {
            for (int i = 0; i < GameListHolder.gameLists[8].Count; i++)
            {
                GameObject openDeck = GameListHolder.gameLists[8][i];
                if (Cash.preference.isRightHand == true)
                    openDeck.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck[1], Cash.speedDeckToOpenDeck);//deck[1] = row8_1
                else
                    openDeck.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck_Left[1], Cash.speedDeckToOpenDeck);//deck[1] = row8_1

            }
        }

        else if (flipAmount == 1 && GameListHolder.gameLists[8].Count >= 3)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject openDeck = GameListHolder.gameLists[8][GameListHolder.gameLists[8].Count - 2 + i];
                if (Cash.preference.isRightHand == true)
                    openDeck.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck[i + 1], Cash.speedDeckToOpenDeck);
                else
                    openDeck.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck_Left[i + 1], Cash.speedDeckToOpenDeck);

            }
        }
    }



    public static void MoveOpenDeckCardsOpen(){

        if(GameListHolder.gameLists[8].Count >= 3){
            for (int i = 0; i < 3; i++){
                GameObject target = GameListHolder.gameLists[8][GameListHolder.gameLists[8].Count - 1 - i];
                if(Cash.preference.isRightHand == true)
                    target.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck[3 - i], Cash.speedDeckToOpenDeck);
                else
                    target.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck_Left[3 - i], Cash.speedDeckToOpenDeck);
                if (i == 0)
                    target.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else if(GameListHolder.gameLists[8].Count <= 2 && GameListHolder.gameLists[8].Count > 0){
            for (int i = 0; i < GameListHolder.gameLists[8].Count; i++){
                GameObject target = GameListHolder.gameLists[8][GameListHolder.gameLists[8].Count - GameListHolder.gameLists[8].Count + i];
                if (Cash.preference.isRightHand == true)
                    target.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck[1 + i], Cash.speedDeckToOpenDeck);
                else
                    target.GetComponent<Mover>().MoveToPosition(Cash.cardPos.deck_Left[1 + i], Cash.speedDeckToOpenDeck);
                if(i == GameListHolder.gameLists[8].Count - 1)
                    target.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

    }



}
