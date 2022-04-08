using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDealer : MonoBehaviour {
    

    public void DealCards(bool isFromReplay){
        if (GameListHolder.gameLists.Count == 0) return;

        //全てのカードをソリティアのポジションへ配る。
        for (int i = 0; i < GameListHolder.gameLists.Count; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){
                
                GameObject card = GameListHolder.gameLists[i][n];

                CardInfo cardInfo = card.GetComponent<CardInfo>();
                SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>();
                cardInfo.intInList = n;
                cardInfo.placeListInt = i;
                cardInfo.isFront = false;
                spriteRenderer.sortingOrder = n;
                spriteRenderer.sprite = cardInfo.backSprite;
                card.GetComponent<BC2DSizeChanger>().ChangeBxcSizeToFull();
                card.GetComponent<BoxCollider2D>().enabled = false;

                if(i <= 6)
                    cardInfo.place = Cash.retu;
                if(i == 7)
                    cardInfo.place = Cash.deck;
                


                float posX;
                float posY;
                if (i < 7)
                {
                    posX = Cash.cardPos.retu[i].x;
                    posY = Cash.cardPos.retu[i].y - (n * Cash.cardPos.spaceBtwRetu_Back);
                }
                else
                {
                    if (Cash.preference.isRightHand == true)
                    {
                        posX = Cash.cardPos.deck[0].x;
                        posY = Cash.cardPos.deck[0].y;
                    }
                    else
                    {
                        posX = Cash.cardPos.deck_Left[0].x;
                        posY = Cash.cardPos.deck_Left[0].y;
                    }
                }
                card.GetComponent<Mover>().MoveToPosition(new Vector3(posX, posY, 0f), Cash.speedToRandom);
            }
        }

        //全てのカードを配り終わったら、retuの最後のカードをめくる。
        for (int i = 0; i < 7; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){ 
                if(n == GameListHolder.gameLists[i].Count - 1 && i != GameListHolder.gameLists.Count - 1){
                    GameObject card = GameListHolder.gameLists[i][n];
                    card.GetComponent<Rotater>().RotateToFront();
                    card.GetComponent<BoxCollider2D>().enabled = true;
                    card.GetComponent<BC2DSizeChanger>().ChangeBxcSizeToFull();
                    GameObject.Find("row8").GetComponent<BoxCollider2D>().enabled = true;
                    GameObject.Find("row9").GetComponent<BoxCollider2D>().enabled = true;
                    GameObject.Find("row10").GetComponent<BoxCollider2D>().enabled = true;
                    GameObject.Find("row11").GetComponent<BoxCollider2D>().enabled = true;
                    GameObject.Find("row12").GetComponent<BoxCollider2D>().enabled = true;

                }
            }
        }

        UiManager.HomeUi(false);
        if (isFromReplay == false)
            UiManager.GetPlayUi(true);

        Cash.isStaredGame = true;
    }
}
