using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMoverBtwLists : MonoBehaviour {



    //GameListHolder.gameLists内のList間のカード移動（Add）をこのメソッドを利用して行う。
    public static void ChangeCardGameList(GameObject card, List<GameObject> listToAdd, List<GameObject> listToRemove)
    {
        listToAdd.Add(card);
        listToRemove.Remove(card);

        string _place = GetPlaceName(card);
        int _placeListNum = GetPlaceListNum(card);
        int _intInList = GetIntInList(card);

        CardInfo cardInfo = card.GetComponent<CardInfo>();
        cardInfo.place = _place;
        cardInfo.placeListInt = _placeListNum;
        cardInfo.intInList = _intInList;
        card.GetComponent<SpriteRenderer>().sortingOrder = _intInList;
    }





    //GameListHolder.gameLists内のList間のカード移動（Add）をこのメソッドを利用して行う。
    public static void AddCardToGameList(GameObject card, List<GameObject> listToAdd, int indexNum)
    {
        listToAdd.Insert(indexNum, card);

        string _place = GetPlaceName(card);
        int _placeListNum = GetPlaceListNum(card);
        int _intInList = GetIntInList(card);

        CardInfo cardInfo = card.GetComponent<CardInfo>();
        cardInfo.place = _place;
        cardInfo.placeListInt = _placeListNum;
        cardInfo.intInList = _intInList;
        card.GetComponent<SpriteRenderer>().sortingOrder = _intInList;
    }





    //GameListHolder.gameLists内のList間のカード移動（Remove）をこのメソッドを利用して行う。
    public static void RemoveCardToGameList(GameObject card, List<GameObject> listToRemove)
    {
        listToRemove.Remove(card);

        string _place = GetPlaceName(card);
        int _placeListNum = GetPlaceListNum(card);
        int _intInList = GetIntInList(card);

        CardInfo cardInfo = card.GetComponent<CardInfo>();
        cardInfo.place = _place;
        cardInfo.placeListInt = _placeListNum;
        cardInfo.intInList = _intInList;
    }









    //渡したカードが所属するList内で何番目にあるかを返してくれる。
    static  int GetIntInList(GameObject card){
        
        int listNum = -1;

        for (int i = 0; i < GameListHolder.gameLists.Count; i++)
        {
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++)
            {
                GameObject _card = GameListHolder.gameLists[i][n];
                if (_card == card)
                {
                    listNum = n;
                    break;
                }
            }
        }
        return listNum;
    }



    //渡したカードが所属するListがgameLists内の何番目にあるのかを返してくれる。
    static  int GetPlaceListNum(GameObject card){
        
        int listNum = -1;

        for (int i = 0; i < GameListHolder.gameLists.Count; i++)
        {
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++)
            {
                GameObject _card = GameListHolder.gameLists[i][n];
                if (_card == card)
                {
                    listNum = i;
                    break;
                }
            }
        }
        return listNum;
    }



    //渡したカードがいる場所の名前をStringで返してくれる
    static  string GetPlaceName(GameObject card){
        
        string place = null;
        int listNum = -1;

        for (int i = 0; i < GameListHolder.gameLists.Count; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){
                GameObject _card = GameListHolder.gameLists[i][n];
                if(_card == card){
                    listNum = i;
                    break;
                }
            }
        }

        if (listNum <= 6)
            place = Cash.retu;
        if (listNum == 7)
            place = Cash.deck;
        if (listNum == 8)
            place = Cash.opendDeck;
        if (listNum >= 9)
            place = Cash.yama;
        
        return place;
    }
}
