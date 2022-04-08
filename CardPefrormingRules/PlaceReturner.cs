using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceReturner : MonoBehaviour
{


    public static string GetPlaceFromInt(int listNum){

        string place = null;

        if (listNum <= 6)
            place = Cash.retu;
        if (listNum == 7)
            place = Cash.deck;
        if (listNum == 8)
            place = Cash.opendDeck;
        if (listNum >= 9)
            place = Cash.yama;

        return place;
    }


}
