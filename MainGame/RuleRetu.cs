using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleRetu : MonoBehaviour
{

    public static bool CheckAcceptability(GameObject child, GameObject oya, bool kingToEmpty){
        bool isAcceptable = false;

        CardInfo childInfo = child.GetComponent<CardInfo>();
        CardInfo oyaInfo = oya.GetComponent<CardInfo>();

        List<GameObject> oyaList = GameListHolder.gameLists[oyaInfo.placeListInt];
        int child_Num = childInfo.cardNum;
        int childListNum = childInfo.intInList;
        string child_SuitColor = childInfo.suitColor;
        int oya_Num = oyaInfo.cardNum;
        int oyaIntNum = oya.GetComponent<CardInfo>().intInList;
        string oya_SuitColor = oyaInfo.suitColor;
        string oya_placeName = oyaInfo.place;
        string child_placeName = childInfo.place;

        if (child_SuitColor != oya_SuitColor && child_Num + 1 == oya_Num && oyaIntNum == oyaList.Count - 1)
            isAcceptable = true;

        if (kingToEmpty == false){
            if (child_Num == 13 && oya_placeName == Cash.retu_empty && oyaList.Count == 0 && childListNum > 0)
                isAcceptable = true;
            else if(child_Num == 13 && oya_placeName == Cash.retu_empty && oyaList.Count == 0 && child_placeName == Cash.opendDeck)
                isAcceptable = true;
        }

        if (kingToEmpty == true){
            if (child_Num == 13 && oya_placeName == Cash.retu_empty && oyaList.Count == 0)
                isAcceptable = true;
        }

        return isAcceptable;
    }
}
