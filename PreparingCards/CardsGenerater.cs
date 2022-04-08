using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGenerater : MonoBehaviour {




    GameObject cardPrefab;
    Vector3 firstPos;
    Sprite backSprite;
    List<GameObject> cards;


	public void Init () {
        cardPrefab = Resources.Load<GameObject>("Prefab/CardPrefab");
        firstPos = new Vector3(3000, 0, 0);
        backSprite = Resources.Load<Sprite>("Image/DotCards/back");

        Generate();
	}



    void Generate(){

        cards = new List<GameObject>();

        for (int suit = 1; suit <= 4; suit++)
        {
            for (int num = 1; num<= 13; num++)
            {
                GameObject card = Instantiate(cardPrefab) as GameObject;
                card.transform.position = firstPos;
                CardInfo cardInfo = card.GetComponent<CardInfo>();
                cardInfo.suit = suit;
                cardInfo.cardNum = num;
                cardInfo.backSprite = backSprite;
                cardInfo.frontSprite = Resources.Load<Sprite>("Image/DotCards/"+num.ToString()+"_"+suit.ToString());
                card.transform.localScale = PlacePos.scaleCard;
                if (suit == 2 || suit == 4)cardInfo.suitColor = Cash.red;
                else cardInfo.suitColor = Cash.black;

                card.GetComponent<SpriteRenderer>().sprite = backSprite;

                cards.Add(card);
            }
        }

        GameListArrenger.AddCardsToArrenge(cards);
        GameListArrenger.ArrengeCardsToLists();
        cards = null;
    }



	
	
}
