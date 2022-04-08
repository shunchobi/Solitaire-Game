using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoDirecter : MonoBehaviour
{

    Button undoB;


    private void Start()
    {
        undoB = GameObject.Find("Undo").GetComponent<Button>();
    }

    public void PlaceUndoCards(){
        StartCoroutine("_PlaceUndoCards");
    }




    IEnumerator _PlaceUndoCards()
    {
        if (UndoListHolder.undoListPlace.Count == 0)
            yield break;

        undoB.enabled = false;
        //CardsUntouchabler.UntouchableAllCards();

        //undo対象のカードと戻り先のList番号を取得
        List<GameObject> undoCardsList = UndoListHolder.undoCardsLists[UndoListHolder.undoCardsLists.Count - 1];
        int exListNum = UndoListHolder.undoListPlace[UndoListHolder.undoListPlace.Count - 1];
        bool retuReturned = UndoListHolder.retuReturned[UndoListHolder.retuReturned.Count - 1];


        //undo実行
        UndoCardsDealer.DealUndoCards(undoCardsList, exListNum, retuReturned);


        //処理したundoカードとList番号を削除
        UndoListHolder.undoCardsLists.RemoveAt(UndoListHolder.undoCardsLists.Count - 1);
        UndoListHolder.undoListPlace.RemoveAt(UndoListHolder.undoListPlace.Count - 1);
        UndoListHolder.retuReturned.RemoveAt(UndoListHolder.retuReturned.Count - 1);

        //Debug.Log(UndoListHolder.undoCardsLists.Count+"  "+UndoListHolder.undoListPlace.Count+ "  " +UndoListHolder.retuReturned.Count);

        yield return new WaitForSeconds(Cash.speedToRetuYama);
        //CardsUntouchabler.TouchableAllCards();
        undoB.enabled = true;
    }



}
