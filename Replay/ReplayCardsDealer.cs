using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayCardsDealer : MonoBehaviour
{ 
    CardsDealer cardsDealer;


    private void Start()
    {
        cardsDealer = GameObject.Find("Start").GetComponent<CardsDealer>();
    }



    public void DealCardsForReplay(){

        GameListHolder.gameLists.Clear();

        List<GameObject> list;

        for (int i = 0; i < ReplayListHolder.replayLists.Count; i++){
            list = new List<GameObject>();

            for (int n = 0; n < ReplayListHolder.replayLists[i].Count; n++){
                GameObject card = ReplayListHolder.replayLists[i][n];
                list.Add(card);
            }
            GameListHolder.gameLists.Add(list);
        }

        UndoListHolder.undoCardsLists.Clear();
        UndoListHolder.undoListPlace.Clear();
        UndoListHolder.retuReturned.Clear();


        cardsDealer.DealCards(true);
	}





}
