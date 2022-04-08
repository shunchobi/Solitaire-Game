using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsShuffler : MonoBehaviour
{
    
    static List<GameObject> shaffleCards;
    static List<List<int>> shaffleCardsInfo; //shaffleCardsInfo[]の[0]はListIntNum、[1]はNumInList



    public void _ShaffleBackCards(){
        bool existTarget = BackCardsChecker.CheckExistBackCards();
        if (existTarget == false) return;

        StartCoroutine("ShaffleBackCards");
    }



    IEnumerator ShaffleBackCards(){

        GameObject.Find("Shuffle").GetComponent<Button>().enabled = false;
        RuleDeck.FlipOpenCardsBack();
        UpFrontCardsLayer(true);

        yield return new WaitForSeconds(Cash.speedDeckToOpenDeck);

        CardsUntouchabler.UntouchableAllCards();
        shaffleCards = new List<GameObject>();
        shaffleCardsInfo = new List<List<int>>();
        List<int> info;

        //裏側のカードをshaffleCardsへ追加
        //Listへ追加する時に、そのカードのListIntNumと、NumInListを取得 = List<List<Int>(2,1)>
        //そのカードをNowListからRemoveする
        for (int i = 0; i < GameListHolder.gameLists.Count; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){
                GameObject target = GameListHolder.gameLists[i][n];
                CardInfo cardInfo = target.GetComponent<CardInfo>();
                bool isFront = cardInfo.isFront;

                if(isFront == false){
                    shaffleCards.Add(target);
                    info = new List<int>{i,n};
                    shaffleCardsInfo.Add(info);
                    info = null;
                }
            }
        }


        for (int i = 0; i < shaffleCards.Count; i++){
            GameObject target = shaffleCards[i];
            int listPlace = shaffleCardsInfo[i][0];
            GameListHolder.gameLists[listPlace].Remove(target);
        }


        //shaffleCardsからランダムにカードを取り出し、shaffleCardsInfoの順番どうりにカードを追加
        int shaffleCardsListAmount = shaffleCards.Count;
        for (int i = 0; i < shaffleCardsListAmount; i++) {
            GameObject targetCard = shaffleCards[Random.Range(0, shaffleCards.Count)];
            CardMoverBtwLists.AddCardToGameList(targetCard, GameListHolder.gameLists[shaffleCardsInfo[i][0]], shaffleCardsInfo[i][1]);
            Vector3 willPos = PosFromIndexReturner.GetPosFromIndex(shaffleCardsInfo[i][0], shaffleCardsInfo[i][1]);
            targetCard.GetComponent<Mover>().MoveToPosition(willPos, Cash.speedToRetuYama);
            shaffleCards.Remove(targetCard);
            yield return new WaitForSeconds(0.06f);
        }
                                    
        yield return new WaitForSeconds(Cash.speedToRetuYama - 0.06f);

        CardsUntouchabler.TouchableAllCards();
        shaffleCards = null;
        shaffleCardsInfo = null;
        GameObject.Find("Shuffle").GetComponent<Button>().enabled = true;
        UpFrontCardsLayer(false);

        UndoListHolder.undoCardsLists.Clear();
        UndoListHolder.undoListPlace.Clear();
        UndoListHolder.retuReturned.Clear();
    }





    void UpFrontCardsLayer(bool isUp){
        for (int i = 0; i < 7; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){
                GameObject target = GameListHolder.gameLists[i][n];
                CardInfo cardInfo = target.GetComponent<CardInfo>();
                bool isFront = cardInfo.isFront;
                if (isFront == true)
                {
                    if(isUp == true)
                        target.GetComponent<SpriteRenderer>().sortingOrder = 700 + cardInfo.intInList;
                    else
                        target.GetComponent<SpriteRenderer>().sortingOrder = cardInfo.intInList;
                }

            }
        }
        
    }




}
