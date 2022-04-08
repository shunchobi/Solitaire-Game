using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCompleteChecker : MonoBehaviour
{



    public static bool CheckAbleToAutoComplete(){
        int deckCount = GameListHolder.gameLists[7].Count;
        int openDeckCount = GameListHolder.gameLists[8].Count;
        if (deckCount + openDeckCount > 0)
            return false;

        for (int i = 0; i <= 6; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){
                GameObject target = GameListHolder.gameLists[i][n];
                bool isFront = target.GetComponent<CardInfo>().isFront;
                if (!isFront)
                    return false;}}

        return true;
    }


    
}
