using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCompleter : MonoBehaviour
{
    


    public void CompleteCards()
    {
        StartCoroutine("_CompleteCards");
    }


    IEnumerator _CompleteCards()
    {

        CardsUntouchabler.UntouchableAllCards();

        int remaningCards = 0;
        for (int i = 0; i <= 6; i++)
        {
            int listAmount = GameListHolder.gameLists[i].Count;
            remaningCards = remaningCards + listAmount;
        }

        for (int i = 0; i <= remaningCards; i++)
        {
            GameObject target_retu;
            GameObject target_yama;

            for (int retu = 0; retu <= 6; retu++)
            {
                if (GameListHolder.gameLists[retu].Count == 0) continue;
                target_retu = GameListHolder.gameLists[retu][GameListHolder.gameLists[retu].Count - 1];

                for (int yama = 9; yama <= 12; yama++)
                {

                    if (GameListHolder.gameLists[yama].Count == 0)
                        target_yama = EmptyObjectReturner.GetEmptyObj(yama);
                    else
                        target_yama = GameListHolder.gameLists[yama][GameListHolder.gameLists[yama].Count - 1];

                    bool match = RuleYama.CheckAcceptability(target_retu, target_yama);

                    if (match == true)
                    {
                        GameObject yamaEmpty = EmptyObjectReturner.GetEmptyObj(target_yama.GetComponent<CardInfo>().placeListInt);
                        target_retu.GetComponent<Mover>().MoveToPosition(yamaEmpty.transform.position, 0.1f);
                        CardMoverBtwLists.ChangeCardGameList(target_retu, OwnListReturner.GetList(target_yama), OwnListReturner.GetList(target_retu));
                        yield return new WaitForSeconds(0.2f);
                        break;
                    }
                }
            }
        }

        UiController.autoCompleteObjDirec.UnableAutoCompObj();

        UiManager.isMovedCard = false;
        NewGameCardsArrenger.isCompleteSecen = true;
        UiManager.PlayUi(false);
        UiManager.ClearUi(true);
        CardsAction.MoveAllCards(new Vector3(2000, 2000, 0));

        NewGameCardsArrenger.ArrengeCardsList();
        UndoListHolder.undoCardsLists.Clear();
        UndoListHolder.undoListPlace.Clear();
        UndoListHolder.retuReturned.Clear();

    }
}
