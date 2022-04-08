using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameListArrenger : MonoBehaviour {


    static List<GameObject> cardList = new List<GameObject>();



    public static void AddCardsToArrenge(List<GameObject> cards){
        for (int i = 0; i < cards.Count; i++)
            cardList.Add(cards[i]);
    }



    //このメソッドに合計52枚のカードを含むListをすべて渡すと、
    //gameLists,replayListsにカードをランダムにAddしてくれる。
    public static void ArrengeCardsToLists(){


        if (cardList.Count == 52)//ランダムにカードを選択し、各リストへカードをAddする。
        {

            List<GameObject> listForNewGame;
            List<GameObject> listForReplay;

            for (int i = 1; i <= 13; i++)
            {
                listForNewGame = new List<GameObject>();
                listForReplay = new List<GameObject>();

                if (i == 8)
                {
                    for (int n = 0; n < 24; n++)
                    {
                        GameObject targetCard = cardList[Random.Range(0, cardList.Count)];
                        listForNewGame.Add(targetCard);
                        listForReplay.Add(targetCard);

                        CardInfo cardInfo = targetCard.GetComponent<CardInfo>();
                        cardInfo.place = Cash.deck;
                        cardInfo.placeListInt = i - 1;
                        cardInfo.intInList = n;

                        cardList.Remove(targetCard);
                    }
                }
                else if(i <= 7){
                    for (int n = 0; n < i; n++)
                    {
                        GameObject targetCard = cardList[Random.Range(0, cardList.Count)];
                        listForNewGame.Add(targetCard);
                        listForReplay.Add(targetCard);

                        CardInfo cardInfo = targetCard.GetComponent<CardInfo>();
                        cardInfo.place = Cash.retu;
                        cardInfo.placeListInt = i - 1;
                        cardInfo.intInList = n;

                        cardList.Remove(targetCard);
                    }
                }

                GameListHolder.gameLists.Add(listForNewGame);
                ReplayListHolder.replayLists.Add(listForReplay);
                listForNewGame = null;
                listForReplay = null;

            }
            cardList.Clear();
        }

        else return;
    }

}
