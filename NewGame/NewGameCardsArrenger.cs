using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameCardsArrenger : MonoBehaviour
{

    CardsDealer cardsDealer;

    float timeToDealCards = 0.3f;

    public static bool isNewGameByClear = false;

    public static bool isCompleteSecen = false;


    void Start(){
        cardsDealer = GameObject.Find("Start").GetComponent<CardsDealer>();
    }




    public void StarNewGame()
    {
        StartCoroutine("_StartNewGame");
    }




    IEnumerator _StartNewGame()
    {
        UiManager.isMovedCard = false;
        UiManager.PlayUi(false);

        if(isCompleteSecen == true)
        {
            UiManager.ClearUi(false);
            isCompleteSecen = false;
        }

        CardsAction.MoveAllCards(Vector3.zero); //カードを画面外へ出す
        ArrengeCardsList(); //Listを調整
        UndoListHolder.undoCardsLists.Clear();
        UndoListHolder.undoListPlace.Clear();
        UndoListHolder.retuReturned.Clear();
        yield return new WaitForSeconds(timeToDealCards); //移動しきるまで待つ
        cardsDealer.DealCards(false);
    }




    　public static void ArrengeCardsList(){
        for (int i = 0; i < 13; i++){
            List<GameObject> targetList = GameListHolder.gameLists[i];
            GameListArrenger.AddCardsToArrenge(targetList);
        }
        ReplayListHolder.replayLists.Clear();
        GameListHolder.gameLists.Clear();

        GameListArrenger.ArrengeCardsToLists();
    }



}
