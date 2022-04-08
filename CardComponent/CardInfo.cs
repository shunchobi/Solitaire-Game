using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo : MonoBehaviour {


    public int suit = -1;
    public string suitColor = "null";
    public int cardNum = -1;
    public string place = "null";　//retu,deck,yama,openedDeckのどこにいるか

    public int placeListInt = -1; //retu,yama,deck,openedDeckのすべてのリスト13個のうちどのリストに現在属しているか。
    public int intInList = -1;　//属しているリストの何番目（インデックス番号）に現在いるか。　

    public bool isDragged = false;

    public bool isFront = false;

    public Sprite backSprite;
    public Sprite frontSprite;

}
