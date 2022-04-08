using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPosReturner : MonoBehaviour
{


    /// <summary>
    /// 渡したカードの座標をリストのインデックス番号から求めて返す
    /// カードの座標をそのまま返すと、カードが移動中だとずれてくる
    /// </summary>
    public static Vector3 GetCardPosFromObj(GameObject obj)
    {
        CardInfo cardInfo = obj.GetComponent<CardInfo>();
        int _placeListInt = cardInfo.placeListInt;
        int _intInList = cardInfo.intInList;

        List<GameObject> list = OwnListReturner.GetList(obj);

        GameObject emptyObj = null;
        if (_placeListInt >= 9) emptyObj = EmptyObjectReturner.GetEmptyObj(_placeListInt);
        else if(_placeListInt == 8)
        {
            if(_intInList == 0) emptyObj = EmptyObjectReturner.GetEmptyObj(81);
            if (_intInList == 1) emptyObj = EmptyObjectReturner.GetEmptyObj(82);
            if (_intInList >= 2) emptyObj = EmptyObjectReturner.GetEmptyObj(83);

            return new Vector3(emptyObj.transform.position.x, emptyObj.transform.position.y, emptyObj.transform.position.z);
        }
        else emptyObj = EmptyObjectReturner.GetEmptyObj(_placeListInt + 1);

        int backCardsAmount = 0; //自身の上には何枚の裏カードがあるか。
        int frontCardsAmount = 0;　//自身の上には何枚の表カードがあるか。
        for (int i = 0; i < list.Count; i++)
        {
            if (i == _intInList) break;
            GameObject target = list[i];
            bool isFront = target.GetComponent<CardInfo>().isFront;
            if (isFront == true) frontCardsAmount += 1;
            else backCardsAmount += 1;
        }

        float yPos = 0;
        if(frontCardsAmount == 1)
            yPos = emptyObj.transform.position.y - (Cash.cardPos.spaceBtwRetu_Back * backCardsAmount) - Cash.cardPos.spaceBtwRetu_Front;
        else
            yPos = emptyObj.transform.position.y - (Cash.cardPos.spaceBtwRetu_Back * backCardsAmount) - (Cash.cardPos.spaceBtwRetu_Front * frontCardsAmount);
        

       　　 return new Vector3(emptyObj.transform.position.x, yPos, emptyObj.transform.position.z);

    }　
    　　　　　　　　　　　　
    　
    　
    　　
    　
    /// <summary>
    /// Listの場所とindex番号から座標をゲット　　　　    　　
    /// </summary>
    public static Vector3 GetCardPosFromListInt(int placeListInt, int intInList)
    {
        GameObject emptyObj = null;
        if (placeListInt >= 9) emptyObj = EmptyObjectReturner.GetEmptyObj(placeListInt);
        else emptyObj = EmptyObjectReturner.GetEmptyObj(placeListInt + 1);
        　　
        float yPos = emptyObj.transform.position.y - (Cash.cardPos.spaceBtwRetu_Back * intInList);


        return new Vector3(emptyObj.transform.position.x, yPos, emptyObj.transform.position.z);
    }




}
