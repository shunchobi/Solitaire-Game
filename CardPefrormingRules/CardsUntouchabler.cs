using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsUntouchabler : MonoBehaviour
{
   






    /// <summary>
    /// 全てのカードのBoxCollider2Dをfalseにする。
    /// </summary>
    public static void UntouchableAllCards(){
        for (int i = 0; i < GameListHolder.gameLists.Count; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){
                GameObject target = GameListHolder.gameLists[i][n];
                BoxCollider2D boxCollider2D = target.GetComponent<BoxCollider2D>();
                boxCollider2D.enabled = false;
            }
        }

        GameObject.Find("row8").GetComponent<BoxCollider2D>().enabled = false;
    }




    /// <summary>
    /// 有効なカードのBoxCollider2Dのみtrueにする
    /// </summary>
    public static void TouchableAllCards()
    {
        for (int i = 0; i < GameListHolder.gameLists.Count; i++)
        {
            //retuのカード
            if (i <= 6)
            {
                for (int n = 0; n < GameListHolder.gameLists[i].Count; n++)
                {
                    GameObject target = GameListHolder.gameLists[i][n];
                    bool isFront = target.GetComponent<CardInfo>().isFront;
                    if (isFront){
                        BoxCollider2D boxCollider2D = target.GetComponent<BoxCollider2D>();
                        boxCollider2D.enabled = true;
                    }
                }
            }

            //openedDeckのカード
            if(i == 8)
            {
                if(GameListHolder.gameLists[i].Count > 0){
                    GameListHolder.gameLists[i][GameListHolder.gameLists[i].Count - 1].GetComponent<BoxCollider2D>().enabled = true;
                }
            }

            //yamaのカード
            if (i >= 9)
            {
                if (GameListHolder.gameLists[i].Count > 0)
                    GameListHolder.gameLists[i][GameListHolder.gameLists[i].Count - 1].GetComponent<BoxCollider2D>().enabled = true;
            }

        }

        GameObject.Find("row8").GetComponent<BoxCollider2D>().enabled = true;

    }





}
