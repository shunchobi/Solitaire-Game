using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoListHolder : MonoBehaviour {


    //一回で戻すカードが全て入っているList。（３枚めくりで３枚戻すなら一つのListに３枚入っている、子連れのretuカードなら子供含めたカード全て）
    public static List<List<GameObject>> undoCardsLists = new List<List<GameObject>>();



    //戻るListの場所(Listのインデックス番号）　
    public static List<int> undoListPlace = new List<int>();



    //retuのカードが表にめくれたか、めくれなかったか
    public static List<bool> retuReturned = new List<bool>();





    public static void AddUndoCardsLists(List<GameObject> undoCards){
        undoCardsLists.Add(undoCards);
    } 

     

    public static void AddUndoListPlace(int listPlace)
    {
        undoListPlace.Add(listPlace);

    }


    public static void AddRetuReturned(bool isReturnd)
    {
        retuReturned.Add(isReturnd);
    }





	
}
