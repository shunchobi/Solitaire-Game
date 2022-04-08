using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCardsChecker : MonoBehaviour
{


    public static bool CheckExistBackCards(){
        int backCAmount = 0;

        for (int i = 0; i < GameListHolder.gameLists.Count; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){
                GameObject target = GameListHolder.gameLists[i][n];
                if (target.GetComponent<CardInfo>().isFront == false)
                    backCAmount++;
            }
        }

        if (backCAmount >= 2)
            return true;
        else
            return false;
    }




 
}
