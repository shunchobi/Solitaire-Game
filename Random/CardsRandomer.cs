using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsRandomer : MonoBehaviour
{

    List<GameObject> list;


    CardsDealer cardsDealer;


    void Start()
    {
        cardsDealer = GameObject.Find("Start").GetComponent<CardsDealer>();
    }



    public void DealCardsRandomly()
    {
        list = new List<GameObject>();

        for(int i = 0; i < GameListHolder.gameLists.Count; i++)
        {
            for(int n = 0; n < GameListHolder.gameLists[i].Count; n++)
            {
                GameObject card = GameListHolder.gameLists[i][n];
                list.Add(card);
            }
        }
        GameListHolder.gameLists.Clear();
        ReplayListHolder.replayLists.Clear();


        GameListArrenger.AddCardsToArrenge(list);
        GameListArrenger.ArrengeCardsToLists();
        cardsDealer.DealCards(false);

        list = null;
    }






}
