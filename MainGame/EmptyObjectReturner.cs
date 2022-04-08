using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyObjectReturner : MonoBehaviour {



    static GameObject row1;
    static GameObject row2;
    static GameObject row3;
    static GameObject row4;
    static GameObject row5;
    static GameObject row6;
    static GameObject row7;
    static GameObject row8;
    static GameObject row8_1;
    static GameObject row8_2;
    static GameObject row8_3;
    static GameObject row9;
    static GameObject row10;
    static GameObject row11;
    static GameObject row12;

    static bool isIniti = false;

    private static void Initi()
    {
        if(isIniti == false){
            row1 = GameObject.Find("row1");
            row2 = GameObject.Find("row2");
            row3 = GameObject.Find("row3");
            row4 = GameObject.Find("row4");
            row5 = GameObject.Find("row5");
            row6 = GameObject.Find("row6");
            row7 = GameObject.Find("row7");
            row8 = GameObject.Find("row8");
            row8_1 = GameObject.Find("row8_1");
            row8_2 = GameObject.Find("row8_2");
            row8_3 = GameObject.Find("row8_3");
            row9 = GameObject.Find("row9");
            row10 = GameObject.Find("row10");
            row11 = GameObject.Find("row11");
            row12 = GameObject.Find("row12");

            isIniti = true;
        }
    }


    ///ここでintの数字を渡すとそれと同じ数字のrowオブジェクトを返すメソッドを書き、
    ///TapActionDealerにからのオブジェクトも選別に加える
    public static GameObject GetEmptyObj(int num){
        Initi();
        GameObject gameObject = null;

        switch(num){
            case 1:
                gameObject = row1;
                break;
            case 2:
                gameObject = row2;
                break;
            case 3:
                gameObject = row3;
                break;
            case 4:
                gameObject = row4;
                break;
            case 5:
                gameObject = row5;
                break;
            case 6:
                gameObject = row6;
                break;
            case 7:
                gameObject = row7;
                break;
            case 8:
                gameObject = row8;
                break;
            case 81:
                gameObject = row8_1;
                break;
            case 82:
                gameObject = row8_2;
                break;
            case 83:
                gameObject = row8_3;
                break;
            case 9:
                gameObject = row9;
                break;
            case 10:
                gameObject = row10;
                break;
            case 11:
                gameObject = row11;
                break;
            case 12:
                gameObject = row12;
                break;
        }

        return gameObject;
    }
   





}
