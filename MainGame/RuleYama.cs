using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleYama : MonoBehaviour {


    /// <summary>
    /// Checks the possibility.
    /// </summary>
    public static bool CheckAcceptability(GameObject child, GameObject oya){
        bool isAcceptable = false;

        CardInfo childInfo = child.GetComponent<CardInfo>();
        CardInfo oyaInfo = oya.GetComponent<CardInfo>();
        List<GameObject> oyaList = OwnListReturner.GetList(oya);

        //childがListの最後のカードでなければfalseを返す。
        //最後でないとルール的にyamaへは移動できないから。
        if (childInfo.intInList != OwnListReturner.GetList(child).Count - 1)
            return false;

        int child_Num = childInfo.cardNum;
        int child_Suit = childInfo.suit;
        int oya_Num = oyaInfo.cardNum;
        int oya_Suit = oyaInfo.suit;
        string oya_placeName = oyaInfo.place;

        if (child_Num - 1 == oya_Num && child_Suit == oya_Suit)
            isAcceptable = true;

        if(child_Num == 1 && oya_placeName == Cash.yama_empty && oyaList.Count == 0)
            isAcceptable = true;


        return isAcceptable;
    }





}
